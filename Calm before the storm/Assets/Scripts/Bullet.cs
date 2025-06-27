using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;

    [SerializeField] private float speed;

    private void Start()
    {
        Vector3 dir = transform.right;

        rb.velocity = dir * speed;

        Destroy(gameObject, 1f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        rb.velocity = Vector3.zero;

        Destroy(gameObject);
    }
}
