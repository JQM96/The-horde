using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float speed;
    [SerializeField] private float damage;
    [SerializeField] private float attackRate;
    [SerializeField] private Knockback knockback;
    [SerializeField] private float stunTime;

    private GameObject target;
    private bool canAttack;
    private float attackTimer;
    private float nextAttackTime;
    private bool isStunned;
    private float stunnedTimer;

    void Start()
    {
        target = FindObjectOfType<Movement>().gameObject;
        knockback.OnKnockBack += Knockback_OnKnockBack;
    }

    private void Knockback_OnKnockBack(object sender, EventArgs e)
    {
        Stun();
    }

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

            if (isStunned == false)
            {
                dir.Normalize();

                rb.velocity = dir * speed;
                transform.eulerAngles = new Vector3(0, 0, angle);
            }
            else
            {
                stunnedTimer += Time.deltaTime;
                if (stunnedTimer >= stunTime)
                {
                    isStunned = false;
                }
            }
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

    public void Stun()
    {
        isStunned = true;
        stunnedTimer = 0;
    }
}
