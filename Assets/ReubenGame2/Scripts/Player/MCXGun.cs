using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Analytics;
using MoreMountains.Feedbacks;

public class MCXGun : MonoBehaviour
{
    //feel
    [SerializeField] MMFeedbacks mMFeedbacks;
    //LeanTween variables
    Vector3 gunWalkingPos = new Vector3(0.42f, -0.45f, 0.91f);
    Vector3 gunWalkingRot = new Vector3(-0.6f, -0.75f, 0f);
    Vector3 gunAimingPos = new Vector3(0f, -0.36f, 0.91f);
    Vector3 gunAimingRot = new Vector3(-0.88f, 0f, 0f);
    //shooting kickback
    float neutralZ = 0.91f; 
    float kickbackZ = 0.82f;

    [SerializeField] GameObject playerGun;
    [SerializeField] PlayerBullet bulletPrefab;
    [SerializeField] Transform bulletTransform;
    [SerializeField] Camera playerCam;

    private bool gunTweeningToAim = false;
    private bool gunTweeningToWalk = false;
    float tweenTime = .25f;
    private int leanTweenMoveID;
    private int leanTweenRotateID;

    [Header("Fire Rate")]
    [SerializeField] float fireRate = .1f;
    bool canFire = true;
    float shootingTimer;

    [SerializeField] ParticleSystem muzzleFlash;

    //HipFiring varables
    private bool isHipFiring = true;
    [SerializeField] float hipFireAccuracyRadius = 10f;

    //Sprinting variables
    bool canAim = true;

    // Start is called before the first frame update
    void Start() 
    {
        GoToWalkPos();        
    }
    //sets the gun to walk position at the start of the game
    private void GoToWalkPos() 
    {
        playerGun.transform.localPosition = gunWalkingPos;
        playerGun.transform.localRotation = Quaternion.Euler(gunWalkingRot);
    }

    void OnEnable()
    {
        FPSGameEvents.OnPlayerSprinting += OnPlayerSprinting;
    }

    void OnDisable()
    {
        FPSGameEvents.OnPlayerSprinting -= OnPlayerSprinting;
    }

    void OnPlayerSprinting(bool isSprinting)
    {
        //switches can aim and cant aim depending on if the player is sprinting
        canAim = !isSprinting;
    }

    // Update is called once per frame
    void Update() 
    {
        FireTimer();
        //checks if the left mouse button is pressed
        if (Input.GetMouseButton(0) && canFire) 
        {        
            FPSGameEvents.OnPlayPlayerGunShot?.Invoke();
            mMFeedbacks.PlayFeedbacks();
            Debug.DrawRay(bulletTransform.position, bulletTransform.forward, Color.red, 10f);   
            Shoot();
            ShootingTweenShake();
        }

        //checks if the right mouse button down
        if (Input.GetMouseButtonDown(1) && !gunTweeningToAim && canAim) 
        {   
            FPSGameEvents.OnPlayerAiming?.Invoke(true);
            TweenToAimPos();
            isHipFiring = false;
        }
        else if (Input.GetMouseButtonUp(1)) //if the right mouse button is released
        {
            FPSGameEvents.OnPlayerAiming?.Invoke(false);
            LeanTween.cancel(leanTweenMoveID);
            LeanTween.cancel(leanTweenRotateID);
            gunTweeningToAim = false;
            TweenToWalkPos();
            isHipFiring = true;
        }
    }

    //timer for the fire rate
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

    //shoots the bullet if can fire is true
    //calls functions that decrease accuracy if hipfiring is true
    void Shoot() 
    {
        if (canFire) 
        {
            PlayerBullet bullet = GetPooledBullet(); 
            canFire = false;
            muzzleFlash.Play();
            bullet.transform.position = bulletTransform.position;            
            if (!isHipFiring)
            {
                Vector3 hitPoint = GetRayCastHitPoint();
                bullet.transform.LookAt(hitPoint);                
            }
            else
            {
                Vector3 hitPoint = GetRayCastHitPoint();
                Vector3 randomPoint = ReduceAccuracy(hitPoint);
                bullet.transform.LookAt(randomPoint);
            }
        }
    }

    //gets pooled bullet
    //sets the bullet to be active and returns it
    PlayerBullet GetPooledBullet()
    {
        PlayerBullet playerBullet = ObjectPool.instance.GetPooledPlayerBullet();
        if (playerBullet != null)
        {
            playerBullet.gameObject.SetActive(true);
            return playerBullet;
        } 
        else
        {
            return null;
        }
    }

    //returns an accuate hit point
    Vector3 GetRayCastHitPoint()
    {
        Physics.Raycast(playerCam.transform.position, playerCam.transform.forward, out RaycastHit hit);
        return hit.point;
    }

    //changes the location of the hitpoint found in the function above to a random location inside of the hipfire accuracy radius
    Vector3 ReduceAccuracy(Vector3 hitPoint)
    {
        Vector3 randomPoint = Random.insideUnitSphere * hipFireAccuracyRadius;
        hitPoint += randomPoint;
        return hitPoint;
    }

    //leanTweening to aim position
    private void TweenToAimPos() 
    {
        if (gunTweeningToAim) return;
        gunTweeningToAim = true;
        leanTweenMoveID = LeanTween.moveLocal(playerGun.gameObject, gunAimingPos, tweenTime)
                        .setEase(LeanTweenType.easeInOutSine)
                        .setOnComplete(() => gunTweeningToAim = false).id;
        leanTweenRotateID = LeanTween.rotateLocal(playerGun.gameObject, gunAimingRot, tweenTime)
                        .setEase(LeanTweenType.easeInOutSine)
                        .setOnComplete(() => gunTweeningToAim = false).id;
        
    }

    //leanTweening to walk position
    private void TweenToWalkPos() 
    {
        if (gunTweeningToWalk) return;
        gunTweeningToWalk = true;
        LeanTween.moveLocal(playerGun.gameObject, gunWalkingPos, tweenTime)
                .setEase(LeanTweenType.easeInOutSine)
                .setOnComplete(() => gunTweeningToWalk = false);
        LeanTween.rotateLocal(playerGun.gameObject, gunWalkingRot, tweenTime)
                .setEase(LeanTweenType.easeInOutSine)
                .setOnComplete(() => gunTweeningToWalk = false);
    }

    //shakes the gun while shooting
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
