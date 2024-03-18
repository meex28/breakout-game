using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Callbacks;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float movementSpeed = 5; // multiplies target velocity vector by it

    private Rigidbody2D playerBody;
    private Animator animator;
    private float horizontalValue; // stores value of which key is holding user (i.e. if below 0 then user is keeping left arrow or A)
    private bool isFacingRight = true;

    void Awake() 
    {
        playerBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
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

        var isPlayerMoving = dir != 0;
        if(isPlayerMoving) {
            isFacingRight = dir > 0;
            var initialLocalScale = transform.localScale;
            var initialScaleX = Math.Abs(initialLocalScale.x);
            initialLocalScale.x = isFacingRight ? initialScaleX : -1 * initialScaleX; 
            transform.localScale = initialLocalScale;
        }

        animator.SetFloat("xVelocity", isPlayerMoving ? 1 : 0);
    }
}
