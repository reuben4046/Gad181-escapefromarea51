using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ShootDrone : MonoBehaviour
{
    [SerializeField]
    private Gamemanager droneTracker;

    private void Start()
    {
        droneTracker = FindFirstObjectByType<Gamemanager>();
    }


    void OnMouseDown()
    {
        droneTracker.numberOfdrones -= 1;
        Destroy(gameObject);
    }

}
