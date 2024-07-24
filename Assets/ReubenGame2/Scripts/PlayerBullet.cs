using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour {    
    
    float speed = 1.0f;

    // Update is called once per frame
    void Update() {
        ForwardMovement();
    }

    private void ForwardMovement () {
        transform.position = Vector3.forward * speed;
    }
}
