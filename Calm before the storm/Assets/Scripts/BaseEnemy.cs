using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Health))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(EnemyMovement))]
public class BaseEnemy : MonoBehaviour
{
    Health healthComponent;
    EnemyMovement movement;

    Transform playerTransform;

    private void Awake()
    {
        healthComponent = GetComponent<Health>();

        movement = GetComponent<EnemyMovement>();

        playerTransform = GameObject.Find("Player").GetComponent<Transform>();
    }

    private void Start()
    {
        ChasePlayer();
    }

    public void ChasePlayer()
    {

    }
}
