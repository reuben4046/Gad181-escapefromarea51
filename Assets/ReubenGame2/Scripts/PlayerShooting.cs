using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerShooting : MonoBehaviour 
{
    public PlayerBullet PlayerBullet;
    public Transform bulletTransform;

    List<PlayerBullet> bulletList = new List<PlayerBullet>();

    [SerializeField] Camera playerCam;


    [SerializeField] float fireRate = .1f;
    bool canFire = true;
    float shootingTimer;

    void Start()
    {

    }

    private void Update() 
    {

        FireTimer();
        
        if (Input.GetMouseButton(0) && canFire) 
        {
            Shoot();
            ShootingTweenShake();
        }

    }


    void FireTimer() 
    {
        if (canFire == false) 
        {
            shootingTimer += Time.deltaTime;
            if (shootingTimer > fireRate) 
            {
                canFire = true;
                shootingTimer = 0f;
            }
        }
    }
        //next thing to add is reduced accuracy when the player hipfires. 
    void Shoot() 
    {
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit)) 
        {
            canFire = false;
            Vector3 hitPosDirection = hit.point - bulletTransform.position;
            hitPosDirection.Normalize();
            bulletTransform.forward = hitPosDirection;
            PlayerBullet bullet = Instantiate(PlayerBullet, bulletTransform.position, bulletTransform.rotation);
            bulletList.Add(bullet);
        }
    }

    float neutralZ = 0.91f; 
    float kickbackZ = 0.82f;
    void ShootingTweenShake() 
    {
        LeanTween.moveLocalZ(gameObject, kickbackZ, fireRate / 2)
            .setEase(LeanTweenType.easeOutSine)
            .setOnComplete(() => 
            {
                LeanTween.moveLocalZ(gameObject, neutralZ, fireRate / 2)
                .setEase(LeanTweenType.easeOutSine);    
            });
    }

}
