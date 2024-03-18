using System.Collections;
using System.Collections.Generic;
using UnityEditor.Callbacks;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float movementSpeed = 3; // multiplies target velocity vector by it

    Rigidbody2D playerBody;
    float horizontalValue; // stores value of which key is holding user (i.e. if below 0 then user is keeping left arrow or A)

    void Awake() 
    {
        playerBody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        horizontalValue = Input.GetAxisRaw("Horizontal");
        Move(horizontalValue);
    }

    void FixedUpdate()
    {

    }


    void Move(float dir) 
    {
        var calcDir = dir * movementSpeed * 100 * Time.deltaTime;
        Vector2 targetVelocity = new Vector2(calcDir, playerBody.velocity.y);
        playerBody.velocity = targetVelocity;
    }
}
