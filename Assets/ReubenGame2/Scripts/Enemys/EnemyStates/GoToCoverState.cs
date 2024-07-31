using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GoToCoverState : BaseEnemyState
{
    //next state reference
    public MoveTowardsPlayerState moveTowardsPlayerState;

    //NavMeshAgent
    [SerializeField] NavMeshAgent agentEnemy;
    [SerializeField] Transform target;

    //Covers
    [SerializeField] List<CoverToList> covers = new List<CoverToList>();
    List<Transform> coverPoints = new List<Transform>();

    CoverToList currentCover = null;
    Transform currentCoverPoint = null;

    //used like a start function, so it gets called when the state is entered
    private void OnEnable()
    {
        CallGoToCover();
    }
    //makes sure all coroutines are not running
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
        CoverToList cover = GetClosestCover();
        if (cover != null)
        {
            Transform destination = GetClosestCoverPoint(cover);
            if (agentEnemy != null && destination != null)
            {
                agentEnemy.SetDestination(destination.position);
            }
            else 
            { 
                Debug.Log($"agent ={agentEnemy} destination={destination}"); 
            }
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
        //sets closest cover to null so that it can be found later in the function. 
        CoverToList closestCover = null;
        //sets closest distance to a large number 
        float closestDistance = 100f;
        //loops through the list of covers
        foreach (CoverToList cover in covers)
        {
            if (cover != null)
            {
                float distance = Vector3.Distance(cover.transform.position, transform.position);
                if (distance < closestDistance && distance > 5f)
                {
                    closestDistance = distance;
                    closestCover = cover;
                } //setting the cover with the shortest distance as the closest cover every time it loops
            }
        }
        currentCover = closestCover;
        return closestCover;
    }

    protected Transform GetClosestCoverPoint(CoverToList cover)
    {
        Transform closestCoverPoint = null;
        //adding the coverpoints on the cover found in the previous function to a list
        foreach (Transform point in cover.transform)
        {
            coverPoints.Add(point);
        }
        float closestDistance = 50f;
        foreach (Transform point in coverPoints)
        {
            if (point == currentCoverPoint)
            {
                //skips the current coverpoint
                continue;
            }
            float distance = Vector3.Distance(point.position, transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestCoverPoint = point;
            }
        }
        coverPoints.Clear(); //clears the list so it is ready for next time this state is called
        currentCoverPoint = closestCoverPoint;
        return closestCoverPoint;
    }


}