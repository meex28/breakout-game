using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Callbacks;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float movementSpeed = 5;
    public float movementSpeedBoost = 2.5f;
    private float currentMovementSpeed;
    private Rigidbody2D playerBody;
    private Animator animator;
    private float horizontalValue;
    private bool isFacingRight = true;
    public float boostDuration = 10f;
    public AudioSource moveSound;

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
            if(!moveSound.isPlaying){
                moveSound.Play();
            }

        } else {
            moveSound.Stop();
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
