using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

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
            CallGoToCover();
            return this;
        }
    }
    bool goToCoverStateActive = false;
    void CallGoToCover()
    {
        if (!goToCoverStateActive)
        {
            base.GoToCover();
            StartCoroutine(WaitTillCoverFound());
            goToCoverStateActive = true;
        }
    }

    IEnumerator WaitTillCoverFound()
    {
        yield return new WaitForSeconds(2f);
        behindCover = true;
    }

    protected override void OnStateChanged(State newState)
    {
        base.OnStateChanged(newState);
        behindCover = false;
    }

}