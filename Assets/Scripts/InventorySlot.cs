using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public static bool tooltipOn = false;
    GameObject tooltip;

    void CreateTooltip()
    {
        tooltip = Instantiate(PlayerInventory.tooltipPrefab, transform.parent.parent.parent.parent);
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
        if (tooltipOn)
        {    
            if (Player.cursorObject.activeSelf) Player.cursorObject.SetActive(false);
            tooltip.GetComponent<RectTransform>().position = Input.mousePosition;
        }
        else if (!tooltipOn)
        {
            if (!Player.cursorObject.activeSelf) Player.cursorObject.SetActive(true);
        }
    }
}
