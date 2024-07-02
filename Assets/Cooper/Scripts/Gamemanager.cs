using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gamemanager : MonoBehaviour
{
    bool mouse;
    public Spawnpoints_List spawnpoints;
    public GameObject drone;
    public int maxdrones = 10;
    private int numberOfdrones;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (numberOfdrones < maxdrones)
        {
            Instantiate(drone, spawnpoints.spawnPoints[Random.Range(0, spawnpoints.spawnPoints.Count)].transform.position, Quaternion.identity);
            numberOfdrones += 1;
        }
    }

    

    
}
