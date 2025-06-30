using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum State
{
    Chase,
    Attack,
    KnockedBack
}

public class FSM : MonoBehaviour //Finite State Machine for the zombies
{
    private State currentState;
    private BaseEnemy baseEnemyRef;

    private void Update()
    {
        switch (currentState)
        {
            case State.Chase:
                //baseEnemyRef.
                break;

            case State.Attack:
                break;

            case State.KnockedBack:
                break;

            default:
                break;
        }
    }

    public void SetBaseEnemyRef(BaseEnemy eRef)
    {
        baseEnemyRef = eRef;
    }
}
