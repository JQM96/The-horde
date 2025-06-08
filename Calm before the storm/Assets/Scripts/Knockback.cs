using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knockback : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;

    public event EventHandler OnKnockBack;

    public void ApplyKnockBack(float force, Vector2 dir)
    {
        OnKnockBack?.Invoke(this, EventArgs.Empty);

        dir.Normalize();

        rb.velocity = Vector2.zero;
        rb.AddForce(dir * force * 100);
    }
}
