using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtPlayer : MonoBehaviour
{

    Transform target;

    void Start()
    {
        target = GameObject.FindWithTag("Player")?.transform;
    }
    
    void Update()
    {
        transform.LookAt(target);
    }
}
