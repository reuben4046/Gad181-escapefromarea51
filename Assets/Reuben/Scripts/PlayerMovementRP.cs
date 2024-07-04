using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class PlayerMovementRP : MonoBehaviour
{

    private Rigidbody2D rigidBody;

    [SerializeField] private TrailRenderer leftWing;

    [SerializeField] private TrailRenderer rightWing;

    [SerializeField] private float forwardForce = 1f;

    [SerializeField] private float maxSpeed = 20f;

    [SerializeField] private float rotationSpeed = 0.3f;

    [SerializeField] private float moveSpeed = 25f;


    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        leftWing.emitting = false;
        rightWing.emitting = false;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKey(KeyCode.Space))
        {
            ForwardForce();
        }

        if (Input.GetKey(KeyCode.A))
        {
            RotateLeft();
            RightWingTrail(true);
        }
        else
        {
            RightWingTrail(false);
        }
        if (Input.GetKey(KeyCode.D))
        {
            RotateRight();
            LeftWingTrail(true);
        }
        else
        {
            LeftWingTrail(false);
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

    private void RotateLeft()
    {
        transform.Rotate(0, 0, + rotationSpeed);
    }
    private void RotateRight()
    {
        transform.Rotate(0,0, - rotationSpeed);
    }
   

    void ForwardForce()
    {
        transform.Translate(new Vector2(0, moveSpeed) * Time.deltaTime);
    }
}
