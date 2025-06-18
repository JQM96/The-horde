using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Health))]
[RequireComponent(typeof(Rigidbody2D))]
public class BaseEnemy : MonoBehaviour
{
    [SerializeField] Transform playerTransform; //Temporal

    Health healthComponent;
    Rigidbody2D rb;

    private void Awake()
    {
        healthComponent = GetComponent<Health>();
        rb = GetComponent<Rigidbody2D>();
    }
}
