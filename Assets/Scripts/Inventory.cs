using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public partial class ItemSlotData
{
    public int quantity;
}

public class Inventory : MonoBehaviour
{
    public Dictionary<Type, ItemSlotData> inventoryList = new Dictionary<Type, ItemSlotData>();

    // inventory handler
    public ItemSlotData CreateSlot() 
    {
        return new ItemSlotData {
             quantity = 0
        };
    }
    
    public virtual void Pickup(GameObject itemObject)
    {
        Item itemType = itemObject.GetComponent<Item>();
        
        if (!inventoryList.ContainsKey(itemType.GetType()))
        {
            ItemSlotData itemSlotData = CreateSlot();
            itemSlotData.quantity++;
            inventoryList[itemType.GetType()] = itemSlotData;
        }
        else
        {
            inventoryList[itemType.GetType()].quantity++;
        }

        Destroy(itemObject);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Item>())
        {
            Pickup(collision.gameObject);
        }
    }


    //utility function
    public IEnumerable<Type> GetParentTypes(Type type)
    {
        if (type == null)
        {
            yield break;
        }

        foreach (var i in type.GetInterfaces())
        {
            yield return i;
        }

        var currentBaseType = type.BaseType;
        while (currentBaseType != null)
        {
            yield return currentBaseType;
            currentBaseType = currentBaseType.BaseType;
        }
    }
}

