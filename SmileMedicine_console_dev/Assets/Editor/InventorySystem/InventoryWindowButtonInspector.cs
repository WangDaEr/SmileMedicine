using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(InventoryWindowButton))]
public class InventoryWindowButtonInspector : Editor
{
    // Start is called before the first frame update
    private InventoryWindowButton iwb;

    private GameObject bindedPanel;
    public GameObject BindedPanel { get { return bindedPanel; } set { bindedPanel = value; iwb.bindedPanel = value; } }

    private void OnEnable()
    {
        iwb = (InventoryWindowButton)target;

        bindedPanel = iwb.bindedPanel;
    }

    public override void OnInspectorGUI()
    {
        EditorGUILayout.BeginVertical();

        BindedPanel = (GameObject)EditorGUILayout.ObjectField(new GUIContent("Item Panel"), bindedPanel, typeof(GameObject), true);

        EditorGUILayout.EndVertical();
    }
}
