using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerGunRP : MonoBehaviour
{
    Quaternion walkRotation = Quaternion.Euler(0f, 0f, 0f);
    Quaternion aimRotation = Quaternion.Euler(-0.73f, 0f, 0f);

    //LeanTween variables
    Vector3 gunWalkingPos = new Vector3(0.42f, -0.45f, 0.92f);
    Vector3 gunAimingPos = new Vector3(0f, -0.36f, 0.91f);
    Vector3 gunAimingRot = new Vector3(-0.73f, 0f, 0f);

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
        transform.localPosition = gunWalkingPos;
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
        leanTweenMoveID = LeanTween.moveLocal(gameObject, gunAimingPos, tweenTime)
                        .setEase(LeanTweenType.easeInOutSine)
                        .setOnComplete(() => gunTweeningToAim = false).id;
        leanTweenRotateID = LeanTween.rotateLocal(gameObject, gunAimingRot, tweenTime)
                        .setEase(LeanTweenType.easeInOutSine)
                        .setOnComplete(() => gunTweeningToAim = false).id;
    }

    private void TweenToWalkPos() {
        if (gunTweeningToWalk) return;
        gunTweeningToWalk = true;
        LeanTween.moveLocal(gameObject, gunWalkingPos, tweenTime)
                .setEase(LeanTweenType.easeInOutSine)
                .setOnComplete(() => gunTweeningToWalk = false);
    }
}
