using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryMainPanel : MonoBehaviour
{
    public int currentSelectedIndex = 0;
    public Color selectedColor;
    public Color unSelectedColor;
    public InventorySystem.ItemType itemType;

    public InventoryCanvasController icc;

    protected bool InputLockAcquired;

    public enum NewIndexType
    {
        Increment,
        NewIndex
    }

    public enum ChildPanelType
    {
        ItemPanel,
        InfoPanel,
        LabelPanel
    }

    protected Dictionary<InputSystem.AxisButtomPressed, int> inputInterpolation = new Dictionary<InputSystem.AxisButtomPressed, int>()
    {
        { InputSystem.AxisButtomPressed.Positive, 1},
        { InputSystem.AxisButtomPressed.Negative, -1},
        { InputSystem.AxisButtomPressed.NoInput, 0}
    };

    public Dictionary<string, ChildPanelType> panelIndex = new Dictionary<string, ChildPanelType>()
    {
        { "InventoryChildItemPanel", ChildPanelType.ItemPanel },
        { "InventoryChildInfoPanel", ChildPanelType.InfoPanel },
        { "InventoryChildLabelPanel", ChildPanelType.LabelPanel }
    };

    public List<GameObject> childPanels;

    private void Awake()
    {
        childPanels = new List<GameObject>() { null, null, null };

        //Debug.Log("mainpanel super class panel initialization");

        foreach (Transform panel in transform)
        {
            int curIdx = childPanels.Count;

            try
            {
                curIdx = (int)panelIndex[panel.tag];
                childPanels[curIdx] = panel.gameObject;
            }
            catch (KeyNotFoundException)
            {
                childPanels.Add(panel.gameObject);
            }
        }
    }

    public void ChangeInputLock()
    {
        InputLockAcquired = !InputLockAcquired;
    }

    protected virtual void PanelInput()
    {
    }

    protected virtual void switchItem(int newSelectedIndex)
    {
    }

    public virtual void AddItemToPanel(InventoryItem ii, int previousCount)
    {
    }
}
