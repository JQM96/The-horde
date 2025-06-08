using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float speed;
    [SerializeField] private float damage;
    [SerializeField] private float attackRate;
    [SerializeField] private Knockback knockback;
    [SerializeField] private float stunTime;
    [SerializeField] private float enemeyRadiusDetection = 3f;
    [SerializeField] private float separationRadius = 0.75f;
    [SerializeField] private int scoreValue;
    [SerializeField] private Spawner lootSpawner;
    [SerializeField] private Health health;
    [SerializeField] private List<AudioClip> deathAudios;

    private GameObject target;
    private bool canAttack;
    private float attackTimer;
    private float nextAttackTime;
    private bool isStunned;
    private float stunnedTimer;

    private Vector2 dir;
    Enemy[] enemies;

    void Start()
    {
        enemies = FindObjectsByType<Enemy>(FindObjectsSortMode.InstanceID);

        target = FindObjectOfType<Movement>().gameObject;

        knockback.OnKnockBack += Knockback_OnKnockBack;
        health.OnHealthReachZero += Health_OnHealthReachZero;

        if (WaveManager.instance != null)
            WaveManager.instance.EnemySpawned();
    }

    private void Health_OnHealthReachZero(object sender, EventArgs e)
    {
        int randomSoundIndex = Random.Range(0, deathAudios.Count);
        AudioManager.PlaySound(deathAudios[randomSoundIndex]);

        if (lootSpawner != null)
            SpawnLoot();
    }

    private void Knockback_OnKnockBack(object sender, EventArgs e)
    {
        Stun();
    }

    void Update()
    {
        if (target != null)
        {
            dir = target.transform.position - transform.position;
            dir.Normalize();

            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

            dir -= SteerSeparation();

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
            Movement movementComponent = collision.gameObject.GetComponent<Movement>();
            if (movementComponent != null && healthComponent != null)
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

    public void SpawnLoot()
    {
        lootSpawner.gameObject.SetActive(true);
        lootSpawner.Spawn();
    }

    public Vector2 SteerSeparation()
    {
        Vector2 direction = Vector2.zero;
        foreach (var e in enemies)
        {
            if (e != null)
            {
                if ((transform.position - e.transform.position).magnitude <= enemeyRadiusDetection)
                {
                    float ratio = Mathf.Clamp01((transform.position - e.transform.position).magnitude / separationRadius);
                    direction -= (Vector2)(ratio * (transform.position - e.transform.position));
                }
            }
        }

        return direction.normalized;
    }

    private void OnDestroy()
    {
        if (ScoreManager.instance != null)
        {
            ScoreManager.instance.AddScore(scoreValue);
            ScoreManager.instance.AddMultiplier(1);
        }

        if (WaveManager.instance != null)
            WaveManager.instance.EnemyKilled();
    }
}
