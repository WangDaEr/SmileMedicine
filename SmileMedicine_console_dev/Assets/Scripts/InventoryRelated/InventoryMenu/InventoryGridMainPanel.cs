using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryGridMainPanel : InventoryMainPanel
{
    public bool checkItemPanel;
    public bool useGridLayoutGroup;
    public bool setGridLayout;

    public int gridRowSize;
    public int gridColumnSize;

    public int itemCount;

    public List<int> gridLayout = new List<int>();
    
    // Start is called before the first frame update
    void Start()
    {
        useGridLayoutGroup = true;

        if (useGridLayoutGroup && childPanels[(int)panelIndex["InventoryChildItemPanel"]].GetComponent<GridLayoutGroup>())
        {
            //checkGridLayout();
        }
        switchItem(currentSelectedIndex);
        //Debug.Log(name + " GridRowSize: " + gridRowSize + " GridColumnSize: " + gridColumnSize + " " + useGridLayoutGroup);
    }

    // Update is called once per frame
    void Update()
    {
        if (InputLockAcquired)
        {
            PanelInput();
        }
    }

    private void checkGridLayout()
    {
        gridColumnSize = childPanels[(int)panelIndex["InventoryChildItemPanel"]].GetComponent<GridLayoutGroup>().constraintCount;
        itemCount = childPanels[(int)panelIndex["InventoryChildItemPanel"]].transform.childCount;

        while (itemCount > 0)
        {
            int rowNum = Mathf.Min(itemCount, gridColumnSize);
            gridLayout.Add(rowNum);
            itemCount -= rowNum;
        }

        //Debug.Log("gridColumn: " + gridColumnSize);
    }

    protected override void switchItem(int newSelectedIndex)
    {
        //Debug.Log(name + "change from: " + currentSelectedIndex + "to " + newSelectedIndex);

        childPanels[(int)panelIndex["InventoryChildItemPanel"]].transform.GetChild(currentSelectedIndex).GetComponent<Image>().color = unSelectedColor;
        childPanels[(int)panelIndex["InventoryChildItemPanel"]].transform.GetChild(newSelectedIndex).GetComponent<Image>().color = selectedColor;

        Transform infoPanel = childPanels[(int)panelIndex["InventoryChildInfoPanel"]].transform;
        Sprite itemSprite = null;
        string itemDescription = "";

        if (newSelectedIndex < icc.m_is.items[(int)itemType].Count)
        {
            InventoryItem selectedItem = icc.m_is.items[(int)itemType][newSelectedIndex];
            itemSprite = selectedItem.itemSprite;
            itemDescription = selectedItem.itemDescription;
        }
        
        foreach (Transform info in infoPanel)
        {
            if (info.GetComponent<Text>())
            {
                info.GetComponent<Text>().text = itemDescription;
            }
            else if (info.GetComponent<Image>())
            {
                //set image window;
            }
        }

        currentSelectedIndex = newSelectedIndex;
    }

    protected override void PanelInput()
    {
        if (inputInterpolation[icc.m_input.hor_axis_button] != 0)
        {
            int newIdx = Mathf.Clamp(currentSelectedIndex + inputInterpolation[icc.m_input.hor_axis_button], 0, childPanels[(int)panelIndex["InventoryChildItemPanel"]].transform.childCount - 1);
            switchItem(newIdx);
        }
        else if (inputInterpolation[icc.m_input.ver_axis_button] != 0)
        {
            int newIdx = Mathf.Clamp(currentSelectedIndex + (-inputInterpolation[icc.m_input.ver_axis_button] * gridColumnSize), 0, childPanels[(int)panelIndex["InventoryChildItemPanel"]].transform.childCount - 1);
            switchItem(newIdx);
        }
    }

    public override void AddItemToPanel(InventoryItem ii, int previousCount)
    {
        Transform itemGrid = childPanels[(int)panelIndex["InventoryChildItemPanel"]].transform.GetChild(previousCount - 1);

        if (ii.itemSprite)
        {
            itemGrid.GetComponent<Image>().sprite = ii.itemSprite;
        }
        else
        {
            
        }
    }
}
