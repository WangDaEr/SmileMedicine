using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(LayerSwitch))]
public class LayerSwitchInspector : Editor
{
    private LayerSwitch ls;

    private GameObject upArrowPortal;
    public GameObject UpArrowPortal { get { return upArrowPortal; } set { upArrowPortal = value; ls.UpArrowPortal = value; } }

    private GameObject downArrowPortal;
    public GameObject DownArrowPortal { get { return downArrowPortal; } set { downArrowPortal = value; ls.DownArrowPortal = value; } }

    private void OnEnable()
    {
        ls = (LayerSwitch)target;

        UpArrowPortal = ls.UpArrowPortal;
        downArrowPortal = ls.DownArrowPortal;
    }
    public override void OnInspectorGUI()
    {
        EditorGUILayout.BeginVertical();

        UpArrowPortal = (GameObject)EditorGUILayout.ObjectField(new GUIContent("Portal (Up Arrow)"), UpArrowPortal, typeof(GameObject), true);
        DownArrowPortal = (GameObject)EditorGUILayout.ObjectField(new GUIContent("Portal (Down Arrow)"), DownArrowPortal, typeof(GameObject), true);

        EditorGUILayout.EndVertical();
    }
}
