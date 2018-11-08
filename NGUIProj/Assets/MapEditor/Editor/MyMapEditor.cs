using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(MyMap))]
//[InitializeOnLoad]
public class MyMapEditor : Editor {
    private static Transform s_transform;

    static Vector3 CurrentHandlePosition;
    static Vector3 OldHandlePosition;
    static RaycastHit hit;
    
    public void OnSceneGUI()
    {
        UpdateHandlePosition();
        UpdateRepaint();
        CheckClickObject();
    }

    void CheckClickObject()
    {
        MyMap mm = target as MyMap;
        serializedObject.Update();
        int controlId = GUIUtility.GetControlID(FocusType.Passive);

        // 如果是鼠标左键被点击同时没有其他特定按键按下的话
        if (Event.current.type == EventType.mouseDown &&
            Event.current.button == 0 &&
            Event.current.alt == false &&
            Event.current.shift == false &&
            Event.current.control == false)
        {
            if (hit.transform != null)
            {
                //EditorGUILayout.PropertyField(serializedObject.FindProperty(""));
                int x, y = 0;
                string[] xy = hit.transform.name.Split(',');
                x = int.Parse(xy[0]);
                y = int.Parse(xy[1]);
                mm.CurrentSelectedCell = mm.m_mapCells[x,y];
            }
        }

        HandleUtility.AddDefaultControl(controlId);
    }

    

