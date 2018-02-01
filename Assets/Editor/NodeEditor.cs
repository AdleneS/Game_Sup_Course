using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(InstantiateNodes))]
public class NodeEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        InstantiateNodes IN = target as InstantiateNodes;

        if (GUILayout.Button("Instantiate Node"))
        {
            IN.GenerateNodes();
            EditorUtility.SetDirty(target);
        }
    }
}
