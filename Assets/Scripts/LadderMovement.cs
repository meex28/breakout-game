using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class LadderMovement : MonoBehaviour
{

    private float vertical;
    private float speed = 10f;
    private bool isLadder;
    private bool isClimbing;

    [SerializeField] private Rigidbody2D playerBody;

    void Start()
    {
        
    }

    void Update()
    {
        vertical = Input.GetAxis("Vertical");
        if(isLadder && Mathf.Abs(vertical) > 0f) {
            isClimbing = true;
        }
    }

    private void FixedUpdate() {
        if (isClimbing) {
            playerBody.gravityScale = 0f;
            playerBody.velocity = new Vector2(playerBody.velocity.x, vertical * speed);
        }
        else {
            playerBody.gravityScale = 1f;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.CompareTag("Ladder")) {
            isLadder = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if(collision.CompareTag("Ladder")) {
            isLadder = false;
            isClimbing = false;
        }
    }
}
