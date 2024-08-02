using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class MoveTowardsPlayerState : BaseEnemyState
{
    public ShootingState shootingState;
    public GoToCoverState goToCoverState;
    float backToCoverWaitTime = 3f;

    Transform target;

    [SerializeField] NavMeshAgent agentEnemy;

    void Awake()
    {
        target = GameObject.FindWithTag("Player").transform;
        if (target == null)
        {
            Debug.Log($"target= {target}");
        }
    }

    private void OnEnable()
    {
        CallMoveTowardsPlayer();
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

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

    IEnumerator WaitThenGoToCover()
    {
        yield return new WaitForSeconds(backToCoverWaitTime);
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
