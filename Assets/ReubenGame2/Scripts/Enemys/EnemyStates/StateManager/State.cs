using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.AI;

public abstract class State : MonoBehaviour
{
    public abstract State RunCurrentState();


    //PlayerReference
    protected Transform target;

    protected Camera cam;

    //NavMeshAgent
    protected NavMeshAgent agentEnemy;

    //Gun
    protected Transform gunTransform;
    protected bool canFire = true;
    protected float fireRate = 0.5f;
    protected float shootingTimer = 0f;

    //Covers
    protected List<GameObject> covers = new List<GameObject>();
    protected List<Transform> coverPoints = new List<Transform>();

    protected GameObject currentCover = null;
    protected Transform currentCoverPoint = null;

    void Start()
    {
        target = GameObject.FindWithTag("Player").transform;
        cam = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();

        agentEnemy = GameObject.FindWithTag("Enemy").GetComponent<NavMeshAgent>();

        gunTransform = agentEnemy.transform.GetChild(0);

        foreach (GameObject cover in GameObject.FindGameObjectsWithTag("Cover"))
        {
            covers.Add(cover);
        }
    }

    void Update()
    {
        DebugRays();
    }


    // bool playerVisible = false;
    // IEnumerator ContinuousRayCast()
    // {
    //     while (true)
    //     {
    //         yield return new WaitForSeconds(0.5f);
    //         Vector3 rayDirection = (target.position - transform.position).normalized;
    //         RaycastHit hit;
    //         Physics.Raycast(transform.position, rayDirection, out hit);
    //         Debug.Log(hit.transform.name);
    //         if (hit.transform == target)
    //         {
    //             playerVisible = true;
    //             Debug.Log("Player Detected");
    //         }
    //         else
    //         {
    //             playerVisible = false;
    //         }
    //     }
    // }



    //go to cover
    protected void GoToCover()
    {
        transform.forward = GetDirectionOfTarget();
        GameObject cover = GetClosestCover();
        Transform destination = GetClosestCoverPoint(cover);

        agentEnemy.SetDestination(destination.position);
    }

    protected Vector3 GetDirectionOfTarget()
    {
        Vector3 direction = target.position - transform.position;
        direction.Normalize();
        return direction;
    }

    protected GameObject GetClosestCover()
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

    protected Transform GetClosestCoverPoint(GameObject cover)
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
    

    //switch cover
    protected virtual Transform SwitchCoverPoint(GameObject currentCover)
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
