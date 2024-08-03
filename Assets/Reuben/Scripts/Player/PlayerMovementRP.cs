using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class PlayerMovementRP : MonoBehaviour {

    private Rigidbody2D rigidBody;

    [SerializeField] private TrailRenderer leftWing;

    [SerializeField] private TrailRenderer rightWing;

    [SerializeField] private float forwardForce = 1f;

    [SerializeField] private float maxSpeed = 20f;

    [SerializeField] private float rotationSpeed = 200f;

    [SerializeField] private float moveSpeed = 25f;


    // Start is called before the first frame update
    void Start() {
        rigidBody = GetComponent<Rigidbody2D>();
        leftWing.emitting = false;
        rightWing.emitting = false;
    }

    // Update is called once per frame
    void Update() 
    {
        DisableWingTrails();
        ForwardForce();
        playerInputs();
    }


    void playerInputs()
    {
        if (Input.GetKey(KeyCode.A))
        {
            RotateLeft();
            RightWingTrail(true);
        }
        if (Input.GetKey(KeyCode.D)) 
        {
            RotateRight();
            LeftWingTrail(true);
        }
    }

    private void LeftWingTrail(bool showTrail) 
    {
        leftWing.emitting = showTrail;
    }

    private void RightWingTrail(bool showTrail) 
    {
        rightWing.emitting = showTrail;
    }

    void DisableWingTrails()
    {
        LeftWingTrail(false);
        RightWingTrail(false); 
    }

    private void RotateLeft() 
    {
        transform.Rotate(0, 0, + (rotationSpeed * Time.deltaTime)); 
    }
    private void RotateRight() 
    {
        transform.Rotate(0,0, - (rotationSpeed * Time.deltaTime));
    }
   

    void ForwardForce() 
    {
        transform.Translate(new Vector2(0, moveSpeed) * Time.deltaTime);
    }
}
