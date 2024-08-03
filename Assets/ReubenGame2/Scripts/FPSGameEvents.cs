using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class FPSGameEvents
{
    //player spawned
    public delegate void OnPlayerSpawnedDelegate(PlayerTarget playerTarget);
    public static OnPlayerSpawnedDelegate OnPlayerSpawned;
    //PlayerHealth 
    public delegate void OnUpdatePlayerHealthDelegate(float health);
    public static OnUpdatePlayerHealthDelegate OnUpdatePlayerHealth;

    //targets Hit
    public delegate void OnTargetHitDelegate(TargetRP target);
    public static OnTargetHitDelegate OnTargetHit;

    public delegate void OnPlayerTargetHitDelegate(float damageAmmount);
    public static OnPlayerTargetHitDelegate OnPlayerTargetHit;

    //used to add covers to lists where needed
    public delegate void OnCoverStartDelegate(CoverRP cover);
    public static OnCoverStartDelegate OnCoverStart;

    //used to switch enemy states
    public delegate void OnSwitchStateDelegate(BaseEnemyState State, EnemyStateManager enemy);
    public static OnSwitchStateDelegate OnSwitchState;

    //Enemy Death
    public delegate void OnEnemyDeathDelegate(TargetRP target);
    public static OnEnemyDeathDelegate OnEnemyDeath;

    //Player death
    public delegate void OnPlayerDeathDelegate();
    public static OnPlayerDeathDelegate OnPlayerDeath;

    //Player sprinting and aiming
    public delegate void OnPlayerSprintingDelegate(bool isSprinting);
    public static OnPlayerSprintingDelegate OnPlayerSprinting;

    public delegate void OnPlayerAimingDelegate(bool isAiming);
    public static OnPlayerAimingDelegate OnPlayerAiming;
}
