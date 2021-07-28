using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Tooltip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject prefab;
    [NonSerialized] public GameObject tooltipObject;

    protected Canvas canvas;
    protected RectTransform canvasRectTrans;
    protected RectTransform rect;

    void Start()
    {
        canvas = gameObject.GetComponentInParent<Canvas>();
        canvasRectTrans = canvas.GetComponent<RectTransform>();
        CreateTooltip();
    }

    public virtual void CreateTooltip()
    {
        tooltipObject = Instantiate(prefab, canvas.transform);
        tooltipObject.SetActive(false);
        tooltipObject.name += "'s tooltip panel";
        rect = tooltipObject.GetComponent<RectTransform>();  
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!tooltipObject) CreateTooltip();
        tooltipObject.SetActive(true);
        Player.cursorOn = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        tooltipObject.SetActive(false);
        Player.cursorOn = false;
    }

    void Update()
    {
        Vector2 tooltipPos = Input.mousePosition;
        float rectSizeX = canvas.scaleFactor * rect.sizeDelta.x;
        float rectSizeY = canvas.scaleFactor * rect.sizeDelta.y;

        if (tooltipPos.x + rectSizeX > canvasRectTrans.rect.width) tooltipPos.x = canvasRectTrans.rect.width - rectSizeX;
        if (tooltipPos.y - rectSizeY < 0) tooltipPos.y = rectSizeY;

        if (Player.cursorOn)
        {
            if (Player.cursorObject.activeSelf) Player.cursorObject.SetActive(false);
            tooltipObject.GetComponent<RectTransform>().position = tooltipPos;
        }
        else if (!Player.cursorOn)
        {
            if (!Player.cursorObject.activeSelf) Player.cursorObject.SetActive(true);
        }
    }
}
