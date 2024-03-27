using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

[RequireComponent(typeof(BoxCollider2D))]
public class EnemyAI : MonoBehaviour
{
    public List<Transform> waypoints;
    public int nextPointIndex=0;
    int idChangeValue = 1;
    public float movementSpeed = 2;


    private void Reset()
    {
        Init();
    }

    void Init()
    {
        GetComponent<BoxCollider2D>().isTrigger = true;

        GameObject root = new GameObject(name + "_Root");
        root.transform.position = transform.position;
        transform.SetParent(root.transform);
        GameObject waypoints = new GameObject("Waypoints");
        waypoints.transform.SetParent(root.transform);
        waypoints.transform.position = root.transform.position;

        GameObject p1 = new GameObject("Point1"); 
        p1.transform.SetParent(waypoints.transform);
        p1.transform.position = root.transform.position;
        IconManager.SetIcon(p1, IconManager.Icon.DiamondBlue);
        GameObject p2 = new GameObject("Point2"); 
        p2.transform.SetParent(waypoints.transform);
        p2.transform.position = root.transform.position;
        IconManager.SetIcon(p2, IconManager.Icon.DiamondBlue);

        this.waypoints = new List<Transform>
        {
            p1.transform,
            p2.transform
        };
    }

    private void FixedUpdate()
    {
        MoveToNextPoint();
    }

    void MoveToNextPoint()
    {
        Transform goalPoint = waypoints[nextPointIndex];
        var initialLocalScale = transform.localScale;
        var initialScaleX = Math.Abs(initialLocalScale.x);
        var isFacingRight = goalPoint.transform.position.x > transform.position.x;
        initialLocalScale.x = isFacingRight ? initialScaleX : -1 * initialScaleX; 
        transform.localScale = initialLocalScale;
        transform.position = Vector2.MoveTowards(transform.position,goalPoint.position,movementSpeed*Time.deltaTime);
        if(Vector2.Distance(transform.position, goalPoint.position)<0.9f)
        {
            if (nextPointIndex == waypoints.Count - 1)
                idChangeValue = -1;
            if (nextPointIndex == 0)
                idChangeValue = 1;
            nextPointIndex += idChangeValue;
        }
    }
}