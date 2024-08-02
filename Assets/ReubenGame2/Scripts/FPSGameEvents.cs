using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class FPSGameEvents
{
    //targets Hit
    public delegate void OnTargetHitDelegate(TargetRP target);
    public static OnTargetHitDelegate OnTargetHit;

    public delegate void OnPlayerTargetHitDelegate(float damageAmmount);
    public static OnPlayerTargetHitDelegate OnPlayerTargetHit;

    //used to add covers to lists where needed
    public delegate void OnCoverStartDelegate(CoverRP cover);
    public static OnCoverStartDelegate OnCoverStart;

    //used to switch enemy states
    public delegate void OnSwitchStateDelegate(BaseEnemyState State, StateManager enemy);
    public static OnSwitchStateDelegate OnSwitchState;

    //Player death
    public delegate void OnPlayerDeathDelegate();
    public static OnPlayerDeathDelegate OnPlayerDeath;

    //Player sprinting and aiming
    public delegate void OnPlayerSprintingDelegate(bool isSprinting);
    public static OnPlayerSprintingDelegate OnPlayerSprinting;

    public delegate void OnPlayerAimingDelegate(bool isAiming);
    public static OnPlayerAimingDelegate OnPlayerAiming;
}
