using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(InventoryCanvasController))]
public class InventoryCanvasInspector : Editor
{
    private InventoryCanvasController icc;

    private GameObject initialButton;
    public GameObject InitialButton { get { return initialButton; } set { initialButton = value; icc.initialButton = value; } }

    private int initialPanelIdx = 0;
    public int InitialPanelIdx { get { return initialPanelIdx; } set { initialPanelIdx = value; icc.initialPanelIdx = value; } }

    private int initialButtonIdx = 0;
    public int InitialButtonIdx { get { return initialButtonIdx; }set { initialButtonIdx = value; icc.initialButtonIdx = value; } }

    private void OnEnable()
    {
        icc = (InventoryCanvasController)target;

        initialButton = icc.initialButton;
        initialButtonIdx = icc.initialButtonIdx;
        initialPanelIdx = icc.initialPanelIdx;
    }

    private void getButtonIdx()
    {
        if (initialButton != null)
        {
            InitialButtonIdx = initialButton.transform.GetSiblingIndex();
            InitialPanelIdx = initialButton.transform.parent.GetSiblingIndex();

            Debug.Log("button Idx: " + initialButtonIdx + " panel Idx: " + initialPanelIdx);
        }
    }

    public override void OnInspectorGUI()
    {
        EditorGUILayout.BeginVertical();

        InitialButton = (GameObject)EditorGUILayout.ObjectField(new GUIContent("Initial button"), initialButton, typeof(GameObject), true);
        getButtonIdx();

        EditorGUILayout.EndVertical();
    }
}
