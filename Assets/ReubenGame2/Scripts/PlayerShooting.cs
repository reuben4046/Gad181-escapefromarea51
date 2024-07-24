using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour {

    public PlayerBullet PlayerBullet;

    public Transform bulletTransform;

    List<PlayerBullet> bulletList = new List<PlayerBullet>();
    

    private void Update() {
        if (Input.GetMouseButtonDown(0)) {
            if (bulletTransform != null) {
                PlayerBullet bullet = Instantiate(PlayerBullet, bulletTransform.position, Quaternion.identity);
                bulletList.Add(bullet);
            }
        }
    }


}
