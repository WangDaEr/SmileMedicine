using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryGridMainPanel : InventoryMainPanel
{
    //public int currentSelectedIndex;
    //public Color focusColor;
    // Start is called before the first frame update

    void Start()
    {
        currentSelectedIndex = 0;
        //transform.GetChild(0).GetChild(currentSelectedIndex).gameObject.GetComponent<Image>().color = focusColor;

        Debug.Log("setactive$$$$$$$$$$$$$$$$$$" + focusColor);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void ChangeSelectedItem(int input, NewIndexType it)
    {
        int newItemIndex = 0;
        switch (it)
        {
            case NewIndexType.Increment:

                newItemIndex = currentSelectedIndex + input;

                break;

            case NewIndexType.NewIndex:

                newItemIndex = input;

                break;

            default:
                break;
        }

        transform.GetChild(0).GetChild(currentSelectedIndex).gameObject.GetComponent<Image>().color = Color.white;
        transform.GetChild(0).GetChild(newItemIndex).gameObject.GetComponent<Image>().color = focusColor;
        currentSelectedIndex = newItemIndex;
    }
}
