using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StopMovingState : State
{
    public ShootingState shootingState;
    public MoveTowardsPlayerState moveTowardsPlayerState;
    //public GoToCoverState goToCoverState;
    public bool canSeePlayer;
    public bool canNotSeePlayer;

    public override State RunCurrentState()
    {
        if(canSeePlayer)
        {
            return shootingState;
            //return goToCoverState;
        }
        else if(canNotSeePlayer)
        {
            return moveTowardsPlayerState;
        }
        else
        {
            CallStopMoving();
            return this;
        }
    }

    void CallStopMoving()
    {
        base.StopMoving();
        Vector3 direction = base.GetDirectionOfTarget();
        if(Physics.Raycast(transform.position, direction, out RaycastHit hit))
        {
            if(hit.collider.tag == "Player")
            {
                canSeePlayer = true;
            }
            else 
            {
                canNotSeePlayer = false;
            }
        }
        canSeePlayer = true;
    }


}

