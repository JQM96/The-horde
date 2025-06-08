using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float speed;

    [SerializeField] private Health playerHealthComponent;
    [SerializeField] private List<AudioClip> hurtSounds;
    [SerializeField] private AudioClip deathSound;

    private void Start()
    {
        playerHealthComponent.OnDamage += PlayerHealthComponent_OnDamage;
        playerHealthComponent.OnHealthReachZero += PlayerHealthComponent_OnHealthReachZero;
    }

    private void PlayerHealthComponent_OnHealthReachZero(object sender, System.EventArgs e)
    {
        AudioManager.PlaySound(deathSound);
    }

    private void PlayerHealthComponent_OnDamage(object sender, System.EventArgs e)
    {
        int randomIndex = Random.Range(0, hurtSounds.Count);
        AudioManager.PlaySound(hurtSounds[randomIndex], true);
    }

    void Update()
    {
        Vector2 movementVector;

        movementVector.x = Input.GetAxis("Horizontal");
        movementVector.y = Input.GetAxis("Vertical");

        movementVector.Normalize();

        rb.velocity = movementVector * speed;
    }
}
