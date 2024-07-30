using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveTowardsPlayerState : State
{
    public ShootingState shootingState;
    public bool canSeePlayer;

    public override State RunCurrentState()
    {
        if(canSeePlayer)
        {
            return shootingState;
        }
        else
        {
            CallMoveTowardsPlayer();
            return this;
        }
    }

    void CallMoveTowardsPlayer()
    {
        base.MoveTowardsPlayer();
        StartCoroutine(WaitThenStopMoving());
        //CheckIfCanSeePlayer();
    }

    IEnumerator WaitThenStopMoving()
    {
        yield return new WaitForSeconds(2f);
        canSeePlayer = true;
    }

    void CheckIfCanSeePlayer()
    {
        Vector3 direction = base.GetDirectionOfTarget();
        if(Physics.Raycast(transform.position, direction, out RaycastHit hit))
        {
            if(hit.collider.tag == "Player")
            {
                canSeePlayer = true;
            }
        }
        //stop moving then set canSeePlayer to true
        agentEnemy.SetDestination(transform.position);
        canSeePlayer = true;        
    }

    protected override void OnStateChanged(State newState)
    {
        base.OnStateChanged(newState);
        canSeePlayer = false;
    }

}
