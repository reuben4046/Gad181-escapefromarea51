using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveTowardsPlayerState : State
{
    public ShootingState shootingState;
    public bool canSeeDeezNuts;

    public override State RunCurrentState()
    {
        if (canSeeDeezNuts)
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
        //StartCoroutine(WaitThenStopMoving());
        CheckIfCanSeePlayer();
    }

    IEnumerator WaitThenStopMoving()
    {
        yield return new WaitForSeconds(1f);
        canSeeDeezNuts = true;
    }

    void CheckIfCanSeePlayer()
    {
        Vector3 direction = base.GetDirectionOfTarget();
        if(Physics.Raycast(transform.position, direction, out RaycastHit hit))
        {
            if (hit.collider.tag == "Player")
            {
                agentEnemy.SetDestination(transform.position);
                canSeeDeezNuts = true;
                Debug.Log("Hit");
            }
            else 
            { 

                Debug.Log("notHit"); 
            }
        }
        else
        {
            Debug.Log("saw nothing");
            canSeeDeezNuts = false;
        }
    }


    protected override void OnStateChanged(State newState)
    {
        base.OnStateChanged(newState);
        canSeeDeezNuts = false;
    }

}
