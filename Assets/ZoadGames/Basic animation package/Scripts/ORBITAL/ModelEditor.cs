using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;

[CustomEditor(typeof(CharacterSet))]
public class ModelEditor : Editor
{
    CharacterSet tpModelConfig;
    SerializedProperty modelObject;

    void OnEnable()
    {
        tpModelConfig = (CharacterSet)target;
        // Fetch the objects from the GameObject script to display in the inspector
        modelObject = serializedObject.FindProperty("modelo");

    }


    public override void OnInspectorGUI()
    {

        EditorGUILayout.HelpBox("Load your model in the Player Model space and push the button Set Model", MessageType.Info);

        EditorGUILayout.PropertyField(modelObject, new GUIContent("Player Model"), GUILayout.Height(20));
  
        var oldColor = GUI.backgroundColor;
        GUI.backgroundColor = new Color(0.235f, 0.722f, 0.502f);
        if (GUILayout.Button("Set Model"))
        {
            tpModelConfig.SetNewModel();
        }
        GUI.backgroundColor = oldColor;

        // It must be at bottom:
        serializedObject.ApplyModifiedProperties();
    }

}
