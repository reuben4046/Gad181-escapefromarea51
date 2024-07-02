using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EventSystemRP
{
    public delegate void OnMissileDestroyedDelegate(GameObject missile);
    public static OnMissileDestroyedDelegate OnMissileDestroyed;
    
}
