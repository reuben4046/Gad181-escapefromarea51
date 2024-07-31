using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SwitchCoverState : BaseEnemyState
{
    public MoveTowardsPlayerState moveTowardsPlayerState;
    public bool coverpointChanged;
    private void OnEnable()
    {

    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }
}
