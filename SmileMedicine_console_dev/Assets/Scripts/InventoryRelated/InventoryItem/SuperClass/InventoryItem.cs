using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItem : MonoBehaviour
{
    public InventorySystem.ItemType itemType;

    public Sprite itemSprite;
    public Color itemColor;

    public string itemName;
    public string itemDescription;

    public virtual void ItemPickUp() { }
}
