using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryGridMainPanel : MonoBehaviour, IInventoryMainPanel
{
    public int currentSelectedIndex;
    public Color focusColor;
    // Start is called before the first frame update

    void Start()
    {
        currentSelectedIndex = 0;
        transform.GetChild(0).GetChild(currentSelectedIndex).gameObject.GetComponent<Image>().color = focusColor;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeSelectedItem(int newItemIndex)
    {
        transform.GetChild(0).GetChild(currentSelectedIndex).gameObject.GetComponent<Image>().color = Color.white;
        transform.GetChild(0).GetChild(newItemIndex).gameObject.GetComponent<Image>().color = focusColor;
        newItemIndex = currentSelectedIndex;
    }
}
