using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

[CustomEditor(typeof(InventoryGridMainPanel))]
public class InventoryGridMainPanelInspector : Editor
{
    private InventoryGridMainPanel igmp;
    private GameObject itemPanel;

    private int itemCount;
    public int ItemCount { get { return itemCount; } set { itemCount = value; igmp.itemCount = value; } }
    private int itemLeft;

    private bool checkItemPanel;
    public bool CheckItemPanel { get { return checkItemPanel; } set { checkItemPanel = value; igmp.checkItemPanel = value; } }

    private bool useGridLayoutGroup;
    public bool UseGridLayoutGroup { get { return useGridLayoutGroup; } set { useGridLayoutGroup = value; } }

    private int gridRowSize;
    public int GridRowSize { get { return gridRowSize; } set { gridRowSize = value; } }

    private int gridColumnSize;
    public int GridColumnSize { get { return gridColumnSize; } set { gridColumnSize = value; igmp.gridColumnSize = value; } }

    private void AnalyzeItemPanel()
    {
        foreach (Transform panel in igmp.transform)
        {
            if (panel.tag == "InventoryChildItemPanel")
            {
                ItemCount = panel.childCount;
                UseGridLayoutGroup = (panel.GetComponent<GridLayoutGroup>() != null);
                if (UseGridLayoutGroup)
                {
                    GridColumnSize = panel.GetComponent<GridLayoutGroup>().constraintCount;
                }
                else
                {
                    GridColumnSize = 1;
                    Debug.Log(igmp.name + igmp.gridColumnSize);
                }
                itemPanel = panel.gameObject;
                break;
            }
        }
    }

    private void OnEnable()
    {
        igmp = (InventoryGridMainPanel)target;
        useGridLayoutGroup = igmp.useGridLayoutGroup;
        checkItemPanel = igmp.checkItemPanel;
        itemCount = igmp.itemCount;
        itemLeft = itemCount;

        if (!CheckItemPanel)
        {
            AnalyzeItemPanel();
        }
    }

    public override void OnInspectorGUI()
    {
        EditorGUILayout.BeginVertical();
        /*
        if (!useGridLayoutGroup && !igmp.setGridLayout)
        {
            for (int i = 0; i < igmp.gridLayout.Count; ++i)
            {
                igmp.gridLayout[i] = EditorGUILayout.IntSlider(new GUIContent("Row " + (i + 1)), igmp.gridLayout[i], 1, 5);
            }

            if (GUILayout.Button(new GUIContent("Add a New Row")))
            {
                igmp.gridLayout.Add(1);
            }
        }
        */

        EditorGUILayout.EndVertical();
    }
}
