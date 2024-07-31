using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class MoveTowardsPlayerState : BaseEnemyState
{
    public ShootingState shootingState;
    public GoToCoverState goToCoverState;
    float backToCoverWaitTime = 3f;
    bool called = false;

    [SerializeField] Transform target;
    [SerializeField] NavMeshAgent agentEnemy;

    private void OnEnable()
    {
        CallMoveTowardsPlayer();
    }

    private void OnDisable()
    {
        StopAllCoroutines();
        called = false;
    }

    void CallMoveTowardsPlayer()
    {
        
        if (called == false)
        {
            MoveTowardsPlayer();
            StartCoroutine(ContinuousRayCast());
            StartCoroutine(WaitThenGoToCover());
            called = true;
        }
    }
    //move towards player
    void MoveTowardsPlayer()
    {
        agentEnemy.SetDestination(target.position);
    }

    IEnumerator WaitThenGoToCover()
    {
        Debug.Log("started");
        yield return new WaitForSeconds(backToCoverWaitTime);
        Debug.Log("time Up");
        FPSGameEvents.OnSwitchState?.Invoke(goToCoverState, this.stateManager);
    }

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

    private Vector3 GetDirectionOfTarget()
    {
        Vector3 direction = target.position - transform.position;
        direction.Normalize();
        return direction;
    }
}
