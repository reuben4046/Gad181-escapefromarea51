using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyMovementRP : MonoBehaviour
{
    public LayerMask hidableLayers;
    public EnemyLineOfSightChecker lineOfSightChecker;
    public NavMeshAgent agent;
    [Range(-1, 1)]
    [Tooltip("Lower is a better hiding spot")]
    public float hideSensitivity = 0;

    private Coroutine MovementCoroutine;
    private Collider[] colliders = new Collider[10]; //more is less performant but more options

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();

        lineOfSightChecker.OnGainSight += HandleGainSight;
        lineOfSightChecker.OnLoseSight += HandleLoseSight;
    }

    private void HandleGainSight(Transform target)
    {
        if (MovementCoroutine != null)
        {
            StopCoroutine(MovementCoroutine);
        }

        MovementCoroutine = StartCoroutine(Hide(target));
    }

    private void HandleLoseSight(Transform target)
    {
        if (MovementCoroutine != null)
        {
            StopCoroutine(MovementCoroutine);
        }
    }

    private IEnumerator Hide(Transform target)
    {
        while (true)
        {
            int hits = Physics.OverlapSphereNonAlloc(agent.transform.position, lineOfSightChecker.Collider.radius, colliders, hidableLayers);

            for (int i = 0; i < hits; i++)
            {
                if (NavMesh.SamplePosition(colliders[i].transform.position, out NavMeshHit hit, 2f, agent.areaMask))
                {
                    if (!NavMesh.FindClosestEdge(hit.position, out hit, agent.areaMask))
                    {
                        Debug.LogError($"Failed to find closest edge to {hit.position}");
                    }
                    if (Vector3.Dot(hit.normal, (target.position - hit.position).normalized) < hideSensitivity)
                    {
                        agent.SetDestination(hit.position);
                        break;
                    }
                    else
                    {
                        //since previous spot wasnt facing away from the player we try the other side of the object. 
                        if (NavMesh.SamplePosition(colliders[i].transform.position - (target.position - hit.position).normalized * 2, out NavMeshHit hit2, 2f, agent.areaMask))
                        {
                            if (!NavMesh.FindClosestEdge(hit2.position, out hit2, agent.areaMask))
                            {
                                Debug.LogError($"Failed to find closest edge to {hit2.position} (second attempt)");
                            }
                            if (Vector3.Dot(hit2.normal, (target.position - hit2.position).normalized) < hideSensitivity)
                            {
                                agent.SetDestination(hit2.position);
                                break;
                            }
                        }
                    }
                }
                else
                {
                    Debug.LogError($"Failed to find nav mesh near object {colliders[i].name} at {colliders[i].transform.position}");
                }
            }

            yield return null;
        }
    }
}
