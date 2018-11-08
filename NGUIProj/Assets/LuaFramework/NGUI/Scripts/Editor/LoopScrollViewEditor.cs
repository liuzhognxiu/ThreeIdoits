
using UnityEditor;
[CanEditMultipleObjects]
[CustomEditor(typeof(LoopScrollView), true)]


public class LoopScrollViewEditor : UIWidgetContainerEditor
{
    LoopScrollView mLoopScrollView;
    void OnEnable()
    {
        mLoopScrollView = target as LoopScrollView;
    }

    public override void OnInspectorGUI()
    {
        EditorGUILayout.LabelField("对象池中缓存个数:");
        mLoopScrollView.MaxShowCount = EditorGUILayout.IntField("MaxCount", mLoopScrollView.MaxShowCount);
        EditorGUILayout.LabelField("每个方向不可见item缓存个数:");
        mLoopScrollView.InvisibleCache = EditorGUILayout.IntField("InvisibleCache", mLoopScrollView.InvisibleCache);
        EditorGUILayout.LabelField("排列方式:");
        mLoopScrollView.arrangeDirection = (LoopScrollView.ArrangeDirection)EditorGUILayout.EnumPopup("ArrangeDirection", mLoopScrollView.arrangeDirection);
        EditorGUILayout.LabelField("起始位置:");
        mLoopScrollView.itemStartPos = EditorGUILayout.Vector3Field("ItemStartPos", mLoopScrollView.itemStartPos);
        EditorGUILayout.LabelField("每个Item之间间隙:");
        mLoopScrollView.gapDis = EditorGUILayout.FloatField("GapDis", mLoopScrollView.gapDis);
        base.DrawDefaultInspector();
    }
}
