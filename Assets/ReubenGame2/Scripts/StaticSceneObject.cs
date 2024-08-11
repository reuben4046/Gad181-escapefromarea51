using System.Collections;
using System.Collections.Generic;
using UnityEditor.Callbacks;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(Collider))]
public class StaticSceneObject : MonoBehaviour
{
    //makes sure the static scene objects have the right setting on their rigidbody component 
    void Awake()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
        rb.isKinematic = true;
    }
}
