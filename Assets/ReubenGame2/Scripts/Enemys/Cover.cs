using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class CoverRP : MonoBehaviour
{

    Transform target;

    float checkTimeInterval = 0f;

    bool canSeePlayer = false;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindWithTag("Player")?.transform; // ? is a null check
        if (target == null) {Debug.Log($"target= {target}");} //null check
        FPSGameEvents.OnCoverStart?.Invoke(this);

        StartCoroutine(ContinuousPlayerDirectionCheck());
    }


    IEnumerator ContinuousPlayerDirectionCheck()
    {
        while (true)
        {
            yield return new WaitForSeconds(checkTimeInterval);
            Vector2 targetDirection = (target.transform.position - transform.position).normalized;
            Physics.Raycast(transform.position, targetDirection, out RaycastHit hit);
            if (hit.transform == target)
            {
                canSeePlayer = true;
            }
            Debug.Log($"canSeePlayer={canSeePlayer}");
        }
    }
}
