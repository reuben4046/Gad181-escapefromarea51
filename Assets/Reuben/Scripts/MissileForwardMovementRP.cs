using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;

public class MissileForwardMovementRP : MonoBehaviour
{

    [SerializeField] private float acceleration = 3f;

    [SerializeField] private float maxSpeed = 10f;

    [SerializeField] private float currentSpeed = 8f;



    // Update is called once per frame
    void Update()
    {
        ForwardForce();
    }

    private void ForwardForce()
    {
        currentSpeed = Mathf.Min(currentSpeed + acceleration * Time.deltaTime, maxSpeed);
        transform.Translate(new Vector2(0, currentSpeed) * Time.deltaTime);
    }

}
