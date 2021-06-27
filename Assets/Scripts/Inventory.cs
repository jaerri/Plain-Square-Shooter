using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlotData
{
    public GameObject slot;
    public Item itemType;
    public int quantity;
}

public class Inventory : MonoBehaviour
{   
    public GameObject inventoryUI;
    public GameObject slotPrefab;
    public Dictionary<Type, ItemSlotData> inventoryList = new Dictionary<Type, ItemSlotData>();

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            inventoryUI.transform.parent.parent.gameObject.SetActive(!inventoryUI.transform.parent.parent.gameObject.activeSelf);
        }   
    }

    ItemSlotData CreateSlot(GameObject prefab, Item itemType)
    {
        return new ItemSlotData {
             slot = Instantiate(prefab, inventoryUI.transform),
             itemType = itemType,
             quantity = 0
        };
    }

    public void UpdateSlot(ItemSlotData itemSlotData)
    {
        Image displayImage = itemSlotData.slot.GetComponent<Image>();
        if (itemSlotData.itemType && displayImage)
        {
            displayImage.sprite = itemSlotData.itemType.icon;
            displayImage.color = Color.white;
            displayImage.GetComponentInChildren<Text>().text = itemSlotData.quantity.ToString();
        }
        else
        {
            displayImage.sprite = null;
            displayImage.color = Color.clear;
            displayImage.GetComponentInChildren<Text>().text = "0";
        }
    }
    
    public void Pickup(GameObject itemObject)
    {
        Item itemType = itemObject.GetComponent<Item>();
        
        if (!inventoryList.ContainsKey(itemType.GetType()))
        {
            ItemSlotData itemSlotData = CreateSlot(slotPrefab, itemType);
            itemSlotData.quantity++;
            UpdateSlot(itemSlotData);
            inventoryList[itemType.GetType()] = itemSlotData;
        }
        else
        {
            inventoryList[itemType.GetType()].quantity++;
            inventoryList[itemType.GetType()].itemType = itemType;
            UpdateSlot(inventoryList[itemType.GetType()]);
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
}
