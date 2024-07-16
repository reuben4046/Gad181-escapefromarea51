using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIndicatorRP : MonoBehaviour
{
    //Reference to the newest missile
    private Missile newestMissile;

    //Rotation speed that the indication will rotate at around the player
    private float rotationSpeed = 20f;

    public GameObject indicator;

    //subscribing and unsubscribing to the OnMIssileSpawned event
    private void OnEnable()
    {
        EventSystemRP.OnMissileSpawned += OnMissileSpawned;
    }
    private void OnDisable()
    {
        EventSystemRP.OnMissileSpawned -= OnMissileSpawned;
    }


    // Update is called once per frame
    void Update()
    {
        if (newestMissile == null)
        {
            indicator.SetActive(false);
        }
        else
        {
            indicator.SetActive(true);
        }
        if (newestMissile != null)
        {
            RotateTowards();
        }
    }

    //this function is called when the onmissilespawned event is triggered
    private void OnMissileSpawned(Missile missile)
    {
        newestMissile = missile;
    }


    //Rotates towards the newest missile at Rotation speed
    private void RotateTowards()
    {
        Vector3 direction = newestMissile.transform.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
    }

}
