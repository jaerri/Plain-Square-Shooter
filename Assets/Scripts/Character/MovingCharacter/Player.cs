using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MovingCharacter
{ 
    Rigidbody2D rigidbody2d;
    Camera mainCamera;
    Vector3 mousePosition;
    Vector2 movement;
    
    void Start()
    {
        mainCamera = Camera.main;
        rigidbody2d = gameObject.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
    }

    void FixedUpdate()
    {
        Vector3 dir = mousePosition - transform.position;
        MoveAndRotateCharacter(rigidbody2d, dir, movement);
    }
}
