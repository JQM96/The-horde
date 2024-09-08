using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private GameObject target;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float speed;
    [SerializeField] private float damage;
    [SerializeField] private float attackRate;

    private bool canAttack;
    private float attackTimer;
    private float nextAttackTime;

    // Start is called before the first frame update
    void Start()
    {
        target = FindObjectOfType<Movement>().gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            Vector2 dir = target.transform.position - transform.position;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;


            if (canAttack == false)
            {
                rb.velocity = Vector2.zero;

                attackTimer += Time.deltaTime;
                if (attackTimer >= nextAttackTime)
                {
                    canAttack = true;
                }
            }
            else 
            {
                dir.Normalize();

                rb.velocity = dir * speed;
            }

            transform.eulerAngles = new Vector3(0, 0, angle);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (canAttack == true)
        {
            Health healthComponent = collision.gameObject.GetComponent<Health>();
            if (healthComponent != null)
            {
                healthComponent.TakeDamage(damage);

                canAttack = false;
                nextAttackTime = 1 / attackRate;
                attackTimer = 0;
            }
        }
    }
}
