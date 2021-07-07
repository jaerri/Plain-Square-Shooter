using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlotUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Item item;
    public GameObject tooltipPrefab;

    public static bool tooltipOn = false;
    GameObject tooltip;

    void CreateTooltip()
    {
        tooltip = Instantiate(tooltipPrefab, transform.parent.parent.parent.parent);

        foreach (Transform child in tooltip.transform.GetComponentsInChildren<Transform>())
        {
            switch (child.name)
            {
                case "CatagoryColor":
                    child.GetComponent<Image>().color = item.itemTypeColor;
                    break;
                case "Icon":
                    child.GetComponent<Image>().sprite = item.icon;
                    break;
                case "ItemName":
                    child.GetComponent<Text>().text = item.name;
                    break;
                case "CatagoryName":
                    child.GetComponent<Text>().text = item.itemTypeName;
                    child.GetComponent<Text>().color = item.itemTypeColor;
                    break;
            }
        }
        tooltip.name = item.name + "'s tooltip panel";
    }

    void Start()
    {
        CreateTooltip();
        tooltip.SetActive(false);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!tooltip) CreateTooltip();
        tooltip.SetActive(true);
        tooltipOn = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        tooltip.SetActive(false);        
        tooltipOn = false;
    }

    void Update()
    {
        Vector2 tooltipPos = Input.mousePosition;

        Canvas canvas = gameObject.GetComponentInParent<Canvas>();
        RectTransform canvasRectTrans = canvas.GetComponent<RectTransform>();
        RectTransform rect = tooltip.GetComponent<RectTransform>();

        float rectSizeX = canvas.scaleFactor * rect.sizeDelta.x;
        float rectSizeY = canvas.scaleFactor * rect.sizeDelta.y;

        if (tooltipPos.x + rectSizeX > canvasRectTrans.rect.width) tooltipPos.x = canvasRectTrans.rect.width - rectSizeX;
        if (tooltipPos.y - rectSizeY < 0) tooltipPos.y = rectSizeY;

        if (tooltipOn)
        {    
            if (Player.cursorObject.activeSelf) Player.cursorObject.SetActive(false); 
            tooltip.GetComponent<RectTransform>().position = tooltipPos;
        }
        else if (!tooltipOn)
        {
            if (!Player.cursorObject.activeSelf) Player.cursorObject.SetActive(true);
        }
    }
}
