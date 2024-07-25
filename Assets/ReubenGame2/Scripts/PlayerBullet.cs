using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour {    
    [SerializeField] Rigidbody rb;
    float force = 100f;

    void Start() {
        rb.AddForce(transform.forward * force, ForceMode.Impulse);
    }

    void OnCollisionEnter(Collision other)
    {
        
    }
}
