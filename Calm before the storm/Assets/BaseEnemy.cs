using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Health))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(EnemyMovement))]
public class BaseEnemy : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] NodeGrid nodeGrid;

    Health healthComponent;
    Pathfinder pathfinder;
    EnemyMovement movement;

    private void Awake()
    {
        healthComponent = GetComponent<Health>();

        if (nodeGrid == null)
            nodeGrid = (NodeGrid)FindObjectOfType(typeof(NodeGrid));

        pathfinder = new Pathfinder(nodeGrid);

        movement = GetComponent<EnemyMovement>();
    }

    private void Start()
    {
        movement.MoveTowardsTarget(target, pathfinder);
    }
}
