using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTarget : MonoBehaviour
{ 
    [SerializeField] float health = 100f;
    //private float damageAmmount = 10f;    

    void Awake()
    {
        FPSGameEvents.OnPlayerSpawned?.Invoke(this);
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

    void OnPlayerTargetHit(float damage)
    {
        TakeDamage(damage);
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        Debug.Log($"Player Health = {health}");
        if (health <= 0f)
        {
            Die();
        }
    }

    void Die()
    {
        FPSGameEvents.OnPlayerDeath.Invoke();
        gameObject.SetActive(false);
    }
}
