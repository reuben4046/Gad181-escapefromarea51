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
            SwitchCover();
            return this;
        }
    }
    
    //switch cover
    void SwitchCover()
    {
        SwitchCoverPoint(currentCover);
        StartCoroutine(WaitTillCoverFound());
    }

    IEnumerator WaitTillCoverFound()
    {
        yield return new WaitUntil(() => currentCoverPoint != null);
        coverpointChanged = true;
    }
    

    protected override Transform SwitchCoverPoint(CoverToList currentCover)
    {
        base.SwitchCoverPoint(currentCover);
        return currentCoverPoint;
    }

    protected override void OnStateChanged(State newState)
    {
        base.OnStateChanged(newState);
        coverpointChanged = false;
    }
}
