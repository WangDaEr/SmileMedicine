﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryButton : MonoBehaviour
{
    public float selectedAlpha;
    public float unselectedAlpha;
    public float alphaSwitchDuration;

    public InventoryCanvasController icc;

    public GameObject bindedPanel;

    public virtual void GetFocus()
    {

    }

    public virtual void LoseFocus()
    {

    }

    public virtual void ButtonClick()
    {

    }

    public virtual void ReturnCanvas()
    {

    }
}
