using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(SaveAble))]
public class SaveAbleEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        SaveAble saveAble = (SaveAble)target;

        if (GUILayout.Button("Generate Unique Name"))
        {
            Undo.RecordObject(saveAble, "Generate Unique Name");
            saveAble.GenerateUniqueName();
            EditorUtility.SetDirty(saveAble);
        }
    }
}
