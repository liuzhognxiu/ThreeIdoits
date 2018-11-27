using UnityEngine;
using System.Collections.Generic;
using Pathfinding.Serialization.JsonFx;
using Pathfinding.Serialization;
using Pathfinding;

namespace Pathfinding {
	public interface INavmesh {
		void GetNodes(GraphNodeDelegateCancelable del);
	}
	
	[System.Serializable]
	[JsonOptIn]
	/** Generates graphs based on navmeshes.
\ingroup graphs
Navmeshes are meshes where each polygon define a walkable area.
These are great because the AI can get so much more information on how it can walk.
Polygons instead of points mean that the funnel smoother can produce really nice looking paths and the graphs are also really fast to search
and have a low memory footprint because of their smaller size to describe the same area (compared to grid graphs).
\see Pathfinding.RecastGraph

\shadowimage{navmeshgraph_graph.png}
\shadowimage{navmeshgraph_inspector.png}

	 */
	public class NavMeshGraph : NavGraph, INavmesh, IUpdatableGraph, IFunnelGraph, INavmeshHolder
	,IRaycastableGraph 
	{
		/** Mesh to construct navmesh from */
		[JsonMember]
		public Mesh sourceMesh;

		/** Offset in world space */
		[JsonMember]
		public Vector3 offset;

		/** Rotation in degrees */
		[JsonMember]
		public Vector3 rotation;

		/** Scale of the graph */
		[JsonMember]
		public float scale = 1;
		
		/** More accurate nearest node queries.
		 * When on, looks for the closest point on every triangle instead of if point is inside the node triangle in XZ space.
		 * This is slower, but a lot better if your mesh contains overlaps (e.g bridges over other areas of the mesh).
		 * Note that for maximum effect the Full Get Nearest Node Search setting should be toggled in A* Inspector Settings.
		 */
		[JsonMember]
		public bool accurateNearestNode = true;
		
		public TriangleMeshNode[] nodes;
		
		public TriangleMeshNode[] TriNodes {
			get { return nodes; }
		}
		
		public override void GetNodes (GraphNodeDelegateCancelable del) {
			if (nodes == null) return;
			for (int i=0;i<nodes.Length && del (nodes[i]);i++) {}
		}
		
		public override void OnDestroy () {
			
			base.OnDestroy ();
			
			// Cleanup
			TriangleMeshNode.SetNavmeshHolder (active.astarData.GetGraphIndex(this), null);
		}
		
		public Int3 GetVertex (int index) {
			return vertices[index];
		}
		
		public int GetVertexArrayIndex (int index) {
			return index;
		}
		
		public void GetTileCoordinates (int tileIndex, out int x, out int z) {
			//Tiles not supported
			x = z = 0;
		}
		
		/** Bounding Box Tree. Enables really fast lookups of nodes. \astarpro */
		BBTree _bbTree;
		public BBTree bbTree {
			get { return _bbTree; }
			set { _bbTree = value;}
		}
		
		[System.NonSerialized]
		Int3[] _vertices;
		
		public Int3[] vertices {
			get {
				return _vertices;
			}
			set {
				_vertices = value;
			}
		}
		
		[System.NonSerialized]
		Vector3[] originalVertices;
		
		[System.NonSerialized]
		public int[] triangles;
		
		public void GenerateMatrix () {
#if !PhotonImplementation
			SetMatrix (Matrix4x4.TRS (offset,Quaternion.Euler (rotation),new Vector3 (scale,scale,scale)));
#else
			SetMatrix (Matrix4x4.TRS (offset,rotation,new Vector3 (scale,scale,scale)));
#endif
			
		}
		
		/** Transforms the nodes using newMatrix from their initial positions.
		 * The "oldMatrix" variable can be left out in this function call (only for this graph generator)
		 * since the information can be taken from other saved data, which gives better precision.
		 */
		public override void RelocateNodes (Matrix4x4 oldMatrix, Matrix4x4 newMatrix) {
			
			if (vertices == null || vertices.Length == 0 || originalVertices == null || originalVertices.Length != vertices.Length) {
				return;
			}
			
			for (int i=0;i<_vertices.Length;i++) {
				_vertices[i] = (Int3)newMatrix.MultiplyPoint3x4 (originalVertices[i]);
			}
			
			for (int i=0;i<nodes.Length;i++) {
				var node = nodes[i];
				node.UpdatePositionFromVertices();
				
				if (node.connections != null) {
					for (int q=0;q<node.connections.Length;q++) {
						node.connectionCosts[q] = (uint)(node.position-node.connections[q].position).costMagnitude;
					}
				}
			}
			
			SetMatrix (newMatrix);

			RebuildBBTree (this);
		}
	
		public static NNInfo GetNearest (NavMeshGraph graph, GraphNode[] nodes, Vector3 position, NNConstraint constraint, bool accurateNearestNode) {
			if (nodes == null || nodes.Length == 0) {
				Debug.LogError ("NavGraph hasn't been generated yet or does not contain any nodes");
				return new NNInfo ();
			}
			
			if (constraint == null) constraint = NNConstraint.None;
			
			
			Int3[] vertices = graph.vertices;
			
			//Query BBTree
			
			if (graph.bbTree == null) {
				/** \todo Change method to require a navgraph */
				return GetNearestForce (graph, graph, position, constraint, accurateNearestNode);
			}
			
			//Searches in radiuses of 0.05 - 0.2 - 0.45 ... 1.28 times the average of the width and depth of the bbTree
			float w = (graph.bbTree.Size.width + graph.bbTree.Size.height)*0.5F*0.02F;
			
			NNInfo query = graph.bbTree.QueryCircle (position,w,constraint);//graph.bbTree.Query (position,constraint);
			
			if (query.node == null) {
				
				for (int i=1;i<=8;i++) {
					query = graph.bbTree.QueryCircle (position, i*i*w, constraint);
					
					if (query.node != null || (i-1)*(i-1)*w > AstarPath.active.maxNearestNodeDistance*2) { // *2 for a margin
						break;
					}
				}
			}
			
			if (query.node != null) {
				query.clampedPosition = ClosestPointOnNode (query.node as TriangleMeshNode,vertices,position);
			}
			
			if (query.constrainedNode != null) {
				if (constraint.constrainDistance && ((Vector3)query.constrainedNode.position - position).sqrMagnitude > AstarPath.active.maxNearestNodeDistanceSqr) {
					query.constrainedNode = null;
				} else {
					query.constClampedPosition = ClosestPointOnNode (query.constrainedNode as TriangleMeshNode, vertices, position);
				}
			}
			
			return query;	
		}
		
		public override NNInfo GetNearest (Vector3 position, NNConstraint constraint, GraphNode hint) {
			return GetNearest (this, nodes,position, constraint, accurateNearestNode);
		}
		
		/** This performs a linear search through all polygons returning the closest one.
		 * This is usually only called in the Free version of the A* Pathfinding Project since the Pro one supports BBTrees and will do another query
		 */
		public override NNInfo GetNearestForce (Vector3 position, NNConstraint constraint) {
			
			return GetNearestForce (this, this,position,constraint, accurateNearestNode);
			//Debug.LogWarning ("This function shouldn't be called since constrained nodes are sent back in the GetNearest call");
			
			//return new NNInfo ();
		}
		
		/** This performs a linear search through all polygons returning the closest one */
		public static NNInfo GetNearestForce (NavGraph graph, INavmeshHolder navmesh, Vector3 position, NNConstraint constraint, bool accurateNearestNode) {
			NNInfo nn = GetNearestForceBoth (graph, navmesh,position,constraint,accurateNearestNode);
			nn.node = nn.constrainedNode;
			nn.clampedPosition = nn.constClampedPosition;
			return nn;
		}
		
		/** This performs a linear search through all polygons returning the closest one.
		  * This will fill the NNInfo with .node for the closest node not necessarily complying with the NNConstraint, and .constrainedNode with the closest node
		  * complying with the NNConstraint.
		  * \see GetNearestForce(Node[],Int3[],Vector3,NNConstraint,bool)
		  */
		public static NNInfo GetNearestForceBoth (NavGraph graph, INavmeshHolder navmesh, Vector3 position, NNConstraint constraint, bool accurateNearestNode) {
			var pos = (Int3)position;
			
			float minDist = -1;
			GraphNode minNode = null;
			
			float minConstDist = -1;
			GraphNode minConstNode = null;
			
			float maxDistSqr = constraint.constrainDistance ? AstarPath.active.maxNearestNodeDistanceSqr : float.PositiveInfinity;
			
			GraphNodeDelegateCancelable del = delegate (GraphNode _node) {
				var node = _node as TriangleMeshNode;
				
				if (accurateNearestNode) {
					
					Vector3 closest = node.ClosestPointOnNode (position);
					float dist = ((Vector3)pos-closest).sqrMagnitude;
					
					if (minNode == null || dist < minDist) {
						minDist = dist;
						minNode = node;
					}
					
					if (dist < maxDistSqr && constraint.Suitable (node)) {
						if (minConstNode == null || dist < minConstDist) {
							minConstDist = dist;
							minConstNode = node;
						}
					}
					
				} else {
					
					if (!node.ContainsPoint ((Int3)position)) {
						
						float dist = (node.position-pos).sqrMagnitude;
						if (minNode == null || dist < minDist) {
							minDist = dist;
							minNode = node;
						}
						
						if (dist < maxDistSqr && constraint.Suitable (node)) {
							if (minConstNode == null || dist < minConstDist) {
								minConstDist = dist;
								minConstNode = node;
							}
						}
						
					} else {
						int dist = AstarMath.Abs (node.position.y-pos.y);
						
						if (minNode == null || dist < minDist) {
							minDist = dist;
							minNode = node;
						}
						
						if (dist < maxDistSqr && constraint.Suitable (node)) {
							if (minConstNode == null || dist < minConstDist) {
								minConstDist = dist;
								minConstNode = node;
							}
						}
					}
				}
				return true;
			};
			
			graph.GetNodes (del);
			
			var nninfo = new NNInfo (minNode);
			
			//Find the point closest to the nearest triangle
				
			if (nninfo.node != null) {
				var node = nninfo.node as TriangleMeshNode;//minNode2 as MeshNode;
				
				Vector3 clP = node.ClosestPointOnNode (position);
				
				nninfo.clampedPosition = clP;
			}
			
			nninfo.constrainedNode = minConstNode;
			if (nninfo.constrainedNode != null) {
				var node = nninfo.constrainedNode as TriangleMeshNode;//minNode2 as MeshNode;
				
				Vector3 clP = node.ClosestPointOnNode (position);
				
				nninfo.constClampedPosition = clP;
			}
			
			return nninfo;
		}
		
		public void BuildFunnelCorridor (List<GraphNode> path, int startIndex, int endIndex, List<Vector3> left, List<Vector3> right) {
			BuildFunnelCorridor (this,path,startIndex,endIndex,left,right);
		}
		
		public static void BuildFunnelCorridor (INavmesh graph, List<GraphNode> path, int startIndex, int endIndex, List<Vector3> left, List<Vector3> right) {
			
			if (graph == null) {
				Debug.LogError ("Couldn't cast graph to the appropriate type (graph isn't a Navmesh type graph, it doesn't implement the INavmesh interface)");
				return;
			}
			
			for (int i=startIndex;i<endIndex;i++) {
				//Find the connection between the nodes
				
				var n1 = path[i] as TriangleMeshNode;
				var n2 = path[i+1] as TriangleMeshNode;
				
				int a;
				bool search = true;
				for (a=0;a<3;a++) {
					for (int b=0;b<3;b++) {
						if (n1.GetVertexIndex(a) == n2.GetVertexIndex((b+1)%3) && n1.GetVertexIndex((a+1)%3) == n2.GetVertexIndex(b)) {
							search = false;
							break;
						}
					}
					if (!search) break;
				}
				
				if (a == 3) {
					left.Add ((Vector3)n1.position);
					right.Add ((Vector3)n1.position);
					left.Add ((Vector3)n2.position);
					right.Add ((Vector3)n2.position);
				} else {
					left.Add ((Vector3)n1.GetVertex(a));
					right.Add ((Vector3)n1.GetVertex((a+1)%3));
				}
			}
		}
		
		public void AddPortal (GraphNode n1, GraphNode n2, List<Vector3> left, List<Vector3> right) {
		}
		
		/** Returns if there is an obstacle between \a origin and \a end on the graph.
		 * This is not the same as Physics.Linecast, this function traverses the \b graph and looks for collisions instead of checking for collider intersection.
		 * \astarpro */
		public bool Linecast (Vector3 origin, Vector3 end) {
			return Linecast (origin, end, GetNearest (origin, NNConstraint.None).node);
		}
		
		/** Returns if there is an obstacle between \a origin and \a end on the graph.
		 * \param [in] origin Point to linecast from
		 * \param [in] end Point to linecast to
		 * \param [out] hit Contains info on what was hit, see GraphHitInfo
		 * \param [in] hint You need to pass the node closest to the start point
		 * This is not the same as Physics.Linecast, this function traverses the \b graph and looks for collisions instead of checking for collider intersection.
		 * \astarpro */
		public bool Linecast (Vector3 origin, Vector3 end, GraphNode hint, out GraphHitInfo hit) {
			return Linecast (this as INavmesh, origin,end,hint, out hit, null);
		}
		
		/** Returns if there is an obstacle between \a origin and \a end on the graph.
		 * \param [in] origin Point to linecast from
		 * \param [in] end Point to linecast to
		 * \param [in] hint You need to pass the node closest to the start point
		 * This is not the same as Physics.Linecast, this function traverses the \b graph and looks for collisions instead of checking for collider intersection.
		 * \astarpro */
		public bool Linecast (Vector3 origin, Vector3 end, GraphNode hint) {
			GraphHitInfo hit;
			return Linecast (this as INavmesh, origin,end,hint, out hit, null);
		}
		
		/** Returns if there is an obstacle between \a origin and \a end on the graph.
		 * \param [in] origin Point to linecast from
		 * \param [in] end Point to linecast to
		 * \param [out] hit Contains info on what was hit, see GraphHitInfo
		 * \param [in] hint You need to pass the node closest to the start point
		 * \param trace If a list is passed, then it will be filled with all nodes the linecast traverses
		 * This is not the same as Physics.Linecast, this function traverses the \b graph and looks for collisions instead of checking for collider intersection.
		 * \astarpro */
		public bool Linecast (Vector3 origin, Vector3 end, GraphNode hint, out GraphHitInfo hit, List<GraphNode> trace) {
			return Linecast (this as INavmesh, origin,end,hint, out hit, trace);
		}
		
		/** Returns if there is an obstacle between \a origin and \a end on the graph.
		 * \param [in] graph The graph to perform the search on
		 * \param [in] tmp_origin Point to start from
		 * \param [in] tmp_end Point to linecast to
		 * \param [out] hit Contains info on what was hit, see GraphHitInfo
		 * \param [in] hint You need to pass the node closest to the start point, if null, a search for the closest node will be done
		 * This is not the same as Physics.Linecast, this function traverses the \b graph and looks for collisions instead of checking for collider intersection.
		 * \astarpro */
		public static bool Linecast (INavmesh graph, Vector3 tmp_origin, Vector3 tmp_end, GraphNode hint, out GraphHitInfo hit) {
			return Linecast (graph, tmp_origin, tmp_end, hint, out hit, null);
		}
			
		/** Returns if there is an obstacle between \a origin and \a end on the graph.
		 * \param [in] graph The graph to perform the search on
		 * \param [in] tmp_origin Point to start from
		 * \param [in] tmp_end Point to linecast to
		 * \param [out] hit Contains info on what was hit, see GraphHitInfo
		 * \param [in] hint You need to pass the node closest to the start point, if null, a search for the closest node will be done
		 * \param trace If a list is passed, then it will be filled with all nodes the linecast traverses
		 * This is not the same as Physics.Linecast, this function traverses the \b graph and looks for collisions instead of checking for collider intersection.
		 * \astarpro */
		public static bool Linecast (INavmesh graph, Vector3 tmp_origin, Vector3 tmp_end, GraphNode hint, out GraphHitInfo hit, List<GraphNode> trace) {
			var end = (Int3)tmp_end;
			var origin = (Int3)tmp_origin;
			
			hit = new GraphHitInfo ();
			
			if (float.IsNaN (tmp_origin.x + tmp_origin.y + tmp_origin.z)) throw new System.ArgumentException ("origin is NaN");
			if (float.IsNaN (tmp_end.x + tmp_end.y + tmp_end.z)) throw new System.ArgumentException ("end is NaN");
			
			var node = hint as TriangleMeshNode;
			if (node == null) {
				node = (graph as NavGraph).GetNearest (tmp_origin, NNConstraint.None).node as TriangleMeshNode;
				
				if (node == null) {
					Debug.LogError ("Could not find a valid node to start from");
					hit.point = tmp_origin;
					return true;
				}
			}
			
			if (origin == end) {
				hit.node = node;
				return false;
			}
			
			origin = (Int3)node.ClosestPointOnNode ((Vector3)origin);
			hit.origin = (Vector3)origin;
			
			if (!node.Walkable) {
				hit.point = (Vector3)origin;
				hit.tangentOrigin = (Vector3)origin;
				return true;
			}
			
			
			List<Vector3> left = Pathfinding.Util.ListPool<Vector3>.Claim();//new List<Vector3>(1);
			List<Vector3> right = Pathfinding.Util.ListPool<Vector3>.Claim();//new List<Vector3>(1);

			int counter = 0;
			while (true) {

				counter++;
				if ( counter > 2000 ) {
					Debug.LogError ("Linecast was stuck in infinite loop. Breaking.");
					Pathfinding.Util.ListPool<Vector3>.Release(left);
					Pathfinding.Util.ListPool<Vector3>.Release(right);
					return true;
				}

				TriangleMeshNode newNode = null;
				
				if (trace != null) trace.Add (node);
				
				if (node.ContainsPoint (end)) {
					Pathfinding.Util.ListPool<Vector3>.Release(left);
					Pathfinding.Util.ListPool<Vector3>.Release(right);
					return false;
				}
				
				for (int i=0;i<node.connections.Length;i++) {
					//Nodes on other graphs should not be considered
					//They might even be of other types (not MeshNode)
					if (node.connections[i].GraphIndex != node.GraphIndex) continue;
					
					left.Clear();
					right.Clear();
					
					if (!node.GetPortal (node.connections[i],left,right,false)) continue;
					
					Vector3 a = left[0];
					Vector3 b = right[0];
					
					//i.e Right or colinear
					if (!Polygon.LeftNotColinear (a,b,hit.origin)) {
						if (Polygon.LeftNotColinear (a, b, tmp_end)) {
							//Since polygons are laid out in clockwise order, the ray would intersect (if intersecting) this edge going in to the node, not going out from it
							continue;
						}
					}
					
					float factor1, factor2;
					
					if (Polygon.IntersectionFactor (a,b,hit.origin,tmp_end, out factor1, out factor2)) {
						//Intersection behind the start
						if (factor2 < 0) continue;
						
						if (factor1 >= 0 && factor1 <= 1) {
							newNode = node.connections[i] as TriangleMeshNode;
							break;
						}
					}
				}
				
				if (newNode == null) {
					//Possible edge hit
					int vs = node.GetVertexCount();
					
					for (int i=0;i<vs;i++) {
						var a = (Vector3)node.GetVertex(i);
						var b = (Vector3)node.GetVertex((i + 1) % vs);
						
						
						//i.e right or colinear
						if (!Polygon.LeftNotColinear (a,b,hit.origin)) {
							//Since polygons are laid out in clockwise order, the ray would intersect (if intersecting) this edge going in to the node, not going out from it
							if (Polygon.LeftNotColinear (a, b, tmp_end)) {
								//Since polygons are laid out in clockwise order, the ray would intersect (if intersecting) this edge going in to the node, not going out from it
								continue;
							}
						}
						
						float factor1, factor2;
						if (Polygon.IntersectionFactor (a,b,hit.origin,tmp_end, out factor1, out factor2)) {
							if (factor2 < 0) continue;
							
							if (factor1 >= 0 && factor1 <= 1) {
								Vector3 intersectionPoint = a + (b-a)*factor1;
								hit.point = intersectionPoint;
								hit.node = node;
								hit.tangent = b-a;
								hit.tangentOrigin = a;
								
								Pathfinding.Util.ListPool<Vector3>.Release(left);
								Pathfinding.Util.ListPool<Vector3>.Release(right);
								return true;
							}
						}
					}
					
					//Ok, this is wrong...
					Debug.LogWarning ("Linecast failing because point not inside node, and line does not hit any edges of it");
					
					Pathfinding.Util.ListPool<Vector3>.Release(left);
					Pathfinding.Util.ListPool<Vector3>.Release(right);
					return false;
				}
				
				node = newNode;
			}
		}
		
		public GraphUpdateThreading CanUpdateAsync (GraphUpdateObject o) {
			return GraphUpdateThreading.UnityThread;
		}
		
		public void UpdateAreaInit (GraphUpdateObject o) {}
		
		public void UpdateArea (GraphUpdateObject o) {
			UpdateArea (o, this);
		}
		
		
		public static void UpdateArea (GraphUpdateObject o, INavmesh graph) {
			
			//System.DateTime startTime = System.DateTime.UtcNow;
				
			Bounds bounds = o.bounds;
			
			Rect r = Rect.MinMaxRect (bounds.min.x,bounds.min.z,bounds.max.x,bounds.max.z);
			
			var r2 = new IntRect(
				Mathf.FloorToInt(bounds.min.x*Int3.Precision),
				Mathf.FloorToInt(bounds.min.z*Int3.Precision),
				Mathf.FloorToInt(bounds.max.x*Int3.Precision),
				Mathf.FloorToInt(bounds.max.z*Int3.Precision)
			);

			var a = new Int3(r2.xmin,0,r2.ymin);
			var b = new Int3(r2.xmin,0,r2.ymax);
			var c = new Int3(r2.xmax,0,r2.ymin);
			var d = new Int3(r2.xmax,0,r2.ymax);
			
			graph.GetNodes (_node => {
				var node = _node as TriangleMeshNode;
				
				bool inside = false;
				
				int allLeft = 0;
				int allRight = 0;
				int allTop = 0;
				int allBottom = 0;
				
				for (int v=0;v<3;v++) {
					
					Int3 p = node.GetVertex(v);
					var vert = (Vector3)p;
					
					if (r2.Contains (p.x,p.z)) {
						inside = true;
						break;
					}
					
					if (vert.x < r.xMin) allLeft++;
					if (vert.x > r.xMax) allRight++;
					if (vert.z < r.yMin) allTop++;
					if (vert.z > r.yMax) allBottom++;
				}
				if (!inside) {
					if (allLeft == 3 || allRight == 3 || allTop == 3 || allBottom == 3) {
						return true;
					}
				}
				
				for (int v=0;v<3;v++) {
					int v2 = v > 1 ? 0 : v+1;
					
					Int3 vert1 = node.GetVertex(v);
					Int3 vert2 = node.GetVertex(v2);
					
					if (Polygon.Intersects (a,b,vert1,vert2)) { inside = true; break; }
					if (Polygon.Intersects (a,c,vert1,vert2)) { inside = true; break; }
					if (Polygon.Intersects (c,d,vert1,vert2)) { inside = true; break; }
					if (Polygon.Intersects (d,b,vert1,vert2)) { inside = true; break; }
				}
				
				if (node.ContainsPoint (a) || node.ContainsPoint (b) || node.ContainsPoint (c) || node.ContainsPoint (d)) {
					inside = true;
				}
				
				if (!inside) {
					return true;
				}
				
				o.WillUpdateNode(node);
				o.Apply (node);
				return true;
			});
		}
		
		/** Returns the closest point of the node */
		public static Vector3 ClosestPointOnNode (TriangleMeshNode node, Int3[] vertices, Vector3 pos) {
			return Polygon.ClosestPointOnTriangle ((Vector3)vertices[node.v0],(Vector3)vertices[node.v1],(Vector3)vertices[node.v2],pos);
		}
		
		/** Returns if the point is inside the node in XZ space */
		public bool ContainsPoint (TriangleMeshNode node, Vector3 pos) {
			if (	Polygon.IsClockwise ((Vector3)vertices[node.v0],(Vector3)vertices[node.v1], pos)
			    && 	Polygon.IsClockwise ((Vector3)vertices[node.v1],(Vector3)vertices[node.v2], pos)
			    && 	Polygon.IsClockwise ((Vector3)vertices[node.v2],(Vector3)vertices[node.v0], pos)) {
				return true;
			}
			return false;
		}
		
		/** Returns if the point is inside the node in XZ space */
		public static bool ContainsPoint (TriangleMeshNode node, Vector3 pos, Int3[] vertices) {
			if (!Polygon.IsClockwiseMargin ((Vector3)vertices[node.v0],(Vector3)vertices[node.v1], (Vector3)vertices[node.v2])) {
				Debug.LogError ("Noes!");
			}
			
			if ( 	Polygon.IsClockwiseMargin ((Vector3)vertices[node.v0],(Vector3)vertices[node.v1], pos)
			    && 	Polygon.IsClockwiseMargin ((Vector3)vertices[node.v1],(Vector3)vertices[node.v2], pos)
			    && 	Polygon.IsClockwiseMargin ((Vector3)vertices[node.v2],(Vector3)vertices[node.v0], pos)) {
				return true;
			}
			return false;
		}
		
		/** Scans the graph using the path to an .obj mesh */
		public void ScanInternal (string objMeshPath) {
			
			Mesh mesh = ObjImporter.ImportFile (objMeshPath);
			
			if (mesh == null) {
				Debug.LogError ("Couldn't read .obj file at '"+objMeshPath+"'");
				return;
			}
			
			sourceMesh = mesh;
			ScanInternal ();
		}
		
		public override void ScanInternal (OnScanStatus statusCallback) {
			
			if (sourceMesh == null) {
				return;
			}
			
			GenerateMatrix ();
			
			//float startTime = 0;//Time.realtimeSinceStartup;
			
			Vector3[] vectorVertices = sourceMesh.vertices;
			
			triangles = sourceMesh.triangles;
			
			TriangleMeshNode.SetNavmeshHolder (active.astarData.GetGraphIndex(this),this);
			GenerateNodes (vectorVertices,triangles, out originalVertices, out _vertices);
		
		}
		
		/** Generates a navmesh. Based on the supplied vertices and triangles. Memory usage is about O(n) */
		void GenerateNodes (Vector3[] vectorVertices, int[] triangles, out Vector3[] originalVertices, out Int3[] vertices) {
			
			UnityEngine.Profiling.Profiler.BeginSample ("Init");

			if (vectorVertices.Length == 0 || triangles.Length == 0) {
				originalVertices = vectorVertices;
				vertices = new Int3[0];
				//graph.CreateNodes (0);
				nodes = new TriangleMeshNode[0];
				return;
			}
			
			vertices = new Int3[vectorVertices.Length];
			
			int c = 0;
			
			for (int i=0;i<vertices.Length;i++) {
				vertices[i] = (Int3)matrix.MultiplyPoint3x4 (vectorVertices[i]);
			}
			
			var hashedVerts = new Dictionary<Int3,int> ();
			
			var newVertices = new int[vertices.Length];
				
			UnityEngine.Profiling.Profiler.EndSample ();
			UnityEngine.Profiling.Profiler.BeginSample ("Hashing");

			for (int i=0;i<vertices.Length;i++) {
				if (!hashedVerts.ContainsKey (vertices[i])) {
					newVertices[c] = i;
					hashedVerts.Add (vertices[i], c);
					c++;
				}
			}
			
			for (int x=0;x<triangles.Length;x++) {
				Int3 vertex = vertices[triangles[x]];

				triangles[x] = hashedVerts[vertex];
			}
			
			Int3[] totalIntVertices = vertices;
			vertices = new Int3[c];
			originalVertices = new Vector3[c];
			for (int i=0;i<c;i++) {
				
				vertices[i] = totalIntVertices[newVertices[i]];
				originalVertices[i] = vectorVertices[newVertices[i]];
			}

			UnityEngine.Profiling.Profiler.EndSample ();
			UnityEngine.Profiling.Profiler.BeginSample ("Constructing Nodes");

			nodes = new TriangleMeshNode[triangles.Length/3];
			
			int graphIndex = active.astarData.GetGraphIndex(this); 

			// Does not have to set this, it is set in ScanInternal
			//TriangleMeshNode.SetNavmeshHolder ((int)graphIndex,this);

			for (int i=0;i<nodes.Length;i++) {
				
				nodes[i] = new TriangleMeshNode(active);
				TriangleMeshNode node = nodes[i];//new MeshNode ();
				
				node.GraphIndex = (uint)graphIndex;
				node.Penalty = initialPenalty;
				node.Walkable = true;
				
				
				node.v0 = triangles[i*3];
				node.v1 = triangles[i*3+1];
				node.v2 = triangles[i*3+2];
				
				if (!Polygon.IsClockwise (vertices[node.v0],vertices[node.v1],vertices[node.v2])) {
					//Debug.DrawLine (vertices[node.v0],vertices[node.v1],Color.red);
					//Debug.DrawLine (vertices[node.v1],vertices[node.v2],Color.red);
					//Debug.DrawLine (vertices[node.v2],vertices[node.v0],Color.red);
					
					int tmp = node.v0;
					node.v0 = node.v2;
					node.v2 = tmp;
				}
				
				if (Polygon.IsColinear (vertices[node.v0],vertices[node.v1],vertices[node.v2])) {
					Debug.DrawLine ((Vector3)vertices[node.v0],(Vector3)vertices[node.v1],Color.red);
					Debug.DrawLine ((Vector3)vertices[node.v1],(Vector3)vertices[node.v2],Color.red);
					Debug.DrawLine ((Vector3)vertices[node.v2],(Vector3)vertices[node.v0],Color.red);
				}
				
				// Make sure position is correctly set
				node.UpdatePositionFromVertices();
			}

			UnityEngine.Profiling.Profiler.EndSample ();

			var sides = new Dictionary<Int2, TriangleMeshNode>();
			
			for (int i=0, j=0;i<triangles.Length; j+=1, i+=3) {
				sides[new Int2(triangles[i+0],triangles[i+1])] = nodes[j];
				sides[new Int2(triangles[i+1],triangles[i+2])] = nodes[j];
				sides[new Int2(triangles[i+2],triangles[i+0])] = nodes[j];
			}

			UnityEngine.Profiling.Profiler.BeginSample ("Connecting Nodes");

			var connections = new List<MeshNode> ();
			var connectionCosts = new List<uint> ();
			
			for (int i=0, j=0;i<triangles.Length; j+=1, i+=3) {
				connections.Clear ();
				connectionCosts.Clear ();
				
				TriangleMeshNode node = nodes[j];

				for ( int q = 0; q < 3; q++ ) {
					TriangleMeshNode other;
					if (sides.TryGetValue ( new Int2 (triangles[i+((q+1)%3)], triangles[i+q]), out other ) ) {
						connections.Add (other);
						connectionCosts.Add ((uint)(node.position-other.position).costMagnitude);
					}
				}

				node.connections = connections.ToArray ();
				node.connectionCosts = connectionCosts.ToArray ();
			}

			UnityEngine.Profiling.Profiler.EndSample ();
			UnityEngine.Profiling.Profiler.BeginSample ("Rebuilding BBTree");

			RebuildBBTree (this);

			UnityEngine.Profiling.Profiler.EndSample ();

#if ASTARDEBUG
			for (int i=0;i<nodes.Length;i++) {
				TriangleMeshNode node = nodes[i] as TriangleMeshNode;
				
				float a1 = Polygon.TriangleArea2 ((Vector3)vertices[node.v0],(Vector3)vertices[node.v1],(Vector3)vertices[node.v2]);
				
				long a2 = Polygon.TriangleArea2 (vertices[node.v0],vertices[node.v1],vertices[node.v2]);
				if (a1 * a2 < 0) Debug.LogError (a1+ " " + a2);
				
				
				if (Polygon.IsClockwise (vertices[node.v0],vertices[node.v1],vertices[node.v2])) {
					Debug.DrawLine ((Vector3)vertices[node.v0],(Vector3)vertices[node.v1],Color.green);
					Debug.DrawLine ((Vector3)vertices[node.v1],(Vector3)vertices[node.v2],Color.green);
					Debug.DrawLine ((Vector3)vertices[node.v2],(Vector3)vertices[node.v0],Color.green);
				} else {
					Debug.DrawLine ((Vector3)vertices[node.v0],(Vector3)vertices[node.v1],Color.red);
					Debug.DrawLine ((Vector3)vertices[node.v1],(Vector3)vertices[node.v2],Color.red);
					Debug.DrawLine ((Vector3)vertices[node.v2],(Vector3)vertices[node.v0],Color.red);
				}
			}
#endif
			//Debug.Log ("Graph Generation - NavMesh - Time to compute graph "+((Time.realtimeSinceStartup-startTime)*1000F).ToString ("0")+"ms");
		}
		
		/** Rebuilds the BBTree on a NavGraph.
		 * \astarpro
		 * \see NavMeshGraph.bbTree */
		public static void RebuildBBTree (NavMeshGraph graph) {
			//Build Axis Aligned Bounding Box Tree

			BBTree bbTree = graph.bbTree;
			bbTree = bbTree ?? new BBTree (graph);
			bbTree.Clear ();

			var nodes = graph.nodes;
			for (int i=nodes.Length-1;i>=0;i--) {
				bbTree.Insert (nodes[i]);
			}
			
			graph.bbTree = bbTree;
		}
		
		public void PostProcess () {
#if FALSE
			int rnd = Random.Range (0,nodes.Length);
			
			GraphNode nodex = nodes[rnd];
			
			NavGraph gr = null;
			
			if (AstarPath.active.astarData.GetGraphIndex(this) == 0) {
				gr = AstarPath.active.graphs[1];
			} else {
				gr = AstarPath.active.graphs[0];
			}
			
			rnd = Random.Range (0,gr.nodes.Length);
			
			List<GraphNode> connections = new List<GraphNode> ();
			List<int> connectionCosts = new List<int> ();
			
			connections.AddRange (nodex.connections);
			connectionCosts.AddRange (nodex.connectionCosts);
			
			GraphNode otherNode = gr.nodes[rnd];
			
			connections.Add (otherNode);
			connectionCosts.Add ((nodex.position-otherNode.position).costMagnitude);
			
			nodex.connections = connections.ToArray ();
			nodex.connectionCosts = connectionCosts.ToArray ();
#endif
		}
		
		public override void OnDrawGizmos (bool drawNodes) {
			
			if (!drawNodes) {
				return;
			}

			Matrix4x4 preMatrix = matrix;
			
			GenerateMatrix ();
			
			if (nodes == null) {
				//Scan ();
			}
			
			if (nodes == null) {
				return;
			}

			if ( bbTree != null ) {
				bbTree.OnDrawGizmos ();
			}

			if (preMatrix != matrix) {
				//Debug.Log ("Relocating Nodes");
				RelocateNodes (preMatrix, matrix);
			}
			
			PathHandler debugData = AstarPath.active.debugPathData;
			for (int i=0;i<nodes.Length;i++) {
				
				
				var node = nodes[i];
				
				Gizmos.color = NodeColor (node,AstarPath.active.debugPathData);
				
				if (node.Walkable ) {
					
					if (AstarPath.active.showSearchTree && debugData != null && debugData.GetPathNode(node).parent != null) {
						Gizmos.DrawLine ((Vector3)node.position,(Vector3)debugData.GetPathNode(node).parent.node.position);
					} else {
						for (int q=0;q<node.connections.Length;q++) {
							Gizmos.DrawLine ((Vector3)node.position,Vector3.Lerp ((Vector3)node.position, (Vector3)node.connections[q].position, 0.45f));
						}
					}
				
					Gizmos.color = AstarColor.MeshEdgeColor;
				} else {
					Gizmos.color = AstarColor.UnwalkableNode;
				}
				Gizmos.DrawLine ((Vector3)vertices[node.v0],(Vector3)vertices[node.v1]);
				Gizmos.DrawLine ((Vector3)vertices[node.v1],(Vector3)vertices[node.v2]);
				Gizmos.DrawLine ((Vector3)vertices[node.v2],(Vector3)vertices[node.v0]);
				
			}
			
		}
		
		public override void DeserializeExtraInfo (GraphSerializationContext ctx) {
			
			uint graphIndex = (uint)ctx.graphIndex;
			TriangleMeshNode.SetNavmeshHolder ((int)graphIndex,this);
			
			int c1 = ctx.reader.ReadInt32();
			int c2 = ctx.reader.ReadInt32();
			
			if (c1 == -1) {
				nodes = new TriangleMeshNode[0];
				_vertices = new Int3[0];
				originalVertices = new Vector3[0];
			}
			
			nodes = new TriangleMeshNode[c1];
			_vertices = new Int3[c2];
			originalVertices = new Vector3[c2];
			
			for (int i=0;i<c2;i++) {
				_vertices[i] = new Int3(ctx.reader.ReadInt32(), ctx.reader.ReadInt32(), ctx.reader.ReadInt32());
				originalVertices[i] = new Vector3(ctx.reader.ReadSingle(), ctx.reader.ReadSingle(), ctx.reader.ReadSingle());
			}
			
			bbTree = new BBTree(this);
			
			for (int i=0;i<c1;i++) {
				nodes[i] = new TriangleMeshNode(active);
				TriangleMeshNode node = nodes[i];
				node.DeserializeNode(ctx);
				node.UpdatePositionFromVertices();
				bbTree.Insert (node);
			}
		}
		
		public override void SerializeExtraInfo (GraphSerializationContext ctx) {
			if (nodes == null || originalVertices == null || _vertices == null || originalVertices.Length != _vertices.Length) {
				ctx.writer.Write (-1);
				ctx.writer.Write (-1);
				return;
			}
			ctx.writer.Write(nodes.Length);
			ctx.writer.Write(_vertices.Length);
			
			for (int i=0;i<_vertices.Length;i++) {
				ctx.writer.Write (_vertices[i].x);
				ctx.writer.Write (_vertices[i].y);
				ctx.writer.Write (_vertices[i].z);
				
				ctx.writer.Write (originalVertices[i].x);
				ctx.writer.Write (originalVertices[i].y);
				ctx.writer.Write (originalVertices[i].z);
			}
			
			for (int i=0;i<nodes.Length;i++) {
				nodes[i].SerializeNode (ctx);
			}
		}

#if ASTAR_NO_JSON

		public override void SerializeSettings ( GraphSerializationContext ctx ) {
			base.SerializeSettings (ctx);

			ctx.SerializeUnityObject ( sourceMesh );

			ctx.SerializeVector3 (offset);
			ctx.SerializeVector3 (rotation);

			ctx.writer.Write(scale);
			ctx.writer.Write(accurateNearestNode);
		}
		
		public override void DeserializeSettings ( GraphSerializationContext ctx ) {
			base.DeserializeSettings (ctx);
			
			sourceMesh = ctx.DeserializeUnityObject () as Mesh;

			offset = ctx.DeserializeVector3 ();
			rotation = ctx.DeserializeVector3 ();
			scale = ctx.reader.ReadSingle();
			accurateNearestNode = ctx.reader.ReadBoolean();
		}
#endif
	}
}