using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drone_Movement : MonoBehaviour
{
    public List<GameObject> waypoints;
    public float speed = 2;
    int index = 0;
    public bool
 isLoop = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    { 
        
        Vector3 destination = waypoints[index].transform.position;
        Vector3 newPos = Vector3.MoveTowards(transform.position, destination, speed * Time.deltaTime);
        transform.position = newPos;

        float distance = Vector3.Distance(transform.position, destination);
        if(distance <= 0.05)
        {
            if(index < waypoints.Count - 1) 
            {
                index++;
            }
            else
            {
                if(isLoop) 
                { 
                    index = 0;
                }
                
            }
            
        }

    }


}
