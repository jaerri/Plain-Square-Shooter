using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MovingCharacter
{
    public float cameraDist = 10f;
    public Sprite mouseCursor;

    public static GameObject cursorObject;
    public static GameObject playerObject;
    public static PlayerInventory inventory;

    Camera mainCamera;
    Vector3 mousePosition;
    Vector2 movement;
    

    void Start()
    {
        playerObject = gameObject;
        inventory = gameObject.GetComponent<PlayerInventory>();
        mainCamera = Camera.main;
        Cursor.visible = false;
        cursorObject = new GameObject(name = "Cursor");
        cursorObject.AddComponent<SpriteRenderer>().sprite = mouseCursor;
    }

    void Update()
    {
        cursorObject.transform.position = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mainCamera.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - cameraDist);

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
