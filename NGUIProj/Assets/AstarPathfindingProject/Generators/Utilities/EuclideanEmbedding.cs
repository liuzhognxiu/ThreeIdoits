#pragma warning disable 414
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

namespace Pathfinding {
	public enum HeuristicOptimizationMode {
		None,
		Random,
		RandomSpreadOut,
		Custom
	}

	/** Implements heuristic optimizations.
	 *
	 * \see heuristic-opt
	 * \see Game AI Pro - Pathfinding Architecture Optimizations by Steve Rabin and Nathan R. Sturtevant
	 *
	 * \astarpro
	 */
	[System.Serializable]
	public class EuclideanEmbedding {

		public HeuristicOptimizationMode mode;

		public int seed;

		/** All children of this transform will be used as pivot points */
		public Transform pivotPointRoot;

		public int spreadOutCount = 1;

		[System.NonSerialized]
		public bool dirty;

		/**
		 * Costs laid out as n*[int],n*[int],n*[int] where n is the number of pivot points.
		 * Each node has n integers which is the cost from that node to the pivot node.
		 * They are at around the same place in the array for simplicity and for cache locality.
		 *
		 * cost(nodeIndex, pivotIndex) = costs[nodeIndex*pivotCount+pivotIndex]
		 */
		uint[] costs = new uint[8];
		int maxNodeIndex;

		int pivotCount;

		GraphNode[] pivots;

		/*
		 * Seed for random number generator.
		 * Must not be zero
		 */
		uint ra = 12820163;

		/*
		 * Seed for random number generator.
		 * Must not be zero
		 */
		uint rc = 1140671485;

		/*
		 * Parameter for random number generator.
		 */
		uint rval = 0;

		System.Object lockObj = new object();

		/** Simple linear congruential generator.
		 * \see http://en.wikipedia.org/wiki/Linear_congruential_generator
		 */
		uint GetRandom() {
			rval = (ra*rval + rc);
			return rval;
		}

		void EnsureCapacity ( int index ) {
			if ( index > maxNodeIndex ) {
				lock (lockObj) {
					if ( index > maxNodeIndex ) {
						if ( index >= costs.Length ) {
							var newCosts = new uint[System.Math.Max (index*2, pivots.Length*2)];
							for ( int i = 0; i < costs.Length; i++ ) newCosts[i] = costs[i];
							costs = newCosts;
						}
						maxNodeIndex = index;
					}
				}
			}
		}

		public uint GetHeuristic ( int nodeIndex1, int nodeIndex2 ) {
			nodeIndex1 *= pivotCount;
			nodeIndex2 *= pivotCount;

			if ( nodeIndex1 >= costs.Length || nodeIndex2 >= costs.Length ) {
				EnsureCapacity (nodeIndex1 > nodeIndex2 ? nodeIndex1 : nodeIndex2);
			}

			uint mx = 0;
			for ( int i = 0; i < pivotCount; i++ ) {
				uint d = (uint)System.Math.Abs ((int)costs[nodeIndex1+i] - (int)costs[nodeIndex2+i]);
				if ( d > mx ) mx = d;
			}

			return mx;
		}

		void GetClosestWalkableNodesToChildrenRecursively ( Transform tr, List<GraphNode> nodes ) {
			foreach (Transform ch in tr ) {

				NNInfo info = AstarPath.active.GetNearest ( ch.position, NNConstraint.Default );
				if ( info.node != null && info.node.Walkable ) {
					nodes.Add ( info.node );
				}

				GetClosestWalkableNodesToChildrenRecursively ( tr, nodes );
			}
		}

		public void RecalculatePivots () {
			if ( mode == HeuristicOptimizationMode.None ) {
				pivotCount = 0;
				pivots = null;
				return;
			}

			// Reset the random number generator
			rval = (uint)seed;

			var graphs = AstarPath.active.graphs;

			// Get a List<GraphNode> from a pool
			var pivotList = Pathfinding.Util.ListPool<GraphNode>.Claim ();

			if ( mode == HeuristicOptimizationMode.Custom ) {
				if ( pivotPointRoot == null ) throw new System.Exception ("Grid Graph -> heuristicOptimizationMode is HeuristicOptimizationMode.Custom, " +
				                                                                                 "but no 'customHeuristicOptimizationPivotsRoot' is set");


				GetClosestWalkableNodesToChildrenRecursively ( pivotPointRoot, pivotList );

			} else if ( mode == HeuristicOptimizationMode.Random ) {

				int n = 0;

				for ( int j = 0; j < graphs.Length; j++ ) {
					/*var ig = graphs[j] as IEuclideanEmbeddingGraph;
					if ( ig != null ) {
						ig.CalculateEmbeddingPivots ( this, pivotList );
					}*/

					/**
					  * Here we select N random nodes from a stream of nodes.
					  * Probability of choosing the first N nodes is 1
					  * Probability of choosing node i is min(N/i,1)
					  * A selected node will replace a random node of the previously
					  * selected ones.
					  */
					graphs[j].GetNodes (delegate (GraphNode node) {
						if ( !node.Destroyed && node.Walkable ) {


							n++;
							if ( (GetRandom() % n) < spreadOutCount ) {
								if ( pivotList.Count < spreadOutCount ) {
									pivotList.Add ( node );
								} else {
									pivotList[(int)(GetRandom()%pivotList.Count)] = node;
								}
							}
						}
						return true;
					});
				}
			} else if ( mode == HeuristicOptimizationMode.RandomSpreadOut ) {

				GraphNode first = null;

				if ( pivotPointRoot != null ) {
					GetClosestWalkableNodesToChildrenRecursively ( pivotPointRoot, pivotList );
				} else {
					// Find any node in the graphs
					for ( int j = 0; j < graphs.Length; j++ ) {
						graphs[j].GetNodes ( delegate ( GraphNode node ) {
							if ( node != null && node.Walkable ) {
								first = node;
								return false;
							}
							return true;
						});
					}

					if ( first != null ) {
						pivotList.Add (first);
					} else {
						Debug.LogError ("Could not find any walkable node in any of the graphs.");
						Pathfinding.Util.ListPool<GraphNode>.Release ( pivotList );
						return;
					}
				}

				for ( int i = 0; i < spreadOutCount; i++ ) pivotList.Add (null);

			} else {
				throw new System.Exception ("Invalid HeuristicOptimizationMode: " + mode );
			}

			pivots = pivotList.ToArray();

			Pathfinding.Util.ListPool<GraphNode>.Release ( pivotList );
		}

