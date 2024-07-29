using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
