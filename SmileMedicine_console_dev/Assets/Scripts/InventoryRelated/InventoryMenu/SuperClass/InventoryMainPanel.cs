using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryMainPanel : MonoBehaviour
{
    public int currentSelectedIndex;
    public Color focusColor;

    public InventoryCanvasController icc;

    public enum NewIndexType
    {
        Increment,
        NewIndex
    }

    public virtual void ChangeSelectedItem(int input, NewIndexType it)
    {

    }
}
