using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody2D rb;

    [Header("Attributes")]
    [SerializeField] private float moveSpeed = 2f;

    private Transform nextPathPoint;
    private int currentPathPointIndex = 0;

    private void Start() {
        nextPathPoint = WaveLevelManager.main.path[currentPathPointIndex];
    }

    private void Update() {
        if(Vector2.Distance(nextPathPoint.position, transform.position) <= 0.1f) {
            currentPathPointIndex++;
            if(currentPathPointIndex >= WaveLevelManager.main.path.Length) {
                Destroy(gameObject);
                return;
            }
            nextPathPoint = WaveLevelManager.main.path[currentPathPointIndex];
        }
    }

    private void FixedUpdate() {
        Vector2 direction = (nextPathPoint.position - transform.position).normalized;
        rb.velocity = direction * moveSpeed;
    }
}
