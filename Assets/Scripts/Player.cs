using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Callbacks;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float movementSpeed = 5; // multiplies target velocity vector by it
    public float movementSpeedBoost = 2.5f; // adds to currentMovementSpeed when player drinks a energy drink
    private float currentMovementSpeed; // movementSpeed and actual boosts
    private Rigidbody2D playerBody;
    private Animator animator;
    private float horizontalValue; // stores value of which key is holding user (i.e. if below 0 then user is keeping left arrow or A)
    private bool isFacingRight = true;
    public float boostDuration = 10f; // Time for which boost lasts (in seconds)

    void Awake() 
    {
        playerBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        currentMovementSpeed = movementSpeed;
    }

    void FixedUpdate()
    {
        horizontalValue = Input.GetAxisRaw("Horizontal");
        Move(horizontalValue);
    }


    void Move(float dir) 
    {
        var calcDir = dir * currentMovementSpeed * 100 * Time.deltaTime;
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

    public void BoostSpeed()
    {
        currentMovementSpeed += movementSpeedBoost;
        GameObject.FindWithTag("GameManager").GetComponent<TimerDisplay>().AddTimer("Napój energetyczny", boostDuration);
        StartCoroutine(ResetBoost());
    }

    IEnumerator ResetBoost()
    {
        yield return new WaitForSeconds(10f);
        currentMovementSpeed = movementSpeed;
    }
}
