using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float speed;

    private void Start()
    {
        Vector3 dir = transform.up;

        rb.velocity = dir * speed;
    }
}
