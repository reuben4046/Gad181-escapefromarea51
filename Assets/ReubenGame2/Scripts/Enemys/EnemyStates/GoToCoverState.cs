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
            GoToCover();
            return this;
        }
    }


    //PlayerReference
    public Transform target;
    public Camera cam;

    //NavMeshAgent
    public NavMeshAgent agentEnemy;

    //Covers
    [SerializeField] List<GameObject> covers = new List<GameObject>();
    List<Transform> coverPoints = new List<Transform>();

    GameObject currentCover = null;
    Transform currentCoverPoint = null;


    //go to cover
    void GoToCover()
    {
        transform.forward = GetDirectionOfTarget();
        GameObject cover = GetClosestCover();
        Transform destination = GetClosestCoverPoint(cover);

        agentEnemy.SetDestination(destination.position);
        StartCoroutine(WaitTillCoverFound());
    }

    IEnumerator WaitTillCoverFound()
    {
        yield return new WaitUntil(() => currentCoverPoint != null);
        behindCover = true;
    }

    Vector3 GetDirectionOfTarget()
    {
        Vector3 direction = target.position - transform.position;
        direction.Normalize();
        return direction;
    }

    GameObject GetClosestCover()
    {
        GameObject closestCover = null;
        float closestDistance = 100f;
        foreach (GameObject cover in covers)
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

    Transform GetClosestCoverPoint(GameObject cover)
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
            if (distance < closestDistance && distance > 2f)
            {
                closestDistance = distance;
                closestCoverPoint = point;
            }
        }
        coverPoints.Clear();
        currentCoverPoint = closestCoverPoint;
        return closestCoverPoint;
    }
}
