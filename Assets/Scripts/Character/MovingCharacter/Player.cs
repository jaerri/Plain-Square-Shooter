using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MovingCharacter
{
    public float cameraDist = 10f;
    public Sprite mouseCursorSprite;
    public GameObject inventoryUI;

    public static GameObject cursorObject;
    public static GameObject playerObject;

    Camera mainCamera;
    Vector3 mousePosition;
    Vector2 movement;
    

    void Start()
    {
        playerObject = gameObject;
        mainCamera = Camera.main;
        Cursor.visible = false;
        cursorObject = new GameObject(name = "Cursor");
        cursorObject.AddComponent<SpriteRenderer>().sprite = mouseCursorSprite;
    }

    public override void PickupItem(GameObject itemObject)
    {
        DroppedItem itemComponent = itemObject.GetComponent<DroppedItem>();

        inventory.AddItem(itemComponent.item, 1);
        inventoryUI.GetComponent<InventoryUI>().UpdateSlot(itemComponent.item);
        Destroy(itemObject);
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
