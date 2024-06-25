using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class Missile : MonoBehaviour
{
    //Reference to the player
    private PlayerMovementRP player;

    //Rotation speed
    [SerializeField] private float rotationSpeed = 2f;


    // Start is called before the first frame update
    void Start()
    {
        //getting the player
        player = GameObject.Find("Player").GetComponent<PlayerMovementRP>();
    }

    // Update is called once per frame
    void Update()
    {
        RotateTowards();
    }

    //Rotates towards the player at Rotation speed 
    private void RotateTowards()
    {
        if (player != null)
        {
            Vector3 direction = player.transform.position - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg -90f;
            Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
        }
    }
}


