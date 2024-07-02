using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform TargetPlayer;
    public float turnSpeed = 1f;
    public float walkSpeed = 1f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //RotateTowardsPlayer();
       // MoveTowardsPlayer();
    }

    public void RotateTowardsPlayer()
    {
        Vector3 targetDirection;

        if (TargetPlayer == null)
            return;

        targetDirection = transform.position - TargetPlayer.position;

        float turnStep = turnSpeed * Time.deltaTime;
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, turnStep, 0.0f);

        transform.rotation = Quaternion.LookRotation(newDirection);
    }

    public void MoveTowardsPlayer()
    {
        float moveSpeed = walkSpeed;

        if (TargetPlayer == null)
            return;

        var moveStep = moveSpeed * Time.deltaTime;

        transform.position = Vector3.MoveTowards(transform.position, TargetPlayer.position, moveStep);
    } 

}
