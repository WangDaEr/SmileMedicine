using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryMainPanel : MonoBehaviour
{
    public int currentSelectedIndex;
    public Color focusColor;

    public enum NewIndexType
    {
        Increment,
        NewIndex
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void ChangeSelectedItem(int input, NewIndexType it)
    {

    }
}
