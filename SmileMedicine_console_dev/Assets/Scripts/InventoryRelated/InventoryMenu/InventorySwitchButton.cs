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

        //Debug.Log("Switch button: " + gameObject.name + transform.parent.parent.GetComponent<InventoryCanvasController>().initialButtonIdx + " " + transform.parent.parent.GetComponent<InventoryCanvasController>().initialPanelIdx);

        if (transform.parent.parent.GetComponent<InventoryCanvasController>().initialButtonIdx == transform.GetSiblingIndex()
            && transform.parent.parent.GetComponent<InventoryCanvasController>().initialPanelIdx == transform.parent.GetSiblingIndex())
        {
            //Color temp = GetComponent<Image>().color;
            //temp.a = selectedAlpha;
            //GetComponent<Image>().color = temp;

            GetFocus();

            Debug.Log("initial button: " + gameObject.name);
        }
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

    public override void ButtonClick()
    {
        Debug.Log("Button is Clicked: " + gameObject.name);
    }
}
