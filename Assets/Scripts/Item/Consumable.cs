using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Consumable : Item
{
    public virtual void Reset()
    {
        itemTypeName = "Consumable";
        itemTypeColor = new Color32(90, 180, 220, 255);
    }
}
