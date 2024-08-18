using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScanBar : MonoBehaviour
{
    public Slider scanBar;
    public float magicNumber;
    public GameObject gameoverScreen;
    public GameObject gameHud;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
         if (scanBar.value == 100)
        {
            Cursor.visible = true;
            gameHud.SetActive(false);
            gameoverScreen.SetActive(true);
        }
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
