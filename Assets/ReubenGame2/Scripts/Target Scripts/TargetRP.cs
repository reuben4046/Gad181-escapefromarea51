using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class TargetRP : MonoBehaviour
{
    private float damageAmmount = 10f;
    [SerializeField] private float health = 100f; 

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

    //takes damage if this target is hit
    void OnTargetHit(TargetRP target)
    {
        if (target == this)
        {
            TakeDamage(damageAmmount);
        }
    }

    //takes damage and checks if the enemy should die yet
    public void TakeDamage(float amount)
    {
        health -= amount;
        Debug.Log($"Enemy Health = {health}");
        if (health <= 0f)
        {
            Die();
        }
    }

    //sends out an event that lets other scripts know this enemy has died
    void Die()
    {
        FPSGameEvents.OnEnemyDeath.Invoke(this);
        gameObject.SetActive(false);
    }
}
