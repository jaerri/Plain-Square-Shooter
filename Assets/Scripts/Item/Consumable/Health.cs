using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Healing Item", menuName = "Item/Consumable/Health")]
public class Health : Consumable
{
    public override void Reset()
    {
        base.Reset();
        itemStats.Clear();
        itemStats = new List<ItemStat>
        {
            new ItemStat
            {
                name = "HEAL",
                unit = "HP",
                value = 25,
                color = new Color32(30, 255, 30, 250),
            },
        };  
    }
}
