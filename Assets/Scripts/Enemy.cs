using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using Unity.VisualScripting;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Animator))]
public class EnemyAI : MonoBehaviour
{
    public List<Transform> waypoints;
    public int nextWaypointIndex = 0;
    public float movementSpeed = 2;
    private bool isFacingRight = false;

    public GameObject enemyVision;
    private string enemyVisionPrefabPath = "EnemyVision";
    public float visionRange = 2.5f;
    private BoxCollider2D boxCollider;
    private float visionOffset;
    public static event EventHandler<PlayerLostEvent> PlayerLost;

    void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        visionOffset = boxCollider.bounds.size.x / 2f;
    }

    private void Reset()
    {
        Init();
    }

    void Init()
    {
        boxCollider.isTrigger = true;

        GameObject root = new GameObject(name + "_Root");
        root.transform.position = transform.position;
        transform.SetParent(root.transform);

        this.enemyVision = AddEnemyVision(root.transform);
        this.waypoints = AddWaypoints(root.transform);
    }

    private void FixedUpdate()
    {
        MoveToNextPoint();
        SetAnimation();
        IsPlayerInVision();
    }

    void MoveToNextPoint()
    {
        Transform goalPoint = waypoints[nextWaypointIndex];
        var initialLocalScale = transform.localScale;
        var initialScaleX = Math.Abs(initialLocalScale.x);
        isFacingRight = goalPoint.transform.position.x > transform.position.x;
        initialLocalScale.x = isFacingRight ? initialScaleX : -1 * initialScaleX;
        transform.localScale = initialLocalScale;
        transform.position = Vector2.MoveTowards(transform.position, goalPoint.position, movementSpeed * Time.deltaTime);
        if (Vector2.Distance(transform.position, goalPoint.position) < 1f)
        {
            nextWaypointIndex++;
            nextWaypointIndex %= waypoints.Count;
        }
        MoveVision(transform);
    }

    void SetAnimation()
    {
        // TODO: when adding guards stopping it should be changed to activate animation only on move
        GetComponent<Animator>().SetFloat("xVelocity", 1);
    }

    private GameObject AddEnemyVision(Transform root)
    {
        GameObject enemyVisionPrefab = Resources.Load<GameObject>(enemyVisionPrefabPath);
        GameObject createdEnemyVision = null;
        if (enemyVisionPrefab != null)
        {
            createdEnemyVision = Instantiate(enemyVisionPrefab, transform.position, Quaternion.identity);
            createdEnemyVision.transform.SetParent(root);
            createdEnemyVision.transform.localPosition = Vector3.zero;
            createdEnemyVision.layer = LayerMask.NameToLayer("Vision");
        }
        else
        {
            Debug.LogError("Vision field Prefab not found at path: " + enemyVisionPrefabPath);
        }
        return createdEnemyVision;
    }

    private void MoveVision(Transform enemyTransform)
    {
        // set vision range by scaling
        var localScale = enemyVision.transform.localScale;
        localScale.x = visionRange;
        enemyVision.transform.localScale = localScale;

        // move vision
        var visionLength = enemyVision.GetComponent<SpriteRenderer>().bounds.size.x;
        var offset = visionLength / 2f + visionOffset;
        var directedOffset = isFacingRight ? offset : -offset;
        var visionPosition = new Vector3(enemyTransform.position.x + directedOffset, enemyTransform.position.y, enemyTransform.position.z);
        enemyVision.transform.position = visionPosition;

        // rotate vision if needed
        var initialScaleX = Math.Abs(enemyVision.transform.localScale.x);
        var newLocalScale = enemyVision.transform.localScale;
        newLocalScale.x = isFacingRight ? initialScaleX : -1 * initialScaleX;
        enemyVision.transform.localScale = newLocalScale;
    }

    private List<Transform> AddWaypoints(Transform root)
    {
        GameObject waypoints = new GameObject("Waypoints");
        waypoints.transform.SetParent(root);
        waypoints.transform.position = root.position;
        GameObject p1 = new GameObject("Point1");
        p1.transform.SetParent(waypoints.transform);
        p1.transform.position = root.position;
        IconManager.SetIcon(p1, IconManager.Icon.DiamondBlue);
        GameObject p2 = new GameObject("Point2");
        p2.transform.SetParent(waypoints.transform);
        p2.transform.position = root.position;
        IconManager.SetIcon(p2, IconManager.Icon.DiamondBlue);
        return new List<Transform>
        {
            p1.transform,
            p2.transform
        };
    }

private void IsPlayerInVision()
{
    GameObject player = GameObject.FindGameObjectWithTag("Player");
    if (player != null)
    {
        var origin = transform.position + new Vector3(isFacingRight ? visionOffset : -visionOffset, 0, 0);
        var visionLength = enemyVision.GetComponent<SpriteRenderer>().bounds.size.x;
        Vector2 direction = new Vector2(isFacingRight ? 1 : -1, 0);

        int invisibleToEnemy = 1 << LayerMask.NameToLayer("InvisibleToEnemy");
        int seeThrough = 1 << LayerMask.NameToLayer("SeeThrough");
        int items = 1 << LayerMask.NameToLayer("Items");
        int layerMask = invisibleToEnemy | seeThrough | items;
        layerMask = ~layerMask;
        RaycastHit2D hit = Physics2D.Raycast(origin, direction, visionLength, layerMask);
        if (hit.collider != null && hit.collider.CompareTag("Player"))
        {
            InvokePlayerLostEvent();
        }
        Debug.DrawRay(origin, direction * visionLength, Color.red);
    }
    else
    {
        Debug.LogWarning("Player object not found.");
    }
}


    private void OnTriggerEnter2D(Collider2D other)
    {
    if (other.CompareTag("Player"))
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("InvisibleToEnemy"))
        {
            Debug.Log("Player is invincible and cannot be harmed.");
        }
        else
        {
            InvokePlayerLostEvent();
            Debug.Log("Player has touched a guard.");
        }
    }
    }

    private void InvokePlayerLostEvent()
    {
        PlayerLost?.Invoke(this, new PlayerLostEvent("Player is detected by a guard!"));
    }
}