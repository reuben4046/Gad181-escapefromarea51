using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshPlayerInput : MonoBehaviour
{
    [SerializeField] NavMeshAgent agent;
    [SerializeField] Camera cam;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(cam.ScreenPointToRay(Input.mousePosition), out RaycastHit hit))
            {
                agent.SetDestination(hit.point);
            }
        }
    }
}
