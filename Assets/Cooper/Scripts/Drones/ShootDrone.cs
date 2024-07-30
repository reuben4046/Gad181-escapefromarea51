using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ShootDrone : MonoBehaviour
{
    [SerializeField]
    private Gamemanager droneTracker;
    public float fireDelay = 10f;
    public Crosshair crosshair;
    private bool isOnDrone = false;

    private void Start()
    {
        droneTracker = FindFirstObjectByType<Gamemanager>();
        crosshair = FindFirstObjectByType<Crosshair>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }

    private void OnMouseOver()
    {
        
    }

    void OnMouseDown()
    {
        droneTracker.numberOfdrones -= 1;


        
        if (fireDelay <= 0 & isOnDrone == true)
        {
            fireDelay = 10f;
            Destroy(gameObject);
        }
        
    }

}
