using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] float maxHealth = 100f;

    private float currentHealth;

    public event EventHandler OnHealthReachZero;
    public event EventHandler OnDamage;

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth < 0)
            currentHealth = 0;

        OnDamage?.Invoke(this, EventArgs.Empty);
    }

    public void Heal(float healAmmount)
    {
        currentHealth += healAmmount;
        if (currentHealth > maxHealth)
            currentHealth = maxHealth;
    }

    private void Update()
    {
        if (currentHealth <= 0)
        {
            OnHealthReachZero?.Invoke(this, EventArgs.Empty);

            Destroy(gameObject);
        }
    }

    public float GetCurrentHealth()
    {
        return currentHealth;
    }
}
