using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : ScriptableObject
{
    public Sprite icon;
    [TextArea(15, 20)]
    public string description;
    public string itemTypeName = "Consumable";
    public Color itemTypeColor = new Color32(255, 255, 255, 255);

    public virtual void Activate()
    {

    }
}