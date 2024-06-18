using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class Drone_Movement : MonoBehaviour
{
    public List<Drone_Pattern> Patterns;
    public float speed = 2;
    int index = 0; 
    private Drone_Pattern currentPattern;
    // Start is called before the first frame update
    void Start()
    {
        currentPattern = Patterns[Random.Range(0, Patterns.Count)];
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 destination = currentPattern.waypoints[index].transform.position;
        Vector3 newPos = Vector3.MoveTowards(transform.position, destination, speed * Time.deltaTime);
        transform.position = newPos;

        float distance = Vector3.Distance(transform.position, destination);
        if(distance <= 0.05)
        {
            if(index < currentPattern.waypoints.Count - 1) 
            {
                index++;
            }
            else
            {             
                index = 0;
                
            }
            
        }

    }


}
