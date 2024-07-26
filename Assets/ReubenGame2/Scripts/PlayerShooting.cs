using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerShooting : MonoBehaviour 
{
    [Header("References")]
    [SerializeField] PlayerBullet PlayerBullet;
    [SerializeField] Transform bulletTransform;
    [SerializeField] GameObject playerGun;
    [SerializeField] ParticleSystem muzzleFlash;

    [Header("HipFire")]    


    [Header("Fire Rate")]
    [SerializeField] float fireRate = .5f;
    bool canFire = true;
    float shootingTimer;

    //LeanTween variables
    float neutralZ = 0.91f; 
    float kickbackZ = 0.82f;

    private void Update() 
    {
        FireTimer();
        if (Input.GetMouseButton(0) && canFire) 
        {
            Shoot();
            ShootingTweenShake();
        } 

        // if (Input.GetMouseButton(1))
        // {  
        //     isHipFiring = false;
        // } 
        // else { isHipFiring = true;}
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

    void Shoot() 
    {
        if (canFire) 
        {
            GetPooledBullet();
            canFire = false;
        }
    }

    void GetPooledBullet()
    {
        PlayerBullet playerBullet = ObjectPool.instance.GetPooledBullet();
        if (playerBullet != null)
        {
            playerBullet.transform.position = bulletTransform.position;
            playerBullet.transform.rotation = bulletTransform.rotation;
            playerBullet.gameObject.SetActive(true);
            muzzleFlash.Play();
        }
    }


    void ShootingTweenShake() 
    {
        LeanTween.moveLocalZ(playerGun.gameObject, kickbackZ, fireRate / 2)
            .setEase(LeanTweenType.easeOutSine)
            .setOnComplete(() => 
            {
                LeanTween.moveLocalZ(playerGun.gameObject, neutralZ, fireRate / 2)
                .setEase(LeanTweenType.easeOutSine);    
            });
    }

}
