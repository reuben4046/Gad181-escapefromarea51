using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerShooting : MonoBehaviour {

    public PlayerBullet PlayerBullet;
    public Transform bulletTransform;

    List<PlayerBullet> bulletList = new List<PlayerBullet>();


    [SerializeField] float fireRate = .1f;
    bool canFire = true;
    float shootingTimer;

    void Start()
    {

    }

    private void Update() {

        FireTimer();
        
        if (Input.GetMouseButton(0) && canFire) {
            InstanciateBullet();
            ShootingTweenShake();
        }

    }


    void FireTimer() {
        if (canFire == false) {
            shootingTimer += Time.deltaTime;
            if (shootingTimer > fireRate) {
                canFire = true;
                shootingTimer = 0f;
            }
        }
    }
        
    void InstanciateBullet() {
        canFire = false;
        PlayerBullet bullet = Instantiate(PlayerBullet, bulletTransform.position, bulletTransform.rotation);
        bulletList.Add(bullet);
    }

    float neutralZ = 0.91f; 
    float kickbackZ = 0.82f;
    void ShootingTweenShake() {
        LeanTween.moveLocalZ(gameObject, kickbackZ, fireRate / 2)
                .setEase(LeanTweenType.easeOutSine)
                .setOnComplete(() => {
                    LeanTween.moveLocalZ(gameObject, neutralZ, fireRate / 2)
                        .setEase(LeanTweenType.easeOutSine);    
                });
    }

}
