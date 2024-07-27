using System.Collections;
using System.Collections.Generic;
using UnityEditor.Callbacks;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(Collider))]
public class StaticTarget : MonoBehaviour
{
    void Awake()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        rb.collisionDetectionMode = CollisionDetectionMode.ContinuousSpeculative;
        rb.isKinematic = true;
    }
}
