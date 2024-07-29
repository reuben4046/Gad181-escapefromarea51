using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToCoverState :  State
{
    public MoveTowardsPlayerState moveTowardsPlayerState;
    public bool behindCover;

    public override State RunCurrentState()
    {
        if (behindCover)
        {
            return moveTowardsPlayerState;
        }
        else
        {
            return this;
        }
    }

}
