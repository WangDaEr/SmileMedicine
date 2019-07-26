using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryGridMainPanel : InventoryMainPanel
{
    public bool useGridLayoutGroup;

    public int gridRowSize;
    public int gridColumnSize;
    
    // Start is called before the first frame update
    void Start()
    {
        if (useGridLayoutGroup)
        {
            GridLayoutGroup ItemPanelGroup = childPanels[(int)panelIndex["InventoryChildItemPanel"]].GetComponent<GridLayoutGroup>();

            switch (ItemPanelGroup.constraint)
            {
                case GridLayoutGroup.Constraint.FixedColumnCount:

                    gridRowSize = ItemPanelGroup.constraintCount;
                    gridColumnSize = Mathf.CeilToInt(((float)ItemPanelGroup.transform.childCount / (float)gridRowSize));

                    break;

                case GridLayoutGroup.Constraint.FixedRowCount:

                    gridColumnSize = ItemPanelGroup.constraintCount;
                    gridRowSize = Mathf.CeilToInt(((float)ItemPanelGroup.transform.childCount / (float)gridColumnSize));

                    break;

                default:
                    break;
            }
        }

        Debug.Log(name + " GridRowSize: " + gridRowSize + " GridColumnSize: " + gridColumnSize);
    }

    // Update is called once per frame
    void Update()
    {
        if (InputLockAcquired)
        {
            PanelInput();
        }
    }

    protected override void switchItem(bool isHorInput)
    {
        int newItemIdx = currentSelectedIndex;
        int increment = 0;
        if (isHorInput)
        {
            
        }
        else
        {

        }
    }

    protected override void PanelInput()
    {
        if (inputInterpolation[icc.m_input.hor_axis_button] != 0)
        {
            
            return;
        }

        if (inputInterpolation[icc.m_input.ver_axis_button] != 0)
        {

            return;
        }
    }
}
