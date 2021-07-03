using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : MonoBehaviour
{
    public Sprite icon;
    public string description;

    public virtual void Activate()
    {

    }
}
