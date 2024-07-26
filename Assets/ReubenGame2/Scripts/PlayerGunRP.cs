using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGunRP : MonoBehaviour
{
    //LeanTween variables
    Vector3 gunWalkingPos = new Vector3(0.42f, -0.45f, 0.91f);

    Vector3 gunWalkingRot = new Vector3(-0.6f, -0.75f, 0f);
    Vector3 gunAimingPos = new Vector3(0f, -0.36f, 0.91f);
    Vector3 gunAimingRot = new Vector3(-0.88f, 0f, 0f);

    [SerializeField] GameObject playerGun;


    private bool gunTweeningToAim = false;
    private bool gunTweeningToWalk = false;
    float tweenTime = .25f;
    private int leanTweenMoveID;
    private int leanTweenRotateID;


    // Start is called before the first frame update
    void Start() {
        GoToWalkPos();

    }

    private void GoToWalkPos() {
        playerGun.transform.localPosition = gunWalkingPos;
        playerGun.transform.localRotation = Quaternion.Euler(gunWalkingRot);
    }

    // Update is called once per frame
    void Update() {        
        if (Input.GetMouseButtonDown(1) && !gunTweeningToAim) {   
            TweenToAimPos();
        }
        else if (Input.GetMouseButtonUp(1)) {
            LeanTween.cancel(leanTweenMoveID);
            LeanTween.cancel(leanTweenRotateID);
            gunTweeningToAim = false;
            TweenToWalkPos();
        }

        FireTimer();
        if (Input.GetMouseButton(0) && canFire) 
        {
            Shoot();
            ShootingTweenShake();
        } 
    }


    private void TweenToAimPos() {
        if (gunTweeningToAim) return;
        gunTweeningToAim = true;
        leanTweenMoveID = LeanTween.moveLocal(playerGun.gameObject, gunAimingPos, tweenTime)
                        .setEase(LeanTweenType.easeInOutSine)
                        .setOnComplete(() => gunTweeningToAim = false).id;
        leanTweenRotateID = LeanTween.rotateLocal(playerGun.gameObject, gunAimingRot, tweenTime)
                        .setEase(LeanTweenType.easeInOutSine)
                        .setOnComplete(() => gunTweeningToAim = false).id;
        
    }

    private void TweenToWalkPos() {
        if (gunTweeningToWalk) return;
        gunTweeningToWalk = true;
        LeanTween.moveLocal(playerGun.gameObject, gunWalkingPos, tweenTime)
                .setEase(LeanTweenType.easeInOutSine)
                .setOnComplete(() => gunTweeningToWalk = false);
        LeanTween.rotateLocal(playerGun.gameObject, gunWalkingRot, tweenTime)
                .setEase(LeanTweenType.easeInOutSine)
                .setOnComplete(() => gunTweeningToWalk = false);
    }

    [Header("References")]
    [SerializeField] PlayerBullet PlayerBullet;
    [SerializeField] Transform bulletTransform;
    [SerializeField] ParticleSystem muzzleFlash;

    [Header("HipFire")]    


    [Header("Fire Rate")]
    [SerializeField] float fireRate = .5f;
    bool canFire = true;
    float shootingTimer;

    //LeanTween variables
    float neutralZ = 0.91f; 
    float kickbackZ = 0.82f;

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
