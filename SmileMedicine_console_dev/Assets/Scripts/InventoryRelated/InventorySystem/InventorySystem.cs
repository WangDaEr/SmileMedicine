using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySystem : MonoBehaviour
{
    public readonly int itemTypeNum = 5;

    public enum ItemType
    {
        OpalShards,
        SandstoneFragments,
        CollectibleRelics,
        CraftableItems,
        Maps
    }

    public List<List<InventoryItem>> items;
    public List<GameObject> itemPanels;

    private void Awake()
    {
        items = new List<List<InventoryItem>>();
        for (int i = 0; i < itemTypeNum; ++i)
        {
            items.Add(new List<InventoryItem>());
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AcquireItem(InventoryItem ii)
    {
        items[(int)ii.itemType].Add(ii);
        itemPanels[(int)ii.itemType].GetComponent<InventoryMainPanel>().AddItemToPanel(new InventoryItem(ii), items[(int)ii.itemType].Count);
    }
}
