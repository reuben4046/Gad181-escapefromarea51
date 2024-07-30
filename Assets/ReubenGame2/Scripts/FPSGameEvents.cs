using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class FPSGameEvents
{
    public delegate void OnTargetHitDelegate(Target target);
    public static OnTargetHitDelegate OnTargetHit;

    public delegate void OnCoverStartDelegate(CoverToList cover);
    public static OnCoverStartDelegate OnCoverStart;

    public delegate void OnStateChangedDelegate(State newState);
    public static OnStateChangedDelegate OnStateChanged;
}
