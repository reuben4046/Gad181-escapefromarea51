using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileForwardMovementRP : MonoBehaviour {
    //this script moves the missile forward
    [SerializeField] private float acceleration = 3f;

    [SerializeField] private float maxSpeed = 19f;

    [SerializeField] private float currentSpeed = 8f;



    // Update is called once per frame
    void Update() 
    {
        ForwardForce();
    }


    private void ForwardForce() 
    {
        currentSpeed = Mathf.Min(currentSpeed + acceleration * Time.deltaTime, maxSpeed); //Mathf.Min is used to make sure the speed does not exceed the max speed
        transform.Translate(new Vector2(0, currentSpeed) * Time.deltaTime);
    }

}
