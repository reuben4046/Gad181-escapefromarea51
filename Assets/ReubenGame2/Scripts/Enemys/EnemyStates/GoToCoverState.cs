using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GoToCoverState : BaseEnemyState
{
    public MoveTowardsPlayerState moveTowardsPlayerState;

    bool goToCoverStateActive = false;

    [SerializeField] NavMeshAgent agentEnemy;
    [SerializeField] Transform target;

    [SerializeField] List<CoverToList> covers = new List<CoverToList>();
    List<Transform> coverPoints = new List<Transform>();

    CoverToList currentCover = null;
    Transform currentCoverPoint = null;

    private void OnEnable()
    {
        CallGoToCover();
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    void CallGoToCover()
    {
        GoToCover();
        StartCoroutine(WaitTillCoverFound());
    }

    IEnumerator WaitTillCoverFound()
    {
        yield return new WaitForSeconds(3f);
        FPSGameEvents.OnSwitchState?.Invoke(moveTowardsPlayerState, this.stateManager);
    }

    //go to cover
    protected void GoToCover()
    {
        transform.forward = GetDirectionOfTarget();
        CoverToList cover = GetClosestCover();
        if (cover == null)
        {
            Debug.Log("coverNull");
        }

        if (cover != null)
        {
            Transform destination = GetClosestCoverPoint(cover);
            if (agentEnemy != null && destination != null)
            {
                agentEnemy.SetDestination(destination.position);
            }
            else { Debug.Log($"agent ={agentEnemy} destination={destination}"); }
        }

    }

    protected Vector3 GetDirectionOfTarget()
    {
        Vector3 direction = target.position - transform.position;
        direction.Normalize();
        return direction;
    }

    protected CoverToList GetClosestCover()
    {
        CoverToList closestCover = null;
        float closestDistance = 100f;
        foreach (CoverToList cover in covers)
        {
            if (cover != null)
            {
                float distance = Vector3.Distance(cover.transform.position, transform.position);
                if (distance < closestDistance && distance > 5f)
                {
                    closestDistance = distance;
                    closestCover = cover;
                }
            }
        }
        currentCover = closestCover;
        return closestCover;
    }

    protected Transform GetClosestCoverPoint(CoverToList cover)
    {
        Transform closestCoverPoint = null;

        foreach (Transform point in cover.transform)
        {
            coverPoints.Add(point);
        }
        float closestDistance = 50f;
        foreach (Transform point in coverPoints)
        {
            if (point == currentCoverPoint)
            {
                continue;
            }
            float distance = Vector3.Distance(point.position, transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestCoverPoint = point;
            }
        }
        Debug.Log($"closestcover = {closestCoverPoint}");
        coverPoints.Clear();
        currentCoverPoint = closestCoverPoint;
        return closestCoverPoint;
    }


}