using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class TargetRP : MonoBehaviour
{
    private float damageAmmount = 10f;
    [SerializeField] private float health = 100f; 

    void Start()
    {
        //FPSGameEvents.OnTargetSpawned.Invoke(this);
    }

    void Awake()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        rb.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
    }


    void OnEnable()
    {
        FPSGameEvents.OnTargetHit += OnTargetHit;
    }
    void OnDisable()
    {
        FPSGameEvents.OnTargetHit -= OnTargetHit;
    }

    void OnTargetHit(TargetRP target)
    {
        if (target == this)
        {
            TakeDamage(damageAmmount);
        }
    }


    public void TakeDamage(float amount)
    {
        health -= amount;
        Debug.Log($"Enemy Health = {health}");
        if (health <= 0f)
        {
            Die();
        }
    }

    void Die()
    {
        FPSGameEvents.OnEnemyDeath.Invoke(this);
        gameObject.SetActive(false);
    }
}
