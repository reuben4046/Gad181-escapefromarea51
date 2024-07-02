using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public float timeRemaining = 150;
    public bool timerIsRunning = false;
    public TextMeshProUGUI timeText;

    private void Start()
    {
        // Starts the timer automatically
        timerIsRunning = true;
        timeText = GetComponent<TextMeshProUGUI>();
       
    }

    void Update()
    {
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
            }
            else
            {
                //Debug.Log("Time has run out!");
                timeRemaining = 0;
                timerIsRunning = false;
            }
        }

        DisplayTime(timeRemaining);
    }

    void DisplayTime(float timeToDisplay)
    {
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timeText.text = string.Format("Time to Arrival // {0:00}:{1:00}", minutes, seconds);

    }
}
