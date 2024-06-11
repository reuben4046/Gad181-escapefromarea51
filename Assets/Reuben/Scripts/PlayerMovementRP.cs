using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class PlayerMovementRP : MonoBehaviour
{

    private Rigidbody2D Rigidbody2D;

    private float forwardForce = 10f;

    private float maxSpeed = 20f;


    // Start is called before the first frame update
    void Start()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
        //ForwardForce();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Space))
            ForwardForce();
    }
    //private void FixedUpdate()
    //{
        //ForwardForce();
      //  if (Rigidbody2D.velocity.magnitude > maxSpeed)
      //  {
      //      Rigidbody2D.velocity = Rigidbody2D.velocity.normalized * maxSpeed;
      //  }
   // }

    void ForwardForce()
    {
        
        Rigidbody2D.AddForce(transform.up * forwardForce);
        Debug.Log(transform.forward);
    }
}
