using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.TestTools;

public class EnemyController : MonoBehaviour
{
    //PlayerReference
    public Transform target;

    public Camera cam;

    //NavMeshAgent
    public NavMeshAgent agentEnemy;

    //Gun
    public Transform gunTransform;
    bool canFire = true;
    float fireRate = 0.5f;
    float shootingTimer = 0f;    

    //Covers
    [SerializeField] List<GameObject> covers = new List<GameObject>();
    List<Transform> coverPoints = new List<Transform>();

    GameObject currentCover = null;
    Transform currentCoverPoint = null;



    void Start()
    {
        GoToCover();
        StartCoroutine(EnemyControllerFunc());
    }

    void Update()
    {
        DebugRays();

        if (Input.GetMouseButton(0))
        {
            GoToCover();
        }
        if (Input.GetMouseButtonDown(1))
        {
            SwitchCoverPoint(currentCover);
        }
        //ShootAtPlayer();
    }

    IEnumerator EnemyControllerFunc()
    {
        yield return new WaitForSeconds(3f);
        float timeRandom = Random.Range(1f, 5f);
        StartCoroutine(RandomWaitCoroutine(timeRandom));
    }

    IEnumerator RandomWaitCoroutine(float time)
    {
        while (true)
        {
            yield return new WaitForSeconds(time);
            Debug.Log("movingTo Player");
            MoveTowardsPlayer();
            while (IsPlayerVisible().transform != target)
            {
                if (IsPlayerVisible().transform == target)
                {
                    Debug.Log("Player Visible");
                    StopMoving();
                    ShootAtPlayer();
                    yield return new WaitForSeconds(2f);
                }
            }
            float timeRandom = Random.Range(2f, 5f);
            yield return new WaitForSeconds(timeRandom);
            Debug.Log("Going to Cover");
            GoToCover();
            yield return new WaitForSeconds(timeRandom);
        }

    }

    RaycastHit IsPlayerVisible()
    {
        RaycastHit hit;
        Physics.Raycast(transform.position, transform.forward, out hit);
        if (hit.transform == target)
        {
            return hit;
        }
        return new RaycastHit();
    }

    //go to cover
    void GoToCover()
    {
        transform.forward = GetDirectionOfTarget();
        GameObject cover = GetClosestCover();
        Transform destination = GetClosestCoverPoint(cover);

        agentEnemy.SetDestination(destination.position);
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
    

    //switch cover
    Transform SwitchCoverPoint(GameObject currentCover)
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
    void MoveTowardsPlayer()
    {
        agentEnemy.SetDestination(target.position);
    }


    //shoot at player
    void ShootAtPlayer()
    {
        FireTimer();
        transform.LookAt(target);
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit))
        {
            Debug.Log(hit.transform);
            if (hit.transform == target && canFire)
            {
                Shoot();
            }
        }
    }


    void FireTimer() 
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

    void Shoot() 
    {
        if (canFire) 
        {
            EnemyBulletRP bullet = GetPooledEnemyBullet(); 
            bullet.transform.position = gunTransform.position;
            bullet.transform.LookAt(target);
            canFire = false;
        }
    }
    
    EnemyBulletRP GetPooledEnemyBullet()
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
    void StopMoving()
    {
        agentEnemy.SetDestination(transform.position);
    }

    //Debugging
    float directionAngle = 70f;
    void DebugRays()
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



    // void JumpOutAndShoot()
    // {
    //     Vector3 rayShootPoint1 = CurrentCoverPoint.position + new Vector3(0, 0, jumpOutDistance);
    //     Vector3 rayShootPoint2 = CurrentCoverPoint.position - new Vector3(0, 0, -jumpOutDistance);
    //     float distance1 = 1;
    //     float distance2 = 1;
    //     if (Physics.Raycast(rayShootPoint1, target.position - rayShootPoint1, out RaycastHit hit1))
    //     {
    //         distance1 = Vector3.Distance(hit1.point, rayShootPoint1);
    //     }
    //     if (Physics.Raycast(rayShootPoint1, target.position - rayShootPoint2, out RaycastHit hit2))
    //     {
    //         distance2 = Vector3.Distance(hit2.point, rayShootPoint2);
    //     }
    //     if (distance1 < distance2)
    //     {
    //         agentEnemy.SetDestination(rayShootPoint2);
    //     } 
    //     else if (distance1 > distance2)
    //     {
    //         agentEnemy.SetDestination(rayShootPoint1);
    //     }
    // }

    // void OnTriggerEnter(Collider other)
    // {
    //     if (other.gameObject.tag == "Cover")
    //     {
    //         triggeredCovers.Add(other.gameObject);
    //     }
    // }

    // void OnTriggerExit(Collider other)
    // {
    //     if (other.gameObject.tag == "Cover")
    //     {
    //         triggeredCovers.Remove(other.gameObject);
    //     }
    // }

    // GameObject GetClosestCover()
    // {
    //     GameObject closestCover = null;
    //     float closestDistance = 100f;
        

    //     foreach (GameObject cover in covers)
    //     {
    //         if (cover != null)
    //         {        
    //             Vector3 directionOfTarget = (target.position - transform.position).normalized;
    //             float dotProduct = Vector3.Dot(transform.forward, directionOfTarget);
    //             float distance = Vector3.Distance(cover.transform.position, transform.position);
    //             if (distance < closestDistance && distance > 5f && dotProduct >= Mathf.Cos(feildOfView))
    //             {                    
    //                 closestDistance = distance;
    //                 closestCover = cover;
    //             }         
    //         }
    //     }
    //     Vector3 direction = closestCover.transform.position - transform.position;
    //     direction.Normalize();
    //     Debug.DrawRay(transform.position, direction, Color.blue, 5f);
    //     return closestCover;
    // }    
    
    
    
    
    // GameObject GetClosestCover()
    // {
    //     GameObject closestCover = null;
    //     float closestDistance = 100f;

    //     Vector3 directionOfTarget = GetDirectionOfTarget();
    //     transform.forward = directionOfTarget;

    //     Vector3 enemyPosition = transform.position;
    //     Vector3 direction1 = GetDirectionOfTarget();
    //     direction1 = Quaternion.Euler(0, directionAngle, 0) * direction1;
    //     Vector3 direction2 = GetDirectionOfTarget();
    //     direction2 = Quaternion.Euler(0, -directionAngle, 0) * direction2;

    //     foreach (GameObject cover in covers)
    //     {
    //         if (cover != null)
    //         {
    //             Vector3 coverDirection = cover.transform.position - enemyPosition;
    //             if (Vector3.Dot(coverDirection, direction1) > 0 && Vector3.Dot(coverDirection, direction2) > 0)
    //             {
    //                 float distance = Vector3.Distance(cover.transform.position, transform.position);
    //                 if (distance < closestDistance)
    //                 {
    //                     closestDistance = distance;
    //                     closestCover = cover;
    //                 }
    //             }                
    //         }
    //     }

    //     return closestCover;
    // }