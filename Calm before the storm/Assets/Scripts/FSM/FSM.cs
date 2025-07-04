using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum State
{
    Chase,
    Attack,
}

public class FSM : MonoBehaviour //Finite State Machine for the zombies, i'll make a generic one later
{
    private State currentState;
    private BaseEnemy baseEnemyRef;

    private void Start()
    {
        currentState = State.Chase;
    }

    private void Update()
    {
        switch (currentState)
        {
            case State.Chase:
                baseEnemyRef.ChasePlayer();

                if (baseEnemyRef.AttackTransition() == true)
                {
                    currentState = State.Attack;
                }
                break;

            case State.Attack:
                baseEnemyRef.Attack();

                if (baseEnemyRef.ChaseTransition() == true)
                {
                    currentState = State.Chase;
                }
                break;

            default:
                break;
        }
    }

    public void SetBaseEnemyRef(BaseEnemy eRef)
    {
        baseEnemyRef = eRef;
    }

    public void SetState(State newState)
    {
        currentState = newState;
    }
}
