using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[Serializable]
public class ItemSlotData {
    public int amount;
    public Item item;

    public ItemSlotData(Item _item, int _amount)
    {
        amount = _amount;
        item = _item;
    }
}

[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory")]
public class Inventory : ScriptableObject
{
    public List<ItemSlotData> inventoryList = new List<ItemSlotData>();  

    public void AddItem(Item item, int amount)
    {
        bool checkList(ItemSlotData slot)
        {
            return slot.item == item;
        }

        if (!inventoryList.Exists(checkList))
        {
            ItemSlotData itemSlotData = new ItemSlotData(item, amount);
            inventoryList.Add(itemSlotData);
        }
        else
        {
            inventoryList.Find(checkList).amount += amount;
        }
    }
}

