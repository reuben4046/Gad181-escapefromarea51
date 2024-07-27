using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public Transform target;
    public Camera cam;
    public NavMeshAgent agentEnemy;

    [SerializeField] List<GameObject> covers = new List<GameObject>();
    List<Transform> coverPoints = new List<Transform>();

    void Start()
    {
        GoToCover();
    }

    void GoToCover()
    {
        GameObject cover = GetClosestCover();
        Transform destination = GetClosestCoverPoint(cover);
        agentEnemy.SetDestination(destination.position);
    }

    GameObject GetClosestCover()
    {
        GameObject closestCover = null;
        float closestDistance = 100f;
        foreach (GameObject cover in covers)
        {
            float distance = Vector3.Distance(cover.transform.position, transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestCover = cover;
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

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                agentEnemy.SetDestination(hit.point);
            }
        }
    }
}
