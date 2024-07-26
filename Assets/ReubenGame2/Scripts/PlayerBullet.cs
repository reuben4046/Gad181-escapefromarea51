using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerBullet : MonoBehaviour {    
    float speed = 100f;

    void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
    }

    void OnCollisionEnter(Collision other)
    {
        Debug.Log("collision");
        gameObject.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger");
        gameObject.SetActive(false);
    }
}
