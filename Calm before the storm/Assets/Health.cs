using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float health;

    public void TakeDamage(float damage)
    {
        health -= damage;
    }

    private void Update()
    {
        if (health <= 0)
        {
            Debug.Log(transform.name + " DED");
        }
    }
}
