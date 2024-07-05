using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EventSystemRP
{
    public delegate void OnMissileDestroyedDelegate(GameObject missile);
    public static OnMissileDestroyedDelegate OnMissileDestroyed;
    
    public delegate void OnMissileSpawnedDelegate(GameObject missile);
    public static OnMissileSpawnedDelegate OnMissileSpawned;

    public delegate void GetLastPositionDelegate(Vector2 position);
    public static GetLastPositionDelegate OnGetLastPosition;
}
