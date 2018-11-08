using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MyMap : MonoBehaviour {

    public bool ShowGrid = true;
    public Vector2 CellSize;

    [HideInInspector]
    public int Width;
    [HideInInspector]
    public int Height;
    [HideInInspector]
    public Vector3 CursorPosition;

    public MyMapCell CurrentSelectedCell = null;

    private int oldWidth;
    private int oldHeight;

    private Bounds m_mapBouds = new Bounds();
    [SerializeField]
    public MyMapCell[,] m_mapCells = new MyMapCell[100, 100];
	
    public void OnDrawGizmosSelected()
    {
        if (Selection.activeGameObject == gameObject)
        {
            DoDrawGizmos();
        }
    }

    bool IsInitialized()
    {
        bool ret = false;

        if (Width != 0 && Height != 0 && m_mapBouds.max == Vector3.zero)
            ret = true;

        return ret;
    }
    
    void DoDrawGizmos()
    {
        Rect rBound = new Rect(m_mapBouds.min, m_mapBouds.size);
        HandlesEx.DrawRectWithOutline(transform, rBound, new Color(0, 0, 0, 0), new Color(1, 1, 1, 0.5f));

        if (ShowGrid)
        {
            if (IsInitialized()) { RecalculateMapBounds(); RebuildMapCells(); }
            {

                // draw tile cells
                Gizmos.color = new Color(1f, 1f, 1f, .2f);

                // Horizontal lines
                for (float i = 1; i < Width; i++)
                {
                    Gizmos.DrawLine(
                        this.transform.TransformPoint(new Vector3(m_mapBouds.min.x + i * CellSize.x, m_mapBouds.min.y)),
                        this.transform.TransformPoint(new Vector3(m_mapBouds.min.x + i * CellSize.x, m_mapBouds.max.y))
                        );
                }

                // Vertical lines
                for (float i = 1; i < Height; i++)
                {
                    Gizmos.DrawLine(
                        this.transform.TransformPoint(new Vector3(m_mapBouds.min.x, m_mapBouds.min.y + i * CellSize.y, 0)),
                        this.transform.TransformPoint(new Vector3(m_mapBouds.max.x, m_mapBouds.min.y + i * CellSize.y, 0))
                        );
                }
            }
        }

        RebuildDrawMap();
    }

    public void RecalculateMapBounds()
    {
        if (CellSize == Vector2.zero) return;
        Vector2 minCellPos = Vector2.Scale(Vector2.zero, CellSize);
        Vector2 maxCellPos = Vector2.Scale(new Vector2(Width, Height), CellSize);

        m_mapBouds.min = m_mapBouds.max = Vector2.zero;

        m_mapBouds.Encapsulate(minCellPos);
        m_mapBouds.Encapsulate(maxCellPos);
        
        RebuildMapCells();
    }

    public void UpdateMapCells()
    {
        if (Width > 0 && Height > 0)
        {
            for(int i = 0; i < Width; i++)
            {
                for (int j = 0; j < Height; j++)
                {
                    m_mapCells[i, j].UpdateMapCell();
                }
            }
        }
    }

    public void RebuildMapCells()
    {
        if (Width > 0 && Height > 0)
        {
            if (m_mapCells == null)
            {
                for(int i = Width; i < Width; i++)
                {
                    for(int j = Height; j < Height; j++)
                    {
                        m_mapCells[i, j] = CreateMapCell(i, j);
                    }
                }
            }

            if (oldWidth > Width)
            {
                for (int i = Width; i < oldWidth; i++)
                {
                    for (int j = 0; j < oldHeight; j++)
                    {
                        if (m_mapCells[i, j].CellObj != null)
                            DestroyImmediate(m_mapCells[i, j].CellObj);
                        m_mapCells[i, j] = null;
                    }
                }
            }

            if (oldHeight > Height)
            {
                for(int i = Height; i < oldHeight; i++)
                {
                    for (int j = 0; j < oldWidth; j++)
                    {
                        DestroyImmediate(m_mapCells[j, i].CellObj);
                        m_mapCells[j, i] = null;
                    }
                }
            }

            if (oldWidth < Width)
            {
                for (int i = oldWidth; i < Width; i++)
                {
                    for (int j = 0; j < oldHeight; j++)
                    {
                        m_mapCells[i, j] = CreateMapCell(i, j);
                    }
                }
            }

            if (oldHeight < Height)
            {
                for (int i = oldHeight; i < Height; i++)
                {
                    for (int j = 0; j < Width; j++)
                    {
                        m_mapCells[j, i] = CreateMapCell(j, i);
                    }
                }
            }

            oldWidth = Width;
            oldHeight = Height;
            EditorGUIUtility.PingObject(gameObject);
        }
        else
        {
            for (int i = 0; i < oldWidth; i++)
            {
                for (int j = 0; j < oldHeight; j++)
                {
                    DestroyImmediate(m_mapCells[i, j].CellObj);
                    m_mapCells[i, j] = null;
                }
            }
            oldWidth = Width;
            oldHeight = Height;
            EditorGUIUtility.PingObject(gameObject);
        }
    }

    MyMapCell CreateMapCell(int x, int y)
    {
        MyMapCell cell = null;

        cell = ScriptableObject.CreateInstance<MyMapCell>();// new MyMapCell();
        cell.Status = MapCellStatus.Normal;
        cell.X = x;
        cell.Y = y;
        m_mapCells[x, y] = cell;

        if (m_mapCells[x, y].CellObj == null)
        {
            m_mapCells[x, y].CellObj = new GameObject();
            m_mapCells[x, y].CellObj.layer = LayerMask.NameToLayer("Map");
            m_mapCells[x, y].CellObj.name = x + "," + y;
            m_mapCells[x, y].CellObj.hideFlags = HideFlags.HideAndDontSave;
            MeshFilter filter = m_mapCells[x, y].CellObj.AddComponent<MeshFilter>();
            MeshRenderer render = m_mapCells[x, y].CellObj.AddComponent<MeshRenderer>();
            BoxCollider box = m_mapCells[x, y].CellObj.AddComponent<BoxCollider>();
            box.center = new Vector3(x * CellSize.x + CellSize.x * 0.5f, y * CellSize.y + CellSize.y * 0.5f, 0);
            box.size = new Vector3(CellSize.x, CellSize.y, 0);
            render.sharedMaterial = new Material(Material.shader);
            render.sharedMaterial.mainTexture = m_mapCells[x, y].CellSprite;

        }
        
        return cell;
    }

    private Material m_material;
    public Material Material
    {
        get
        {
            if (m_material == null)
            {
                m_material = new Material(Shader.Find("Sprites/Default"));
            }
            return m_material;
        }

        set
        {
            if (value != null && m_material != value)
            {
                m_material = value;
            }
        }
    }

    void RebuildDrawMap()
    {
        for(int i = 0; i < Width; i++)
        {
            for(int j = 0; j < Height; j++)
            {
                Vector3 pos = this.transform.TransformPoint(new Vector3(m_mapBouds.min.x + i * CellSize.x, m_mapBouds.min.y + j * CellSize.y));
                Rect rect = new Rect(new Vector2(pos.x, pos.y), CellSize);
                if (m_mapCells[i, j] != null)
                {
                    List<Vector3> verts = new List<Vector3>();
                    verts.Add(new Vector3(m_mapBouds.min.x + i * CellSize.x, m_mapBouds.min.y + j * CellSize.y, 0));
                    verts.Add(new Vector3(m_mapBouds.min.x + (i + 1) * CellSize.x, m_mapBouds.min.y + j * CellSize.y, 0));
                    verts.Add(new Vector3(m_mapBouds.min.x + i * CellSize.x, m_mapBouds.min.y + (j + 1) * CellSize.y, 0));
                    verts.Add(new Vector3(m_mapBouds.min.x + (i + 1) * CellSize.x, m_mapBouds.min.y + (j + 1) * CellSize.y, 0));

                    List<int> triangles = new List<int>();
                    triangles.Add(3);
                    triangles.Add(0);
                    triangles.Add(2);
                    triangles.Add(0);
                    triangles.Add(3);
                    triangles.Add(1);
                    Mesh mesh = new Mesh();
                    mesh.hideFlags = HideFlags.DontSave;
                    mesh.vertices = verts.ToArray();
                    mesh.triangles = triangles.ToArray();
                    m_mapCells[i, j].CellObj.GetComponent<MeshFilter>().mesh = mesh;

                    Gizmos.DrawMesh(mesh, Vector3.zero);
                }
            }
        }
    }
}
