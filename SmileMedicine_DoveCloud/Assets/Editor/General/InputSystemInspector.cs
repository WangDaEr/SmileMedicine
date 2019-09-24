using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(InputSystem))]
public class InputSystemInspector : Editor
{
    private InputSystem inputSystem;

    private void OnEnable()
    {
        inputSystem = (InputSystem)target;
    }

    public override void OnInspectorGUI()
    {
        EditorGUILayout.BeginVertical();

        inputSystem.useMobileInput = EditorGUILayout.Toggle(new GUIContent("Use Mobile Input"), inputSystem.useMobileInput);

        EditorGUILayout.EndVertical();
    }
}
