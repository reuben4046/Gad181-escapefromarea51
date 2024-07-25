using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerGunRP : MonoBehaviour
{
    //LeanTween variables
    Vector3 gunWalkingPos = new Vector3(0.42f, -0.45f, 0.91f);

    Vector3 gunWalkingRot = new Vector3(-0.6f, -0.75f, 0f);
    Vector3 gunAimingPos = new Vector3(0f, -0.36f, 0.91f);
    Vector3 gunAimingRot = new Vector3(-0.88f, 0f, 0f);

    [SerializeField] PlayerGunRP playerGunRP;


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
        playerGunRP.transform.localPosition = gunWalkingPos;
        playerGunRP.transform.localRotation = Quaternion.Euler(gunWalkingRot);
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
    }


    private void TweenToAimPos() {
        if (gunTweeningToAim) return;
        gunTweeningToAim = true;
        leanTweenMoveID = LeanTween.moveLocal(playerGunRP.gameObject, gunAimingPos, tweenTime)
                        .setEase(LeanTweenType.easeInOutSine)
                        .setOnComplete(() => gunTweeningToAim = false).id;
        leanTweenRotateID = LeanTween.rotateLocal(playerGunRP.gameObject, gunAimingRot, tweenTime)
                        .setEase(LeanTweenType.easeInOutSine)
                        .setOnComplete(() => gunTweeningToAim = false).id;
        
    }

    private void TweenToWalkPos() {
        if (gunTweeningToWalk) return;
        gunTweeningToWalk = true;
        LeanTween.moveLocal(playerGunRP.gameObject, gunWalkingPos, tweenTime)
                .setEase(LeanTweenType.easeInOutSine)
                .setOnComplete(() => gunTweeningToWalk = false);
        LeanTween.rotateLocal(playerGunRP.gameObject, gunWalkingRot, tweenTime)
                .setEase(LeanTweenType.easeInOutSine)
                .setOnComplete(() => gunTweeningToWalk = false);
    }


}
