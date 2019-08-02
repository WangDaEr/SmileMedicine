using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySystem : MonoBehaviour
{
    private int itemTypeNum = 6;

    public enum ItemType
    {
        OpalShards,
        SandstoneFragments,
        CollectibleRelics,
        CraftableItems,
        FunctionalTools,
        Maps
    }

    public List<List<InventoryItem>> items;

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

    public void AcquireItem(ItemType it, InventoryItem ii)
    {
        items[(int)it].Add(ii);
    }
}
