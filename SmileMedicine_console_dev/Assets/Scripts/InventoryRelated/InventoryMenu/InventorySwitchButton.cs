using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySwitchButton : InventoryButton
{
    // Start is called before the first frame update
    void Start()
    {
        selectedAlpha = 0.3F;
        unselectedAlpha = 1.0F;
        alphaSwitchDuration = 1.0F;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void GetFocus()
    {
        GetComponent<Image>().CrossFadeAlpha(selectedAlpha, alphaSwitchDuration, false);
        //Color temp = new Color(GetComponent<Image>().color);
    }

    public override void LoseFocus()
    {
        GetComponent<Image>().CrossFadeAlpha(unselectedAlpha, alphaSwitchDuration, false);
    }
}
