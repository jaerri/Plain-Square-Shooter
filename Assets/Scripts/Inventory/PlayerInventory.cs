using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public partial class ItemSlotData
{
    public GameObject slot;
}

public class PlayerInventory : Inventory
{
    public static GameObject tooltipPrefab;
    public GameObject tooltipPrefabSet;
    public GameObject inventoryUI;
    public GameObject slotPrefab;

    void Awake()
    {
        tooltipPrefab = tooltipPrefabSet;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I)) // for opening and closing inventory panel
        {
            inventoryUI.transform.parent.parent.gameObject.SetActive(!inventoryUI.transform.parent.parent.gameObject.activeSelf);
        }
    }

    public ItemSlotData CreateSlot(GameObject prefab)
    {
        return new ItemSlotData
        {
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

    public override void Pickup(GameObject itemObject)
    {
        Item itemType = itemObject.GetComponent<Item>();

        if (!inventoryList.ContainsKey(itemType.GetType()))
        {
            ItemSlotData itemSlotData = CreateSlot(slotPrefab);
            itemSlotData.slot.name = itemType.name;
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
}
