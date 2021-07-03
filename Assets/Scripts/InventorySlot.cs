using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Item itemType;

    public static bool tooltipOn = false;
    GameObject tooltip;

    void CreateTooltip()
    {
        tooltip = Instantiate(PlayerInventory.tooltipPrefab, transform.parent.parent.parent.parent);

        for (int i = 0; i < tooltip.transform.childCount; i++)
        {
            switch (tooltip.transform.GetChild(i).gameObject.name)
            {
                case "CatagoryColorPanel":
                    tooltip.transform.GetChild(i).GetComponent<Image>().color = Color.green;
                    break;
            }
        }
        tooltip.name = gameObject.name + "'s tooltip panel";
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
