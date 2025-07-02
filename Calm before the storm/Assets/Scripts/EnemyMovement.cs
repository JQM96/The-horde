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

    Transform targetRef;
    Pathfinder pathfinderRef;

    float movementRefreshTime = 0.5f;
    float timer;

    int mpIndex = 0;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= movementRefreshTime)
        {
            movePoints = pathfinderRef.FindPath(transform.position, targetRef.position);
            timer = 0;
        }
    }

    private void FixedUpdate()
    {
        if (isMoving == false)
            return;

        //Calculate direction
        Vector3 dir = Vector3.one;
        if (mpIndex < movePoints.Count)
            dir = movePoints[mpIndex] - transform.position;
        else 
            dir = targetRef.position - transform.position;

        dir.Normalize();

        //Calculate angle
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;


        //Move! (and rotate)
        rb.velocity = dir * moveSpeed;
        transform.eulerAngles = new Vector3(0, 0, angle);

        if (mpIndex < movePoints.Count)
        {
            if (Vector3.Distance(transform.position, movePoints[mpIndex]) < 1f)
            {
                mpIndex++;
            }
        }

        if (movePoints.Count <= 0)
        {
            //isMoving = false;
            //rb.velocity = Vector3.zero;
        }
    }

    internal void MoveTowardsTarget(Transform target, Pathfinder pathfinder)
    {
        isMoving = true;
        targetRef = target;
        pathfinderRef = pathfinder;

        movePoints = pathfinder.FindPath(transform.position, target.position);
    }
}
