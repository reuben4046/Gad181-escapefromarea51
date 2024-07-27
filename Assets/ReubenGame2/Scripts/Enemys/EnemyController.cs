using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.TestTools;

public class EnemyController : MonoBehaviour
{
    public Transform target;
    public Camera cam;
    public NavMeshAgent agentEnemy;

    [SerializeField] List<GameObject> covers = new List<GameObject>();
    List<Transform> coverPoints = new List<Transform>();

    float directionAngle = 70f;

    void Start()
    {
        //GoToCover();
    }

    void GoToCover()
    {
        //List<GameObject> filteredCovers = ShootRaysToFindRoute();
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

        Vector3 directionOfTarget = GetDirectionOfTarget();
        transform.forward = directionOfTarget;

        Vector3 enemyPosition = transform.position;
        Vector3 direction1 = GetDirectionOfTarget();
        direction1 = Quaternion.Euler(0, directionAngle, 0) * direction1;
        Vector3 direction2 = GetDirectionOfTarget();
        direction2 = Quaternion.Euler(0, -directionAngle, 0) * direction2;

        foreach (GameObject cover in covers)
        {
            Vector3 coverDirection = cover.transform.position - enemyPosition;
            if (Vector3.Dot(coverDirection, direction1) > 0 && Vector3.Dot(coverDirection, direction2) > 0)
            {
                float distance = Vector3.Distance(cover.transform.position, transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestCover = cover;
                }
            }
        }

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
            Transform currentCoverPoint = null;
            float distance = Vector3.Distance(point.position, transform.position);
            if(currentCoverPoint)
            {
                continue;
            }
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestCoverPoint = point;
            }
            currentCoverPoint = closestCoverPoint;
        }
        coverPoints.Clear();
        return closestCoverPoint;
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
