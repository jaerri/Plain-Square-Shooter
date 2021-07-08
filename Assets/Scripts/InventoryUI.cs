using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    public Inventory inventory;
    public GameObject slotPrefab;
    public Dictionary<Item, GameObject> itemSlots = new Dictionary<Item, GameObject>();

    public void UpdateSlot(Item item)
    {
        GameObject slot;
        List<ItemSlotData> itemList = inventory.inventoryList;
        if (itemSlots.ContainsKey(item)) slot = itemSlots[item];
        else slot = Instantiate(slotPrefab, transform);

        itemSlots[item] = slot;
        slot.name = item.name;
        slot.GetComponent<InventorySlotUI>().item = item;
        
        Image displayImage = slot.GetComponent<Image>();
        if (item && displayImage)
        {
            displayImage.sprite = item.icon;
            displayImage.color = Color.white;
            displayImage.GetComponentInChildren<Text>().text = itemList.Find(slot => slot.item = item).amount.ToString();
        }
        else
        {
            displayImage.sprite = null;
            displayImage.color = Color.clear;
            displayImage.GetComponentInChildren<Text>().text = "0";
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I)) 
        {
            gameObject.SetActive(!gameObject.activeSelf);
        }
    }
}
