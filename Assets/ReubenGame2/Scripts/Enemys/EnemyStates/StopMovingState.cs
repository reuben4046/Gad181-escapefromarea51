using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StopMovingState : State
{
    public ShootingState shootingState;
    //public GoToCoverState goToCoverState;
    public bool canSeePlayer;

    public override State RunCurrentState()
    {
        if(canSeePlayer)
        {
            return shootingState;
            //return goToCoverState;
        }
        else
        {
            StopMoving();
            return this;
        }
    }

    [SerializeField] NavMeshAgent agentEnemy;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            canSeePlayer = true;
        }
    }
    //stop moving
    void StopMoving()
    {
        agentEnemy.SetDestination(transform.position);
    }

}

