using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EventSystemRP
{
    public delegate void OnMissileDestroyedDelegate(Missile missile);
    public static OnMissileDestroyedDelegate OnMissileDestroyed;
    
    public delegate void OnMissileSpawnedDelegate(Missile missile);
    public static OnMissileSpawnedDelegate OnMissileSpawned;

    public delegate void GetLastPositionDelegate(Vector2 position);
    public static GetLastPositionDelegate OnGetLastPosition;

    public delegate void CaughtInExplosionDelegate(Missile missile);
    public static CaughtInExplosionDelegate OnCaughtInExplosion;

    public delegate void IncreaseSpawnAmmountDelegate();
    public static IncreaseSpawnAmmountDelegate OnIncreaseSpawnAmmount;

    public delegate void PlayerHitDelegate();
    public static PlayerHitDelegate OnPlayerHit;

    public delegate void PlayerHealthZeroDelegate();
    public static PlayerHealthZeroDelegate OnPlayerHealthZero;

    // Sounds
    public delegate void PlayExplosionSoundDelegate();
    public static PlayExplosionSoundDelegate OnPlayExplosionSound;
}
