using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class PlayerMovementRP : MonoBehaviour
{

    private Rigidbody2D rigidBody;

    private float forwardForce = 1f;

    private float maxSpeed = 20f;

    private float rotationSpeed = 0.2f;

    [SerializeField] private float moveSpeed = 20f;


    // Start is called before the first frame update
    void Start()
    {
        xAxis = Input.GetAxis("Horizontal");
        yAxis = Input.GetAxis("Vertical");
        rigidBody = GetComponent<Rigidbody2D>();
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
        }
        else if (Input.GetKey(KeyCode.D))
        {
            RotateRight();
        }
        //if (rigidBody.velocity.magnitude > maxSpeed)
        //{
        //    rigidBody.velocity = Vector2.ClampMagnitude(rigidBody.velocity, maxSpeed);
        //}
    }


    //private void FixedUpdate()
    //{
        //ForwardForce();
      //  if (Rigidbody2D.velocity.magnitude > maxSpeed)
      //  {
      //      Rigidbody2D.velocity = Rigidbody2D.velocity.normalized * maxSpeed;
      //  }
   // }

    private void RotateLeft()
    {
        transform.Rotate(0, 0, rotationSpeed);
    }
    private void RotateRight()
    {
        transform.Rotate(0,0, -rotationSpeed);
    }
   

    void ForwardForce()
    {
        transform.Translate(new Vector2(0, moveSpeed)* Time.deltaTime);
        //rigidBody.velocity = transform.up * moveSpeed;

        //transform.Translate(transform.up * Time.deltaTime, Space.Self); 
        //rigidBody.AddForce(transform.up * forwardForce);
    }
}
