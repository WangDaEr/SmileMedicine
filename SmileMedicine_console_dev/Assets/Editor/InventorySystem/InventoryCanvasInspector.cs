using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(InventoryCanvasController))]
public class InventoryCanvasInspector : Editor
{
    private InventoryCanvasController icc;

    private GameObject currentButton;

    private int currentPanelIdx = 0;
    public int CurrentPanelIdx { get { return currentPanelIdx; } set { currentPanelIdx = value; icc.currentPanelIdx = value; } }

    private int currentButtonIdx = 0;
    public int CurrentButtonIdx { get { return currentButtonIdx; }set { currentButtonIdx = value; icc.currentButtonIdx = value; } }

    private void OnEnable()
    {
        icc = (InventoryCanvasController)target;
    }

    private void getButtonIdx()
    {
        if (currentButton != null)
        {
            CurrentButtonIdx = currentButton.transform.GetSiblingIndex();
            CurrentPanelIdx = currentButton.transform.parent.GetSiblingIndex();

            Debug.Log("button Idx: " + currentButtonIdx + " panel Idx: " + currentPanelIdx);
        }
    }

    public override void OnInspectorGUI()
    {
        EditorGUILayout.BeginVertical();

        currentButton = (GameObject)EditorGUILayout.ObjectField(new GUIContent("Initial button"), currentButton, typeof(GameObject), true);
        getButtonIdx();

        EditorGUILayout.EndVertical();
    }
}
