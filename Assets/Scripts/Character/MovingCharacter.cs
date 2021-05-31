using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingCharacter : Character
{
    public float movementSpeed = 1f;
    public float rotationSpeed = 5f;

    public void MoveCharacter(Vector2 movement)
    {
        rigidbody2d.MovePosition(rigidbody2d.position + movement * movementSpeed * Time.fixedDeltaTime);
    }
    public void RotateCharacter(Vector3 dir)
    {
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.Euler(0, 0, angle - 90f);

        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSpeed * Time.fixedDeltaTime);
    }
}
