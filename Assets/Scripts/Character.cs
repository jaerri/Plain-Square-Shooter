using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public int health;
    public Rigidbody2D rigidbody2d;

    void Start()
    {
        rigidbody2d = gameObject.GetComponent<Rigidbody2D>();
    }
}
