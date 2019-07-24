using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(InventorySwitchButton))]
public class InventorySwitchButtonInspector : Editor
{
    private InventorySwitchButton isb;

    private GameObject bindedPanel;
    public GameObject BindedPanel { get { return bindedPanel; } set { bindedPanel = value; isb.bindedPanel = value; } }

    private void OnEnable()
    {
        isb = (InventorySwitchButton)target;

        bindedPanel = isb.bindedPanel;
    }

    public override void OnInspectorGUI()
    {
        EditorGUILayout.BeginVertical();

        BindedPanel = (GameObject)EditorGUILayout.ObjectField(new GUIContent("Binded Panel"), bindedPanel, typeof(GameObject), true);

        EditorGUILayout.EndVertical();
    }
}
