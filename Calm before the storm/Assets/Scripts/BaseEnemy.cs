using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Health))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(EnemyMovement))]
public class BaseEnemy : MonoBehaviour
{
    [SerializeField] NodeGrid nodeGrid;

    Health healthComponent;
    Pathfinder pathfinder;
    EnemyMovement movement;

    Transform playerTransform;

    private void Awake()
    {
        healthComponent = GetComponent<Health>();

        if (nodeGrid == null)
            nodeGrid = (NodeGrid)FindObjectOfType(typeof(NodeGrid));

        pathfinder = new Pathfinder(nodeGrid);

        movement = GetComponent<EnemyMovement>();

        playerTransform = GameObject.Find("Player").GetComponent<Transform>();
    }

    private void Start()
    {
        ChasePlayer();
    }

    public void ChasePlayer()
    {
        movement.MoveTowardsTarget(playerTransform, pathfinder);
    }
}
