using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScanBar : MonoBehaviour
{
    public Slider scanBar;
    public float magicNumber;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
         
    }

    private void OnEnable()
    {
        CRGameEvents.OnDroneScanning += OnDroneScanning;
    } 

    private void OnDisable()
    {
        CRGameEvents.OnDroneScanning -= OnDroneScanning;
    }

    private void OnDroneScanning(DroneScan drone)
    {
        scanBar.value += magicNumber;
    }
}
