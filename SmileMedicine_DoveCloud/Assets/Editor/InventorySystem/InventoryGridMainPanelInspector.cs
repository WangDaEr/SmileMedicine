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

    private InventorySystem.ItemType itemType;
    public InventorySystem.ItemType ItemType { get { return itemType; } set { itemType = value; igmp.itemType = value; } }

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
        itemType = igmp.itemType;

        if (!CheckItemPanel)
        {
            AnalyzeItemPanel();
        }
    }

    public override void OnInspectorGUI()
    {
        EditorGUILayout.BeginVertical();

        ItemType = (InventorySystem.ItemType)EditorGUILayout.EnumPopup(new GUIContent("Item Type for Panel"), itemType);

        EditorGUILayout.EndVertical();
    }
}
