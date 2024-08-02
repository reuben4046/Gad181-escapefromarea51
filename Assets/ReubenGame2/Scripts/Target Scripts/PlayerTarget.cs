using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTarget : MonoBehaviour
{ 
    [SerializeField] float health = 100f;
    private float damageAmmount = 10f;    

    void Awake()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        rb.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
    }

    void OnEnable()
    {
        FPSGameEvents.OnPlayerTargetHit += OnPlayerTargetHit;
    }
    void OnDisable()
    {
        FPSGameEvents.OnPlayerTargetHit -= OnPlayerTargetHit;
    }

    void OnPlayerTargetHit()
    {
        TakeDamage(damageAmmount);
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
        Debug.Log($"Player Health = {health}");
        if (health <= 0f)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Player Dead");
    }
}
