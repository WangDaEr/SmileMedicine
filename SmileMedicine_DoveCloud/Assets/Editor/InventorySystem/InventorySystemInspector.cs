using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(InventorySystem))]
public class InventorySystemInspector : Editor
{
    private InventorySystem IS;

    private void OnEnable()
    {
        IS = (InventorySystem)target;
        
        if (IS.itemPanels.Capacity == 0)
        {
            IS.itemPanels = new List<GameObject>();
            for (int i = 0; i < IS.itemTypeNum; ++i)
            {
                IS.itemPanels.Add(null);
            }

           Debug.Log("panels initialized " + IS.itemPanels.Count); 
        }
    }

    public override void OnInspectorGUI()
    {
        EditorGUILayout.BeginVertical();

        for (int i = 0; i < IS.itemPanels.Count; ++i)
        {
            IS.itemPanels[i] = (GameObject)EditorGUILayout.ObjectField
            (
                new GUIContent(Enum.GetNames(typeof(InventorySystem.ItemType))[i] + "item panel"),
                IS.itemPanels[i],
                typeof(GameObject),
                true
            );
        }

        EditorGUILayout.EndVertical();
    }
}
