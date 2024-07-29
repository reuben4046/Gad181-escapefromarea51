using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopMovingState : State
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
            return this;
        }
    }
    
}

