using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CRGameEvents
{
    public delegate void OnDroneScanningDelegate(DroneScan drone);
    public static OnDroneScanningDelegate OnDroneScanning;
}
