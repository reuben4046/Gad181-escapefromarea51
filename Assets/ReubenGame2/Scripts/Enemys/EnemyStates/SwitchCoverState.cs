using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SwitchCoverState : State
{
    public MoveTowardsPlayerState moveTowardsPlayerState;
    public bool coverpointChanged;

    public override State RunCurrentState()
    {
        if (coverpointChanged)
        {
            return moveTowardsPlayerState;
        }
        else
        {
            return this;
        }
    }
}
