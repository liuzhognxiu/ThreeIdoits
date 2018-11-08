using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class HandlesEx {

    public static void DrawRectWithOutline(Transform transform, Rect rect, Color color, Color colorOutline)
    {
        Vector3[] rectVerts = {
            transform.TransformPoint(new Vector3(rect.x, rect.y, 0)),
            transform.TransformPoint(new Vector3(rect.x + rect.width, rect.y, 0)),
            transform.TransformPoint(new Vector3(rect.x + rect.width, rect.y + rect.height, 0)),
            transform.TransformPoint(new Vector3(rect.x, rect.y + rect.height, 0)) };
        Handles.DrawSolidRectangleWithOutline(rectVerts, color, colorOutline);
    }
}
