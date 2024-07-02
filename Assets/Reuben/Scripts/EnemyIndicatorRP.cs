using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyIndicatorRP : MonoBehaviour
{

    public MissleSpawnerRP missileSpawner;

    public float minDistance = 3f;

    private float nearestMissileDistance = float.MaxValue;
    private GameObject nearestMissile = null;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        LookForNearestMissile();
        if (nearestMissile != null)
        {
            RotateTowards();
        }
    }

    private void LookForNearestMissile()
    {
        foreach (GameObject missile in missileSpawner.spawnedMissiles)
        {
            float distance = Vector3.Distance(missile.transform.position, transform.position);
            if (distance < minDistance)
            {
                continue;
            }
            else if (distance < nearestMissileDistance)
            {
                nearestMissile = missile;
                nearestMissileDistance = distance;
            }
        }
    }

    //create an event that is fired everytime a missile is spawned so that i can get the latest missle and use the func below to rotate towards it 

    private float rotationSpeed = 10f;

    private void RotateTowards()
    {
        Vector3 direction = nearestMissile.transform.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
    }

}
