using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryWindowButton : InventoryButton
{
    // Start is called before the first frame update
    void Start()
    {
        selectedAlpha = 0.3F;
        unselectedAlpha = 1.0F;
        alphaSwitchDuration = 1.0F;

        if (transform.parent.parent.GetComponent<InventoryCanvasController>().currentButtonIdx == transform.GetSiblingIndex() 
            && transform.parent.parent.GetComponent<InventoryCanvasController>().currentPanelIdx == transform.parent.GetSiblingIndex())
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
    }

    public override void LoseFocus()
    {
        GetComponent<Image>().CrossFadeAlpha(unselectedAlpha, alphaSwitchDuration, false);
    }
}
