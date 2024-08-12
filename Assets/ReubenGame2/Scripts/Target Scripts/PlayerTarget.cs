using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTarget : MonoBehaviour
{ 
    [SerializeField] float health = 100f;
    [SerializeField] float healthRegenMultiplier = 1.5f;

    void Awake()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        rb.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
    }

    void Start()
    {        
        FPSGameEvents.OnPlayerSpawned?.Invoke(this);
    }

    void OnEnable()
    {
        FPSGameEvents.OnPlayerTargetHit += OnPlayerTargetHit;
    }
    void OnDisable()
    {
        FPSGameEvents.OnPlayerTargetHit -= OnPlayerTargetHit;
    }

    //regenerates player health and sends out an event that lets other scripts know the players health 
    void Update()
    {
        if (health > 0f && health < 100f)
        {
            health += Time.deltaTime * healthRegenMultiplier;      
        }

        FPSGameEvents.OnUpdatePlayerHealth?.Invoke(health);
    }

    void OnPlayerTargetHit(float damage)
    {
        TakeDamage(damage);
        FPSGameEvents.OnPlayPlayerHurt?.Invoke();
    }

    //takes damage and checks if the player should die yet 
    public void TakeDamage(float damage)
    {
        health -= damage;
        Debug.Log($"Player Health = {health}");
        if (health <= 0f)
        {
            Die();
        }
    }

    //sends out an event that lets other scripts know the player has died
    void Die()
    {
        FPSGameEvents.OnPlayerDeath.Invoke();
        gameObject.SetActive(false);
    }
}
