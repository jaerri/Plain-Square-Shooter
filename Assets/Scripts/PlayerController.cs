using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float movementSpeed = 1f;
    public float rotationSpeed = 5f;
    public float angleOffset = 90f;
    Vector2 movement;
    Vector3 mousePosition;
    Rigidbody2D rigidbody2d;
    Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
        rigidbody2d = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
    }   

    void FixedUpdate()  
    {
        rigidbody2d.MovePosition(rigidbody2d.position + movement * movementSpeed * Time.fixedDeltaTime);

        Vector3 dir = mousePosition - transform.position;

        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.Euler(0, 0, angle - angleOffset);

        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSpeed * Time.fixedDeltaTime);
    }
}