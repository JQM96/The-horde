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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Health healthComponent = collision.gameObject.GetComponent<Health>();
        if (healthComponent != null)
        {
            healthComponent.TakeDamage(damage);

            if (pierce == false)
            {
                Destroy(gameObject);
            }
        }

        Knockback knockBackComponent = collision.gameObject.GetComponent<Knockback>();
        if (knockBackComponent != null)
        {
            Vector2 direction = (gameObject.transform.position - collision.gameObject.transform.position) * -1;

            knockBackComponent.ApplyKnockBack(damage, direction);
        }
    }

    public void SetBulletProperties(float newSpeed, float newDamage, bool isPierce)
    {
        speed = newSpeed;
        damage = newDamage;
        pierce = isPierce;
    }
}
