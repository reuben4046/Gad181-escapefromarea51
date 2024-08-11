using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class MoveTowardsPlayerState : BaseEnemyState
{
    //next state reference
    public ShootingState shootingState;
    public GoToCoverState goToCoverState;

    //coroutine
    float backToCoverWaitTime = 3f;

    //player target reference
    Transform target;

    //NavMeshAgent
    [SerializeField] NavMeshAgent agentEnemy;

    void Awake()
    {
        target = GameObject.FindWithTag("Player").transform;
        if (target == null)
        {
            Debug.Log($"target= {target}");
        }
    }

    //used like a start function, so it gets called when the state is entered
    private void OnEnable()
    {
        CallMoveTowardsPlayer();
    }

    //makes sure all coroutines are not running
    private void OnDisable()
    {
        StopAllCoroutines();
    }

    //calls the move towards player function and then calls two coroutines whichever one is fufilled first will determine the next state
    void CallMoveTowardsPlayer()
    {
        MoveTowardsPlayer();
        StartCoroutine(ContinuousRayCast());
        StartCoroutine(WaitThenGoToCover());
    }

    //move towards player
    void MoveTowardsPlayer()
    {
        agentEnemy.SetDestination(target.position);
    }

    //waits a certain amount of time then switches to the next state
    IEnumerator WaitThenGoToCover()
    {
        yield return new WaitForSeconds(backToCoverWaitTime);
        FPSGameEvents.OnSwitchState?.Invoke(goToCoverState, this.stateManager);
    }

    //continuously checks if the player is in the shooting range and if so, switches to the shooting state
    IEnumerator ContinuousRayCast()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.5f);
            Vector3 direction = GetDirectionOfTarget();
            RaycastHit hit;
            Physics.Raycast(transform.position, direction, out hit);
            if (hit.transform == target)
            {
                FPSGameEvents.OnSwitchState?.Invoke(shootingState, this.stateManager);
            }
        }
    }

    //gets the direction of the target and returns that direction
    private Vector3 GetDirectionOfTarget()
    {
        Vector3 direction = target.position - transform.position;
        direction.Normalize();
        return direction;
    }
}
