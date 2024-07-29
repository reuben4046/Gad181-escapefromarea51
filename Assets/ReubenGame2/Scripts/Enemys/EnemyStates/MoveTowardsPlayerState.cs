using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTowardsPlayerState : State
{
    public StopMovingState stopMovingState;
    public bool canSeePlayer;

    public override State RunCurrentState()
    {
        if(canSeePlayer)
        {
            return stopMovingState;
        }
        else
        {
            return this;
        }
    }

}
