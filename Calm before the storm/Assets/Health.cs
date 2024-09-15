using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float health;

    public event EventHandler OnHealthReachZero;
    public event EventHandler OnDamage;

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health < 0)
            health = 0;

        OnDamage?.Invoke(this, EventArgs.Empty);
    }

    public void Heal(float healAmmount)
    {
        health += healAmmount;
        if (health > 100)
            health = 100;
    }

    private void Update()
    {
        if (health <= 0)
        {
            OnHealthReachZero?.Invoke(this, EventArgs.Empty);

            Destroy(gameObject);
        }
    }

    public float GetCurrentHealth()
    {
        return health;
    }
}
