using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CameraSenses : MonoBehaviour
{

    public float viewRadius;
    public float viewAngle;
    public bool canSeePlayer = false;

    public LayerMask targetPlayer;
    public LayerMask obstacleMask;

    public GameObject player;

    public TextMeshProUGUI CameraSpot;
    public TextMeshProUGUI TimeRemaining;
    public Image CameraScreen;

    public bool startCapture = true;

    // Start is called before the first frame update
    void Start()
    {
        CameraSpot.enabled = false;
        TimeRemaining.enabled = false;
        CameraScreen.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 playerDirection = (player.transform.position - transform.position).normalized;

        if (Vector3.Angle(transform.forward, playerDirection) < viewAngle / 2)
        {
            Debug.Log("You're in angle!");
            float distanceToTarget = Vector3.Distance(transform.position, player.transform.position);
            if (distanceToTarget <= viewRadius)
            {
                Debug.Log("You're in distance!");
                if (Physics.Raycast(transform.position, playerDirection, distanceToTarget, obstacleMask) == false)
                {
                    Debug.Log("I can see you directly!!!");
                    
                    canSeePlayer = true;
                    CameraSpot.enabled = true;
                    TimeRemaining.enabled = true;
                    CameraScreen.enabled = true;
                    //StartCoroutine(WaitTillCaught());
                    StartCaptureTimer();
                }
                else
                {
                    timer = 6;
                    canSeePlayer = false;
                    //StopCoroutine(WaitTillCaught());
                    CameraSpot.enabled = false;
                    TimeRemaining.enabled = false;
                    CameraScreen.enabled = false;
                }
            }
        }
    }


    public TextMeshProUGUI timerText;

    float captureTime = 1;
    float timer = 6;
    void StartCaptureTimer()
    {
        timer -= Time.deltaTime;
        //timerText.text = $"Time Remaining: {timer}";
        if (timer < captureTime)
        {
            Debug.Log("CAUGHT!!");
            SceneManager.LoadScene(5);
        }
        timerText.SetText($"Time Remaining: {(int)timer}");
    }
}