using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Security.Cryptography;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.UIElements;

public class PlayerShooting : MonoBehaviour 
{
    [Header("References")]
    [SerializeField] PlayerBullet PlayerBullet;
    [SerializeField] Transform bulletTransform;
    [SerializeField] PlayerGunRP playerGun;

    [Header("HipFire")]    
    [SerializeField] private float hipFireBulletSpreadRadius = 8f;
    private bool isHipFiring = true;


    [Header("Fire Rate")]
    [SerializeField] float fireRate = .1f;
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

        if (Input.GetMouseButton(1))
        {  
            isHipFiring = false;
        } 
        else { isHipFiring = true;}
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
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit)) 
        {
            canFire = false;
            if (isHipFiring)
            {
                Vector3 modifiedHitPoint = ReduceHipFireAccuracy(hit);
                Vector3 hitPointDirection = modifiedHitPoint - bulletTransform.position;
                hitPointDirection.Normalize();
                bulletTransform.forward = hitPointDirection;
                GetPooledBullet();
            } 
            else
            {
                Vector3 hitPointDirection = hit.point - bulletTransform.position;
                hitPointDirection.Normalize();
                bulletTransform.forward = hitPointDirection;
                GetPooledBullet();
            }
        }
    }

    Vector3 ReduceHipFireAccuracy(RaycastHit hit)
    {
        Vector3 modifiedHitPoint = hit.point;
        hipFireBulletSpreadRadius = 5f;
        Vector3 randomPoint = Random.insideUnitSphere * hipFireBulletSpreadRadius;
        modifiedHitPoint += randomPoint;
        Debug.Log(modifiedHitPoint);
        return modifiedHitPoint;
    }
    

    void GetPooledBullet()
    {
        PlayerBullet playerBullet = ObjectPool.instance.GetPooledBullet();
        if (playerBullet != null)
        {
            playerBullet.transform.position = bulletTransform.position;
            playerBullet.transform.rotation = bulletTransform.rotation;
            playerBullet.gameObject.SetActive(true);
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
