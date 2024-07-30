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
        if (canSeePlayer)
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
        canSeePlayer = true;
    }

    void CheckIfCanSeePlayer()
    {
        Vector3 direction = base.GetDirectionOfTarget();
        if(Physics.Raycast(transform.position, direction, out RaycastHit hit))
        {
            if (hit.collider.tag == "Player")
            {
                agentEnemy.SetDestination(transform.position);
                canSeePlayer = true;
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
            canSeePlayer=false;
        }
    }


    protected override void OnStateChanged(State newState)
    {
        base.OnStateChanged(newState);
        canSeePlayer = false;
    }

}
