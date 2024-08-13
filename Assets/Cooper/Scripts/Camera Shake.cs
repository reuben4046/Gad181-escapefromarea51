using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [SerializeField] private float shakeDelayMin = 0.1f;
    [SerializeField] private float shakeDelayMax = 2f;

    [SerializeField] private float shakedistanceMax = 10f;

    [SerializeField] private float shakeTimeMin = 0.25f;
    [SerializeField] private float shakeTimeMax = 1f;


    // Start is called before the first frame update
    void Start()
    {
        Shake();
    }

    private void Shake () 
    {
        float shakeDelay = Random.Range(shakeDelayMin, shakeDelayMax);
        float shakeDistance = Random.Range(-shakedistanceMax, shakedistanceMax);
        float shakeTime = Random.Range(shakeTimeMin, shakeTimeMax);
        float shakeToY = transform.position.y +shakeDistance;

        LeanTween.moveY(gameObject, shakeToY, shakeTime)

                    .setDelay(shakeDelay)
                    .setEaseOutBounce()
                    .setLoopPingPong()
                    .setOnComplete(() => Shake());



    }
}
