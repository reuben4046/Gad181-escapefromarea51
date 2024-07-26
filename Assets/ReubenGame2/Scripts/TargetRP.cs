using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class TargetRP : MonoBehaviour
{
    private float damageAmmount = 10f;
    void OnEnable()
    {
        FPSGameEvents.OnTargetHit += OnTargetHit;
    }
    void OnDisable()
    {
        FPSGameEvents.OnTargetHit -= OnTargetHit;
    }

    void OnTargetHit(Target target)
    {
        TakeDamage(damageAmmount);
    }

    void Awake()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        rb.collisionDetectionMode = CollisionDetectionMode.ContinuousSpeculative;
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Bullet")
        {
            TakeDamage(10f);
        }
    }

    public float health = 100f;
    public void TakeDamage(float amount)
    {
        health -= amount;
        Debug.Log(health);
        if (health <= 0f)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Dead");
    }
}
