using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : ScriptableObject
{
    [Serializable]
    public class ItemStat
    {
        public string name;
        public string unit;
        public int value;
        public Color32 color = new Color32(0, 0, 0, 0);
    }

    public Sprite icon;
    public Color itemTypeColor = new Color32(255, 255, 255, 255);
    [TextArea(15, 20)]
    public string description;
    public string itemTypeName = "Consumable";
    public List<ItemStat> itemStats = new List<ItemStat>();

    public virtual void Activate()
    {
        
    }
}