using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Repeating_Background : MonoBehaviour
{
    Vector3 startPos;
    // Start is called before the first frame update
    void Start()
    {
        startPos = new Vector3(5,0,-80);
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.z > startPos.z + 90) 
        { 
            transform.position = startPos;
        }
    }
}
