using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Consumable Item", menuName = "Item/Consumable")]
public class Consumable : Item
{
    public void Reset()
    {
        itemTypeName = "Consumable";
        itemTypeColor = new Color32(90, 180, 220, 255);
    }
}
