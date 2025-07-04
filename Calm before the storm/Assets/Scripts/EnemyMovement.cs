using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] GameObject body;

    NavMeshAgent agent;
    Knockback knockback;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false; //Dont rotate on Z
        agent.updateUpAxis = false;

        knockback = GetComponent<Knockback>();
    }

    public void MoveTo(Transform target)
    {
        agent.isStopped = false;

        agent.SetDestination(target.position);

        Vector2 dir = target.position - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        dir.Normalize();

        transform.eulerAngles = new Vector3(0, 0, angle);
    }

    public void StopMoving()
    {
        agent.isStopped = true;
    }

    public NavMeshAgent GetAgent()
    {
        return agent;
    }

    public Knockback GetKnockback()
    {
        return knockback;
    }

    public void ApplyKnockBack()
    {
        StopMoving();

        knockback.ApplyKnockBack(2, Vector2.down);
    }
}
