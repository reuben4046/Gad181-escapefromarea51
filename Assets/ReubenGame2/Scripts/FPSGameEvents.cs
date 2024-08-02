using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class FPSGameEvents
{
    public delegate void OnTargetSpawnedDelegate(TargetRP target);
    public static OnTargetSpawnedDelegate OnTargetSpawned;

    public delegate void OnTargetHitDelegate(TargetRP target);
    public static OnTargetHitDelegate OnTargetHit;

    public delegate void OnPlayerTargetHitDelegate();
    public static OnPlayerTargetHitDelegate OnPlayerTargetHit;

    public delegate void OnCoverStartDelegate(CoverRP cover);
    public static OnCoverStartDelegate OnCoverStart;

    public delegate void OnSwitchStateDelegate(BaseEnemyState State, StateManager enemy);
    public static OnSwitchStateDelegate OnSwitchState;
}
