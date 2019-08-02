using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryCompositeMainPanel : InventoryMainPanel
{
    public List<GameObject> subPanels;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (InputLockAcquired)
        {
            PanelInput();
        }
    }

    protected override void PanelInput()
    {

    }

    protected override void switchItem(int newSelectedIndex)
    {
        
    }
}
