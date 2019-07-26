using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(InventoryGridMainPanel))]
public class InventoryGridMainPanelInspector : Editor
{
    private InventoryGridMainPanel igmp;

    private bool useGridLayoutGroup;
    public bool UseGridLayoutGroup
    {
        get { return useGridLayoutGroup; }
        set
        {
            
            if (igmp.childPanels[(int)igmp.panelIndex["InventoryChildItemPanel"]])
            {
                igmp.useGridLayoutGroup = value;
                useGridLayoutGroup = value;
            }
        }
    }

    private int gridRowSize;
    public int GridRowSize
    {
        get { return gridRowSize; }
        set
        {
            gridRowSize = value;
            if (!useGridLayoutGroup)
            {
                igmp.gridRowSize = value;
            }
        }
    }

    private int gridColumnSize;
    public int GridColumnSize
    {
        get { return gridColumnSize; }
        set
        {
            gridColumnSize = value;
            if (!useGridLayoutGroup)
            {
                igmp.gridColumnSize = value;
            }
        }
    }

    private void OnEnable()
    {
        igmp = (InventoryGridMainPanel)target;

        useGridLayoutGroup = igmp.useGridLayoutGroup;

        if (!useGridLayoutGroup)
        {
            gridRowSize = igmp.gridRowSize;
            gridColumnSize = igmp.gridColumnSize;
        }
    }

    public override void OnInspectorGUI()
    {
        EditorGUILayout.BeginVertical();

        if (!useGridLayoutGroup)
        {
            
        }

        EditorGUILayout.EndVertical();
    }
}
