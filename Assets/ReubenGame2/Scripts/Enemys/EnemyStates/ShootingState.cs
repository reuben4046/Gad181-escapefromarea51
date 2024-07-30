using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ShootingState : State
{
    public GoToCoverState goToCoverState;
    public bool canSeePlayer;
    public bool finishedShooting;

    public override State RunCurrentState()
    {
        if(finishedShooting)
        {
            return goToCoverState;
        }
        else
        {
            CallShooting();
            return this;
        }
    }

    void CallShooting()
    {
        base.ShootAtPlayer();
        StartCoroutine(StopShooting());
    }

    IEnumerator StopShooting()
    {
        yield return new WaitForSeconds(2f);
        finishedShooting = true;
    }
}
