using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Health))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(EnemyMovement))]
[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(FSM))]
public class BaseEnemy : MonoBehaviour
{
    Health healthComponent;
    EnemyMovement movement;

    Transform playerTransform;

    FSM fsm;

    private void Awake()
    {
        healthComponent = GetComponent<Health>();

        movement = GetComponent<EnemyMovement>();

        playerTransform = GameObject.Find("Player").GetComponent<Transform>();

        fsm = GetComponent<FSM>();
        fsm.SetBaseEnemyRef(this);
    }

    public void ChasePlayer()
    {
        movement.MoveTo(playerTransform);
    }

    public bool AttackTransition()
    {
        return (Vector3.Distance(transform.position, playerTransform.position) <= 1.2f);
    }

    public bool ChaseTransition()
    {
        return (Vector3.Distance(transform.position, playerTransform.position) > 1.2f);
    }

    public void Attack()
    {
        movement.StopMoving();

        //More
    }
}
