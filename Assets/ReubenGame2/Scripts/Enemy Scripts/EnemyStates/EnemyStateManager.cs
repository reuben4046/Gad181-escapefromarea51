using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateManager : MonoBehaviour
{
    [SerializeField] List<BaseEnemyState> states = new List<BaseEnemyState>();
    [SerializeField] BaseEnemyState startState;

    [SerializeField] Animator animator;

    //subscribing and unsubscribing to the switchstate event
    private void Awake()
    {
        FPSGameEvents.OnSwitchState += OnSwitchState;
    }

    private void OnDisable()
    {
        FPSGameEvents.OnSwitchState -= OnSwitchState;
    }


    private void Start()
    {
        //setting all states to inactive and activating the start state
        foreach (BaseEnemyState state in states)
        {
            state.gameObject.SetActive(false);
        }
        startState.gameObject.SetActive(true);

    }

    //switching states
    private void OnSwitchState(BaseEnemyState State, EnemyStateManager enemy)
    {
        //this is to make sure that the state is only changed for this instance of the enemy 
        if (enemy != this)
        {
            return;
        }
        //setting all states to inactive apart from the state received in the event 
        foreach (BaseEnemyState state in states)
        {
            if (state != State)
            {
                state.gameObject.SetActive(false);
            }
            else
            {
                state.gameObject.SetActive(true);
            }
        }

        //setting the animator based on the state using a switch statement
        switch (State)
        {
            case GoToCoverState:
            {
                animator.SetBool("Running", true);
                animator.SetBool("Idle", false);
                animator.SetBool("Shooting", false);
                break;
            }

            case ShootingState:
            {
                animator.SetBool("Running", false);
                animator.SetBool("Idle", false);
                animator.SetBool("Shooting", true);
                break;
            }
            case MoveTowardsPlayerState:
            {
                animator.SetBool("Running", true);
                animator.SetBool("Idle", false);
                animator.SetBool("Shooting", false);
                break;
            }
        }
    }
}
