using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour {    
    float speed = 100f;

    //moving the bullet forward every frame
    void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
    }
    
    void OnEnable()
    {
        StartCoroutine(DisableAfter(2f));
    }

    //disables the bullet after 2 seconds
    IEnumerator DisableAfter(float time)
    {
        yield return new WaitForSeconds(time);
        gameObject.SetActive(false);
    }

    //calls an event when the bullet hits something with a target script on it. 
    void OnTriggerEnter(Collider other)
    {
        gameObject.SetActive(false);

        TargetRP target = other.gameObject.GetComponent<TargetRP>();
        if (target != null)
        {
            FPSGameEvents.OnTargetHit.Invoke(target);
        }
    }
}
