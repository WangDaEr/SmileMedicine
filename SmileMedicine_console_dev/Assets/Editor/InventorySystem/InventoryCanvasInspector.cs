using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(InventoryCanvasController))]
public class InventoryCanvasInspector : Editor
{
    InventoryCanvasController icc;

    private int menuIndex_init;
    private int MenuIndex_init { get { return menuIndex_init; }set { menuIndex_init = value; } }

    private void OnEnable()
    {
        icc = (InventoryCanvasController)target;
        CountButtons();
    }

    private void CountButtons()
    {
        Transform buttons = icc.transform.GetChild(0);
        if (icc.menus.Count <= 0 && buttons != null && buttons.tag == "InventoryButtons")
        {
            icc.menus.Clear();
            for (int i = 0; i < buttons.childCount; ++i)
            {
                icc.menus.Add(new GameObject[2] { buttons.GetChild(i).gameObject, null });
            }
        }
    }

    public override void OnInspectorGUI()
    {
        EditorGUILayout.BeginVertical();

        GUIContent[] buttons = new GUIContent[icc.menus.Count];
        for (int i = 0; i < icc.menus.Count; ++i)
        {
            buttons[i] = new GUIContent(icc.menus[i][(int)InventoryCanvasController.pairIndex.Button].name);
        }
        MenuIndex_init = EditorGUILayout.Popup(new GUIContent("Initial Displaying Button"), menuIndex_init, buttons);

        foreach (GameObject[] pair in icc.menus)
        {
            pair[(int)InventoryCanvasController.pairIndex.Panel] = (GameObject)EditorGUILayout.ObjectField(
                new GUIContent("Panel for " + pair[(int)InventoryCanvasController.pairIndex.Button].name),
                pair[(int)InventoryCanvasController.pairIndex.Panel], 
                typeof(GameObject), 
                true
                );
        }

        EditorGUILayout.EndVertical();
    }
}
