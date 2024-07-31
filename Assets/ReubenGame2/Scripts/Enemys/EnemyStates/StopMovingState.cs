using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StopMovingState : BaseEnemyState
{
    public ShootingState shootingState;
    public MoveTowardsPlayerState moveTowardsPlayerState;
    //public GoToCoverState goToCoverState;
    public bool canSeePlayer;
    public bool canNotSeePlayer;
    [SerializeField] StateVariables stateVariables;
    private void OnEnable()
    {

    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }


}

