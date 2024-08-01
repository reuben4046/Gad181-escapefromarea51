using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class CoverRP : MonoBehaviour
{
    Transform target;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindWithTag("Player")?.transform; // ? is a null check
        if (target == null) {Debug.Log($"target= {target}");} //null check
        FPSGameEvents.OnCoverStart?.Invoke(this);
    }
}
