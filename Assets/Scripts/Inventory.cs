using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlotData
{
    public GameObject slot;
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

    ItemSlotData CreateSlot(GameObject prefab)
    {
        return new ItemSlotData {
             slot = Instantiate(prefab, inventoryUI.transform),
             quantity = 0
        };
    }

    public void UpdateSlot(ItemSlotData itemSlotData, Item itemType)
    {
        Image displayImage = itemSlotData.slot.GetComponent<Image>();
        if (itemType && displayImage)
        {
            displayImage.sprite = itemType.icon;
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
            ItemSlotData itemSlotData = CreateSlot(slotPrefab);
            itemSlotData.quantity++;
            UpdateSlot(itemSlotData, itemType);
            inventoryList[itemType.GetType()] = itemSlotData;
        }
        else
        {
            inventoryList[itemType.GetType()].quantity++;
            UpdateSlot(inventoryList[itemType.GetType()], itemType);
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
