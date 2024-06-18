using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile_Movement : MonoBehaviour
{
    public int speed = 10;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * speed);
    }
}
