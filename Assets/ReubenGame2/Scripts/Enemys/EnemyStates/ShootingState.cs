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
        if(canSeePlayer || finishedShooting)
        {
            return goToCoverState;
        }
        else
        {
            return this;
        }
    }
}
