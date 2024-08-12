using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSoundFPS : MonoBehaviour
{
    [SerializeField] AudioSource playerGunShot;
    [SerializeField] AudioSource playerHurt;
    [SerializeField] AudioSource enemyGunShot;
    [SerializeField] AudioSource enemyDead;



    void OnEnable()
    {
        FPSGameEvents.OnPlayPlayerGunShot += OnPlayPlayerGunShot;
        FPSGameEvents.OnPlayPlayerHurt += OnPlayPlayerHurt;

        FPSGameEvents.OnPlayEnemyGunShot += OnPlayEnemyGunShot;
        FPSGameEvents.OnPlayEnemyDead += OnPlayEnemyDead;
    }

    void OnDisable()
    {
        FPSGameEvents.OnPlayPlayerGunShot -= OnPlayPlayerGunShot;
        FPSGameEvents.OnPlayPlayerHurt -= OnPlayPlayerHurt;

        FPSGameEvents.OnPlayEnemyGunShot -= OnPlayEnemyGunShot;
        FPSGameEvents.OnPlayEnemyDead -= OnPlayEnemyDead;
    }

    void OnPlayPlayerGunShot()
    { 
        playerGunShot.PlayOneShot(playerGunShot.clip);
    }

    void OnPlayPlayerHurt()
    {
        playerHurt.PlayOneShot(playerHurt.clip);
    }
    
    
    void OnPlayEnemyGunShot()
    {
        enemyGunShot.PlayOneShot(enemyGunShot.clip);
    }

    void OnPlayEnemyDead()
    {
        enemyDead.PlayOneShot(enemyDead.clip);
    }
}
