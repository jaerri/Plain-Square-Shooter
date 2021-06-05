using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MovingCharacter
{ 
    Camera mainCamera;
    Vector3 mousePosition;
    Vector2 movement;
    
    void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        Vector2.ClampMagnitude(movement, movementSpeed);
        mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
    }

    void FixedUpdate()
    {
        Vector3 dir = mousePosition - transform.position;
        MoveCharacter(movement);
        RotateCharacter(dir);
    }
}
