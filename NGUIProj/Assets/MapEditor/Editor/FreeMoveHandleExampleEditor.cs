using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(FreeMoveHandleExample)), CanEditMultipleObjects]
public class FreeMoveHandleExampleEditor : Editor
{
    protected virtual void OnSceneGUI()
    {
        FreeMoveHandleExample example = (FreeMoveHandleExample)target;

        float size = HandleUtility.GetHandleSize(example.targetPosition) * 0.1f;
        Vector3 snap = Vector3.one * 0.1f;

        EditorGUI.BeginChangeCheck();
        Vector3 newTargetPosition = Handles.FreeMoveHandle(example.targetPosition, Quaternion.identity, size, snap, Handles.RectangleHandleCap);
        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(example, "Change Look At Target Position");
            example.targetPosition = newTargetPosition;
            example.Update();
        }
    }
}