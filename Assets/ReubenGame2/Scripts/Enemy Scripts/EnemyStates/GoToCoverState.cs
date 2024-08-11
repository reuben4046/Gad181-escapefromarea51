using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class GoToCoverState : BaseEnemyState
{
    //next state reference
    public MoveTowardsPlayerState moveTowardsPlayerState;

    //NavMeshAgent
    [SerializeField] NavMeshAgent agentEnemy;

    Transform target;

    //Covers
    [SerializeField] List<CoverRP> covers = new List<CoverRP>();
    List<Transform> coverPoints = new List<Transform>();

    Transform currentCoverPoint = null;

    GameObject coverHolder;

    //coroutines 
    float coverFindTime = 3f;

    void Awake()
    {
        target = GameObject.FindWithTag("Player")?.transform;
    }

    //adds all the covers in the game to the list
    void AddCoversToList()
    {
        coverHolder = GameObject.Find("Covers");
        for (int i = 0; i < coverHolder.transform.childCount; i++)
        {
            GameObject child = coverHolder.transform.GetChild(i).gameObject;
            CoverRP cover = child.GetComponent<CoverRP>();
            covers.Add(cover);
        }
    }

    //used like a start function, so it gets called when the state is entered
    private void OnEnable()
    {
        AddCoversToList();
        GoToCover();
        StartCoroutine(WaitTillCoverFound());
    }

    //makes sure all coroutines are not running
    private void OnDisable()
    {
        StopAllCoroutines();
        covers.Clear();
    }

    //waits for coverFindTime seconds and then switches states
    IEnumerator WaitTillCoverFound()
    {
        yield return new WaitForSeconds(coverFindTime);
        FPSGameEvents.OnSwitchState?.Invoke(moveTowardsPlayerState, this.stateManager);
    }

    //go to cover
    protected void GoToCover()
    {
        CoverRP cover = GetClosestCover();
        if (cover == null)
        {
            Debug.Log($"cover= {cover}");
            return;
        }
        Transform destination = GetClosestCoverPoint(cover);
        if (agentEnemy != null && destination != null)
        {
            agentEnemy.SetDestination(destination.position);
        }
    }

    //gets the direction of the target and returns that direction
    protected Vector3 GetDirectionOfTarget()
    {
        Vector3 direction = target.position - transform.position;
        direction.Normalize();
        return direction;
    }

    protected CoverRP GetClosestCover()
    {
        //sets closest cover to null so that it can be found later in the function. 
        CoverRP closestCover = null;
        //sets closest distance to a large number 
        float closestDistance = 100f;
        //loops through the list of covers
        foreach (CoverRP cover in covers)
        {
            float distance = Vector3.Distance(cover.transform.position, transform.position);
            if (distance < closestDistance && distance > 5f)
            {
                closestDistance = distance;
                closestCover = cover;
            } //setting the cover with the shortest distance as the closest cover every time it loops
        }
        return closestCover;
    }

    protected Transform GetClosestCoverPoint(CoverRP cover)
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