     void UpdateHandlePosition()
    {
        if (Event.current == null)
            return;

        Vector2 mousePosition = new Vector2(Event.current.mousePosition.x, Event.current.mousePosition.y);
        Ray ray = HandleUtility.GUIPointToWorldRay(mousePosition);
        
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, 1 << LayerMask.NameToLayer("Map")))
        {
            Vector3 offset = Vector3.zero;

            CurrentHandlePosition.x = Mathf.Floor(hit.point.x - hit.normal.x * 0.001f + offset.x);
            CurrentHandlePosition.y = Mathf.Floor(hit.point.y - hit.normal.y * 0.001f + offset.y);
            CurrentHandlePosition.z = Mathf.Floor(hit.point.z - hit.normal.z * 0.001f + offset.z);
        }
    }

    void UpdateRepaint()
    {
        if (OldHandlePosition != CurrentHandlePosition)
        {
            SceneView.RepaintAll();
            OldHandlePosition = CurrentHandlePosition;
        }
    }

    public override void OnInspectorGUI()
    {
        MyMap mme = target as MyMap;
        s_transform = mme.gameObject.transform;
        serializedObject.Update();

        EditorGUILayout.BeginVertical();
        EditorGUILayout.LabelField("Map Width is: ", EditorStyles.boldLabel);
        float savedLabelWidth = EditorGUIUtility.labelWidth;
        EditorGUIUtility.labelWidth = 80;
        EditorGUI.indentLevel += 2;

        EditorGUI.BeginChangeCheck();
        EditorGUILayout.PropertyField(serializedObject.FindProperty("Width"));
        if (EditorGUI.EndChangeCheck())
        {
            serializedObject.ApplyModifiedProperties();
            mme.RecalculateMapBounds();
        }
        EditorGUI.indentLevel -= 2;
        EditorGUIUtility.labelWidth = savedLabelWidth;
        EditorGUILayout.EndVertical();

        EditorGUILayout.BeginVertical();
        EditorGUILayout.LabelField("Map Height is: ", EditorStyles.boldLabel);
        savedLabelWidth = EditorGUIUtility.labelWidth;
        EditorGUIUtility.labelWidth = 80;
        EditorGUI.indentLevel += 2;

        EditorGUI.BeginChangeCheck();
        EditorGUILayout.PropertyField(serializedObject.FindProperty("Height"));
        if (EditorGUI.EndChangeCheck())
        {
            serializedObject.ApplyModifiedProperties();
            mme.RecalculateMapBounds();
        }
        EditorGUI.indentLevel -= 2;
        EditorGUIUtility.labelWidth = savedLabelWidth;
        EditorGUILayout.EndVertical();

        EditorGUILayout.BeginVertical();
        EditorGUILayout.LabelField("Map Cell size: ", EditorStyles.boldLabel);
        savedLabelWidth = EditorGUIUtility.labelWidth;
        EditorGUIUtility.labelWidth = 100;
        EditorGUI.indentLevel += 2;

        EditorGUI.BeginChangeCheck();
        EditorGUILayout.PropertyField(serializedObject.FindProperty("ShowGrid"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("CellSize"));
        if (EditorGUI.EndChangeCheck())
        {
            serializedObject.ApplyModifiedProperties();
            mme.RecalculateMapBounds();
        }
        EditorGUILayout.EndVertical();

        if (mme.CurrentSelectedCell != null && mme.CurrentSelectedCell.CellObj != null)
        {
            EditorGUILayout.BeginVertical();
            EditorGUILayout.LabelField("Selected Map Cell " + mme.CurrentSelectedCell.CellObj.name, EditorStyles.boldLabel);

            EditorGUI.BeginChangeCheck();
            SerializedProperty cellProperty = serializedObject.FindProperty("CurrentSelectedCell");
            SerializedProperty statusProperty = cellProperty.FindPropertyRelative("Status");

            int index = (int)mme.CurrentSelectedCell.Status;
            mme.CurrentSelectedCell.Status = (MapCellStatus)EditorGUILayout.Popup("格子状态:", index, Enum.GetNames(typeof(MapCellStatus)));
            if (EditorGUI.EndChangeCheck())
            {
                serializedObject.ApplyModifiedProperties();
                mme.UpdateMapCells();
            }
            EditorGUILayout.EndVertical();
        }



        EditorGUILayout.Separator();
        EditorGUILayout.Separator();
        EditorGUILayout.Separator();

        if(mme.Width == 0 || mme.Height == 0)
        {
            EditorGUILayout.BeginVertical();
            EditorGUI.indentLevel -= 2;
            EditorGUILayout.LabelField("Load Map Proto", EditorStyles.boldLabel);
            if (GUILayout.Button("Load Map Proto"))
            {
                if (mme.Width == 0 || mme.Height == 0)
                {

                    string path = EditorUtility.OpenFilePanel("choose map data", "", "bytes");
                    if (path.Length != 0)
                    {
                        FileStream fs = new FileStream(path, FileMode.Open);
                        long size = fs.Length;
                        byte[] array = new byte[size];
                        fs.Read(array, 0, array.Length);
                        fs.Close();
                        MapEditorData mapData = MapEditorData.Parser.ParseFrom(array);
                        float cellW = mapData.CellWidth;
                        float cellH = mapData.CellHeight;
                        mme.CellSize = new Vector2(cellW, cellH);
                        mme.Width = mapData.Width;
                        mme.Height = mapData.Height;
                        mme.RecalculateMapBounds();

                        foreach(MapEditorCellData cell in mapData.MapCells)
                        {
                            mme.m_mapCells[cell.X, cell.Y].Status = cell.Status;
                        }
                        mme.UpdateMapCells();
                        SceneView.RepaintAll();
                    }
                }

            }

            EditorGUILayout.EndVertical();
        }
        else
        {
            EditorGUILayout.BeginVertical();
            EditorGUI.indentLevel -= 2;
            EditorGUILayout.LabelField("Export to proto", EditorStyles.boldLabel);
            if (GUILayout.Button("Export"))
            {
                if (mme.Width == 0 || mme.Height == 0)
                {
                    Debug.Log("the map is empty!!!!");
                }
                else
                {
                    MapEditorData mapEditorData = new MapEditorData();
                    mapEditorData.Width = mme.Width;
                    mapEditorData.Height = mme.Height;
                    mapEditorData.CellWidth = mme.CellSize.x;
                    mapEditorData.CellHeight = mme.CellSize.y;
                    for (int i = 0; i < mme.Width; i++)
                    {
                        for (int j = 0; j < mme.Height; j++)
                        {
                            MapEditorCellData cellData = new MapEditorCellData();
                            cellData.X = mme.m_mapCells[i, j].X;
                            cellData.Y = mme.m_mapCells[i, j].Y;
                            cellData.Status = mme.m_mapCells[i, j].Status;
                            mapEditorData.MapCells.Add(cellData);
                        }
                    }

                    byte[] mapArray = PackCodec.Serialize(mapEditorData);
                    string path = EditorUtility.SaveFilePanel("save map data", "", "mapData.bytes", "bytes");

                    if (path.Length != 0)
                    {
                        FileStream fs = new FileStream(path, FileMode.Create);
                        fs.Write(mapArray, 0, mapArray.Length);
                        fs.Close();
                    }
                }
            }

            EditorGUILayout.EndVertical();
        }
    }
}
