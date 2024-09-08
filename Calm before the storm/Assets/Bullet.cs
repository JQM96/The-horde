using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Collider2D col;

    private float speed;
    private float damage;
    private bool pierce;

    private void Start()
    {
        Vector3 dir = transform.up;

        rb.velocity = dir * speed;

        if (pierce)
        {
            col.isTrigger = true;
        }

        Destroy(gameObject, 3.5f);
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Health healthComponent = collision.gameObject.GetComponent<Health>();
        if (healthComponent != null)
        {
            healthComponent.TakeDamage(damage);
        }
    }

    public void SetBulletProperties(float newSpeed, float newDamage, bool isPierce)
    {
        speed = newSpeed;
        damage = newDamage;
        pierce = isPierce;
    }
}
