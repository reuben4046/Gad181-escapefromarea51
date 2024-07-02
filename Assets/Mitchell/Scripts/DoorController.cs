using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    // Start is called before the first frame update
    private void Start()
    {
        DoorEvents.current.onDoorTriggerEnter += OnDoorwayOpen;
    }

    private void OnDoorwayOpen()
    {
        
    }
}
