using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] float turnSpeed;

    private List<Vector3> movePoints;
    private bool isMoving;

    Rigidbody2D rb;

    Transform target;

    int mpIndex = 0;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (isMoving == false)
            return;

        //Calculate direction
        Vector3 dir = movePoints[0] - transform.position;
        dir.Normalize();

        //Calculate angle
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        

        //Move! (and rotate)
        rb.velocity = dir * moveSpeed;
        transform.eulerAngles = new Vector3(0, 0, angle);

        if (Vector3.Distance(transform.position, movePoints[0]) < 1f)
            movePoints.RemoveAt(0);

        if (movePoints.Count <= 0)
        {
            isMoving = false;
            rb.velocity = Vector3.zero;
        }
    }

    internal void MoveTowardsTarget(Transform target, Pathfinder pathfinder)
    {
        isMoving = true;
        this.target = target;

        movePoints = pathfinder.FindPath(transform.position, target.position);
    }
}
