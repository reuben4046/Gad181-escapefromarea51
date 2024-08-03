using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class PlayerMovementRP : MonoBehaviour 
{
    //wingTrail references
    [SerializeField] private TrailRenderer leftWing;
    [SerializeField] private TrailRenderer rightWing;

    //enums
    enum WingTrailSide{left, right, noTrail}
    enum Rotate{left, right}

    //speed variables
    [SerializeField] private float rotationSpeed = 200f;
    [SerializeField] private float moveSpeed = 25f;


    // Start is called before the first frame update
    void Start() 
    {
        leftWing.emitting = false;
        rightWing.emitting = false;
    }

    // Update is called once per frame
    void Update() 
    {
        WingTrail(WingTrailSide.noTrail);
        MoveForward();
        playerInputs();
    }


    void playerInputs()
    {
        if (Input.GetKey(KeyCode.A))
        {
            RotatePlayer(Rotate.left);
            WingTrail(WingTrailSide.right);
        }
        if (Input.GetKey(KeyCode.D)) 
        {
            RotatePlayer(Rotate.right);
            WingTrail(WingTrailSide.left);
        }
    }


    private void WingTrail(WingTrailSide side)
    {
        switch (side)
        {
            case WingTrailSide.left:
            {
                leftWing.emitting = true;
                break;
            }
            case WingTrailSide.right:
            {
                rightWing.emitting = true;
                break;                
            }
            case WingTrailSide.noTrail:
            {
                leftWing.emitting = false;
                rightWing.emitting = false;
                break;
            }
        }
    }

    private void RotatePlayer(Rotate rotationDirection)
    {
        float rotationAngle = rotationSpeed * Time.deltaTime * (rotationDirection == Rotate.left ? 1 : -1);
        transform.Rotate(0, 0, rotationAngle);
    }

    void MoveForward() 
    {
        transform.Translate(new Vector2(0, moveSpeed) * Time.deltaTime);
    }
}
