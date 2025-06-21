using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Health))]
[RequireComponent(typeof(Rigidbody2D))]
public class BaseEnemy : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] NodeGrid nodeGrid;

    Health healthComponent;
    Rigidbody2D rb;
    Pathfinder pathfinder;

    //THIS IS TEMPORARY ALL THIS WILL BE MOVED TO THE ENEMY MOVEMENT SCRIPT
    List<Vector3> movePoints;
    int mpIndex = 0;
    float waitTime = 1.5f;
    float timer;
    //

    private void Awake()
    {
        healthComponent = GetComponent<Health>();
        rb = GetComponent<Rigidbody2D>();

        if (nodeGrid == null)
            nodeGrid = (NodeGrid)FindObjectOfType(typeof(NodeGrid));

        pathfinder = new Pathfinder(nodeGrid);



        movePoints = new List<Vector3>();
    }

    private void Start()
    {
        movePoints = pathfinder.FindPath(transform.position, target.position);
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= waitTime)
        {
            if (mpIndex < movePoints.Count)
            {
                rb.MovePosition(movePoints[mpIndex]);
                Debug.Log(mpIndex + ": " + movePoints[mpIndex]);

                timer = 0;
                mpIndex++;
            }
        }
    }
}
