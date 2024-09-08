using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float speed;
    [SerializeField] private float damage;

    private void Start()
    {
        Vector3 dir = transform.up;

        rb.velocity = dir * speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Health healthComponent = collision.gameObject.GetComponent<Health>();
        if (healthComponent != null)
        {
            healthComponent.TakeDamage(damage);

            Destroy(gameObject);
        }
    }
}