		public void RecalculateCosts () {
			if ( pivots == null ) RecalculatePivots ();
			if ( mode == HeuristicOptimizationMode.None ) return;

			pivotCount = 0;

			for ( int i = 0; i < pivots.Length; i++ ) if ( pivots[i] != null && (pivots[i].Destroyed || !pivots[i].Walkable) ) throw new System.Exception ("Invalid pivot nodes (destroyed or unwalkable)");

			if ( mode != HeuristicOptimizationMode.RandomSpreadOut )
				for ( int i = 0; i < pivots.Length; i++ )
					if ( pivots[i] == null )
						throw new System.Exception ("Invalid pivot nodes (null)");

			Debug.Log ("Recalculating costs...");
			pivotCount = pivots.Length;

			System.Action<int> startCostCalculation = null;

			startCostCalculation = delegate ( int k ) {
				GraphNode pivot = pivots[k];

				FloodPath fp = null;
				fp = FloodPath.Construct ( pivot );
				fp.immediateCallback = delegate ( Path _p ) {

					// Handle path pooling
					_p.Claim (this);

					// When paths are calculated on navmesh based graphs
					// the costs are slightly modified to match the actual target and start points
					// instead of the node centers
					// so we have to remove the cost for the first and last connection
					// in each path
					var mn = pivot as MeshNode;
					uint costOffset = 0;
					if ( mn != null && mn.connectionCosts != null ) {
						for ( int i = 0; i < mn.connectionCosts.Length; i++ ) {
							costOffset = System.Math.Max ( costOffset, mn.connectionCosts[i] );
						}
					}


					var graphs = AstarPath.active.graphs;
					// Process graphs in reverse order to raise probability that we encounter large NodeIndex values quicker
					// to avoid resizing the internal array too often
					for ( int j = graphs.Length-1; j >= 0; j-- ) {
						graphs[j].GetNodes (delegate ( GraphNode node ) {
							int idx = node.NodeIndex*pivotCount + k;
							EnsureCapacity ( idx );
							PathNode pn = fp.pathHandler.GetPathNode(node);
							if ( costOffset > 0 ) {
								costs[idx] = pn.pathID == fp.pathID && pn.parent != null ? System.Math.Max (pn.parent.G-costOffset, 0) : 0;
							} else {
								costs[idx] = pn.pathID == fp.pathID ? pn.G : 0;
							}
							return true;
						});
					}

					if ( mode == HeuristicOptimizationMode.RandomSpreadOut && k < pivots.Length-1 ) {
						int best = -1;
						uint bestScore = 0;

						// Actual number of nodes
						int totCount = maxNodeIndex/pivotCount;

						// Loop through all nodes
						for ( int j = 1; j < totCount; j++ ) {

							// Find the minimum distance from the node to all existing pivot points
							uint mx = 1 << 30;
							for ( int p = 0; p <= k; p++ ) mx = System.Math.Min ( mx, costs[j*pivotCount + p] );

							// Pick the node which has the largest minimum distance to the existing pivot points
							// (i.e pick the one furthest away from the existing ones)
							GraphNode node = fp.pathHandler.GetPathNode (j).node;
							if ( (mx > bestScore || best == -1) && node != null && !node.Destroyed && node.Walkable ) {
								best = j;
								bestScore = mx;
							}
						}

						if ( best == -1 ) {
							Debug.LogError ( "Failed generating random pivot points for heuristic optimizations");
							return;
						}

						pivots[k+1] = fp.pathHandler.GetPathNode (best).node;

						Debug.Log ("Found node at " + pivots[k+1].position  + " with score " + bestScore);

						startCostCalculation ( k+1 );
					}

					// Handle path pooling
					_p.Release (this);
				};

				AstarPath.StartPath ( fp, true );
			};

			if ( mode != HeuristicOptimizationMode.RandomSpreadOut ) {
				// All calculated in paralell
				for ( int i = 0; i < pivots.Length; i++ ) {
					startCostCalculation ( i );
				}
			} else {
				// Recursive and serial
				startCostCalculation ( 0 );
			}


			dirty = false;
		}

		public void OnDrawGizmos () {
			if ( pivots != null ) for ( int i = 0; i < pivots.Length; i++ ) {
				Gizmos.color = new Color(159/255.0f,94/255.0f,194/255.0f, 0.8f);

				if ( pivots[i] != null && !pivots[i].Destroyed ) {
					Gizmos.DrawCube ( (Vector3)pivots[i].position, Vector3.one );
				}
			}
		}
	}
}
