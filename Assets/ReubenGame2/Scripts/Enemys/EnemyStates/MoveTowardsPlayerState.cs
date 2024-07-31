using System.Collections;
using UnityEngine;

public class MoveTowardsPlayerState : State
{
    public ShootingState shootingState;
    public GoToCoverState goToCoverState;
    public bool playerSeen;
    public bool playerNotSeen;

    public override State RunCurrentState()
    {        
        if (playerNotSeen)
        {
            return goToCoverState;
        }
        if (playerSeen)
        {
            return shootingState;
        }

        else
        {
            CallMoveTowardsPlayer();
            return this;
        }
    }

    float backToCoverWaitTime = 1f;
    bool called = false;
    void CallMoveTowardsPlayer()
    {
        base.MoveTowardsPlayer();
        if (called == false)
        {
            StartCoroutine(ContinuousRayCast());
            StartCoroutine(WaitThenGoToCover());
            called = true;
        }
    }

    IEnumerator WaitThenGoToCover()
    {
        Debug.Log("started");
        yield return new WaitForSeconds(backToCoverWaitTime);
        Debug.Log("time Up");
        playerNotSeen = true;
    }

    IEnumerator ContinuousRayCast()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.5f);
            Vector3 direction = base.GetDirectionOfTarget();
            RaycastHit hit;
            Physics.Raycast(transform.position, direction, out hit);
            if (hit.transform == target)
            {
                playerSeen = true;
                Debug.Log("Player Detected");
                agentEnemy.SetDestination(transform.position);
            }
            else
            {
                playerSeen = false;
            }
        }
    }



    protected override void OnStateChanged(State newState)
    {
        base.OnStateChanged(newState);
        playerSeen = false;
        playerNotSeen = false;
    }

}
