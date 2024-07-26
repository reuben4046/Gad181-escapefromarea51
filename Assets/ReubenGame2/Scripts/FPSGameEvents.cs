using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class FPSGameEvents
{
    public delegate void OnTargetHitDelegate(Target target);
    public static OnTargetHitDelegate OnTargetHit;
}
