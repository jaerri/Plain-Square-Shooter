using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{   
    public GameObject inventoryUI;
    public GameObject slotPrefab;
    public Dictionary<Type, Dictionary<string, dynamic>> inventoryList = new Dictionary<Type, Dictionary<string, dynamic>>();

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            inventoryUI.transform.parent.parent.gameObject.SetActive(!inventoryUI.transform.parent.parent.gameObject.activeSelf);
        }   
    }

    Dictionary<string, dynamic> CreateSlot(GameObject prefab, Item itemType)
    {
        return new Dictionary<string, dynamic> {
            { "Slot", Instantiate(prefab, inventoryUI.transform) },
            { "ItemType", itemType },
            { "Quantity", (int)0 }
        };
    }

    public void UpdateSlot(Dictionary<string, dynamic> itemSlotData)
    {
        Debug.Log(itemSlotData);
        Image displayImage = itemSlotData["Slot"].GetComponent<Image>();
        if (itemSlotData["ItemType"] && displayImage)
        {
            displayImage.sprite = itemSlotData["ItemType"].icon;
            displayImage.color = Color.white;
            displayImage.GetComponentInChildren<Text>().text = itemSlotData["Quantity"].ToString();
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
            Dictionary<string, dynamic> itemSlotData = CreateSlot(slotPrefab, itemType);
            itemSlotData["Quantity"]++;
            UpdateSlot(itemSlotData);
            inventoryList[itemType.GetType()] = itemSlotData;
        }
        else
        {
            inventoryList[itemType.GetType()]["Quantity"]++;
            inventoryList[itemType.GetType()]["ItemType"] = itemType;
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
