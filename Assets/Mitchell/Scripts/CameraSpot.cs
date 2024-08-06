using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CameraSpot : MonoBehaviour
{
    public TextMeshProUGUI CameraRun;
    public Image CameraScreen;

    // Start is called before the first frame update
    void Start()
    {
        // Keeps them disabled until the player collides with the box collider
        CameraRun.enabled = false;
        CameraScreen.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        CameraRun.enabled = true;
        CameraScreen.enabled = true;
    }
    private void OnTriggerExit(Collider other)
    {
        // call on the first frame the object leaves the trigger volume.
        //Debug.Log("on Trigger Exit");
        CameraRun.enabled = false;
        CameraScreen.enabled = false;
    }
}
