using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.TestTools;

public class EnemyController : MonoBehaviour
{


    //possible way to get this working properly could be to use a trigger area instead of getting direction angles. 
    //then just grab every cover that is registered in the trigger area. 

    public Transform target;
    public Camera cam;
    public NavMeshAgent agentEnemy;

    [SerializeField] BoxCollider triggerArea;

    [SerializeField] List<GameObject> triggeredCovers = new List<GameObject>();
    [SerializeField] List<GameObject> covers = new List<GameObject>();
    List<Transform> coverPoints = new List<Transform>();

    float directionAngle = 70f;

    void Start()
    {
        //GoToCover();
    }

    void Update()
    {
        DebugRays();

        if (Input.GetMouseButton(0))
        {
            GoToCover();
            // Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            // RaycastHit hit;

            // if (Physics.Raycast(ray, out hit))
            // {
            //     agentEnemy.SetDestination(hit.point);
            // }
        }
        if (Input.GetMouseButton(1))
        {
            JumpOutAndShoot();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Cover")
        {
            triggeredCovers.Add(other.gameObject);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Cover")
        {
            triggeredCovers.Remove(other.gameObject);
        }
    }

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
    //         Vector3 coverDirection = cover.transform.position - enemyPosition;
    //         if (Vector3.Dot(coverDirection, direction1) > 0 && Vector3.Dot(coverDirection, direction2) > 0)
    //         {
    //             float distance = Vector3.Distance(cover.transform.position, transform.position);
    //             if (distance < closestDistance)
    //             {
    //                 closestDistance = distance;
    //                 closestCover = cover;
    //             }
    //         }
    //     }

    //     return closestCover;
    // }

    GameObject GetClosestCover()
    {
        GameObject closestCover = null;
        float closestDistance = 100f;
        

        foreach (GameObject cover in triggeredCovers)
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
        Vector3 direction = closestCover.transform.position - transform.position;
        direction.Normalize();
        Debug.DrawRay(transform.position, direction, Color.blue, 5f);
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
            float distance = Vector3.Distance(point.position, transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestCoverPoint = point;
            }
        }
        coverPoints.Clear();
        return closestCoverPoint;
    }


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
