using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(InventoryCanvasController))]
public class InventoryCanvasInspector : Editor
{
    private InventoryCanvasController icc;

    private int menuIndex_init;
    private int MenuIndex_init { get { return menuIndex_init; }set { menuIndex_init = value; icc.menuIndex_init = value; } }

    private void OnEnable()
    {
        icc = (InventoryCanvasController)target;
        CountButtons();
    }

    private void CountButtons()
    {
        Transform buttons = icc.transform.GetChild(0);
        if (icc.menus.Count != buttons.childCount)
        {
            Debug.Log("Clearing menus: " + icc.menus.Count + " buttons: " + buttons.childCount);
            icc.menus.Clear();
            for (int i = 0; i < buttons.childCount; ++i)
            {
                icc.menus.Add(null);
            }
        }
    }

    public override void OnInspectorGUI()
    {
        EditorGUILayout.BeginVertical();

        Transform buttons = icc.transform.GetChild(0);
        GUIContent[] buttonsName = new GUIContent[buttons.childCount];
        for (int i = 0; i < icc.menus.Count; ++i)
        {
            buttonsName[i] = new GUIContent(buttons.GetChild(i).gameObject.name);
        }
        MenuIndex_init = EditorGUILayout.Popup(new GUIContent("Initial Displaying Button"), menuIndex_init, buttonsName);

        for (int i = 0; i < icc.menus.Count; ++i)
        {
            icc.menus[i] = (GameObject)EditorGUILayout.ObjectField(
                new GUIContent("Main Panel for " + buttonsName[i].text),
                icc.menus[i],
                typeof(GameObject),
                true
                );
        }

        EditorGUILayout.EndVertical();
    }
}
