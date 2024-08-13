using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gamemanager : MonoBehaviour
{
    bool mouse;
    public Spawnpoints_List spawnpoints;
    public GameObject drone;
    public int maxdrones = 10;
    public int numberOfdrones;
    private bool firstSpawnDone = false;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        StartCoroutine(StartSpawning());
    }

    private void Update()
    {
        if (numberOfdrones < maxdrones && firstSpawnDone)
        {
            StartCoroutine(DroneSpawn());

        }
            
    }

    IEnumerator StartSpawning()
    {
        
        while (numberOfdrones < maxdrones)
        {
            numberOfdrones += 1;
            yield return new WaitForSeconds(2);
            var newdrone = Instantiate(drone, spawnpoints.spawnPoints[Random.Range(0, spawnpoints.spawnPoints.Count)].transform.position, Quaternion.identity);

        }
        firstSpawnDone = true;

        
        
       
    }

    private IEnumerator DroneSpawn()
    {
        numberOfdrones += 1;
        yield return new WaitForSeconds(2);
        var newdrone = Instantiate(drone, spawnpoints.spawnPoints[Random.Range(0, spawnpoints.spawnPoints.Count)].transform.position, Quaternion.identity);
    }



    

    
}
