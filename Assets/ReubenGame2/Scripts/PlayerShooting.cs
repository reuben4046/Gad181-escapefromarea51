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
    public PlayerBullet PlayerBullet;
    public Transform bulletTransform;

    List<PlayerBullet> bulletList = new List<PlayerBullet>();

    [SerializeField] PlayerGunRP playerGun;

    private bool isHipFiring = true;

    [SerializeField] float fireRate = .1f;
    bool canFire = true;
    float shootingTimer;

    //LeanTween variables
    float neutralZ = 0.91f; 
    float kickbackZ = 0.82f;

    private void Update() 
    {
        FireTimer();
        Debug.Log(isHipFiring);
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
        //next thing to add is reduced accuracy when the player hipfires. 
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
                GetPooledBullet();
            } 
            else
            {
                Vector3 hitPointDirection = hit.point - bulletTransform.position;
                hitPointDirection.Normalize();
                GetPooledBullet();
            }

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

    Vector3 ReduceHipFireAccuracy(RaycastHit hit)
    {
        float randX = Random.Range(1f, 1f);
        float randY = Random.Range(1f, 1f);
        Vector3 modifiedHitPoint = hit.point;
        modifiedHitPoint += new Vector3(randX, randY, 0f);
        return modifiedHitPoint;
    }
    

}
