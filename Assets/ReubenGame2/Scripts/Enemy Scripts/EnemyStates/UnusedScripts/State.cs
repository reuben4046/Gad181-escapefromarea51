using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.AI;
using System;

public class State : MonoBehaviour
{
    //PlayerReference
    [SerializeField] protected Transform target;

    [SerializeField] protected Camera cam;

    //NavMeshAgent
    [SerializeField] protected NavMeshAgent agentEnemy;

    //Gun
    [SerializeField] protected Transform gunTransform;
    protected bool canFire = true;
    protected float fireRate = 0.5f;
    protected float shootingTimer = 0f;

    //Covers
    [SerializeField] protected List<CoverRP> covers = new List<CoverRP>();
    [SerializeField] protected List<Transform> coverPoints = new List<Transform>();

    [SerializeField] protected CoverRP currentCover = null;
    [SerializeField] protected Transform currentCoverPoint = null;


    private void OnEnable()
    {
        //FPSGameEvents.OnCoverStart += OnCoverStart;
        FPSGameEvents.OnSwitchState += OnSwitchState;
    }

    private void OnDisable()
    {
        //FPSGameEvents.OnCoverStart -= OnCoverStart;
        FPSGameEvents.OnSwitchState -= OnSwitchState;
    }

    protected virtual void OnSwitchState(BaseEnemyState newState, EnemyStateManager enemy)
    {
        Debug.Log($"stateChanged to {newState}");
    }

    private void OnCoverStart(CoverRP cover)
    {
        covers.Add(cover);
        Debug.Log(covers.Count);
    }

    void Update()
    {
        DebugRays();
    }



    //go to cover
    protected void GoToCover()
    {
        transform.forward = GetDirectionOfTarget();
        CoverRP cover = GetClosestCover();
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

    protected CoverRP GetClosestCover()
    {
        CoverRP closestCover = null;
        float closestDistance = 100f;
        foreach (CoverRP cover in covers)
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

    protected Transform GetClosestCoverPoint(CoverRP cover)
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
    

    //switch cover
    protected virtual Transform SwitchCoverPoint(CoverRP currentCover)
    {
        if (currentCover == null)
        {
            return null;
        }
        Transform newCoverPoint = GetClosestCoverPoint(currentCover);
        agentEnemy.SetDestination(newCoverPoint.position);
        return newCoverPoint;
    }    


    //move towards player
    protected virtual void MoveTowardsPlayer()
    {
        agentEnemy.SetDestination(target.position);
    }


    //shoot at player
    protected void ShootAtPlayer()
    {
        FireTimer();
        transform.LookAt(target);
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit))
        {
            if (hit.transform == target && canFire)
            {
                ShootBullet();
            }
        }
    }


    protected void FireTimer() 
    {
        if (canFire == false) 
        {
            shootingTimer += Time.deltaTime;
            if (shootingTimer > fireRate) 
            {
                canFire = true;
                shootingTimer = 0f;
            }
        }
    }

    protected void ShootBullet() 
    {
        if (canFire) 
        {
            EnemyBulletRP bullet = GetPooledEnemyBullet(); 
            bullet.transform.position = gunTransform.position;
            bullet.transform.LookAt(target);
            canFire = false;
        }
    }
    
    protected EnemyBulletRP GetPooledEnemyBullet()
    {
        EnemyBulletRP enemyBullet = ObjectPool.instance.GetPooledEnemyBullet();
        if (enemyBullet != null)
        {
            enemyBullet.gameObject.SetActive(true);
            return enemyBullet;
        } 
        else
        {
            return null;
        }
    }


    //stop moving
    protected void StopMoving()
    {
        agentEnemy.SetDestination(transform.position);
    }

    //Debugging
    float directionAngle = 70f;
    public void DebugRays()
    {
        Vector3 enemyPosition = transform.position;
        Vector3 direction1 = GetDirectionOfTarget();
        direction1 = Quaternion.Euler(0, directionAngle, 0) * direction1;
        Vector3 direction2 = GetDirectionOfTarget();
        direction2 = Quaternion.Euler(0, -directionAngle, 0) * direction2;

        Debug.DrawRay(enemyPosition, direction1, Color.red, 10f);
        Debug.DrawRay(enemyPosition, direction2, Color.red, 10f);
    }

}
