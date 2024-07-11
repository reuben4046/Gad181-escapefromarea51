using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimerRP : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timerText;
    private float elapsedTime;

    private float savedTime = 0f;

    [SerializeField] private float spawnIncreaseWaitTime = 60f;


    // Start is called before the first frame update
    void Start()
    {
        elapsedTime = 0f;
        EventSystemRP.OnIncreaseSpawnAmmount?.Invoke();
    }

    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.deltaTime;
        int minutes = Mathf.FloorToInt(elapsedTime / 60);
        int seconds = Mathf.FloorToInt(elapsedTime % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);

        IncreaseSpawnAmmount();
    }

    //increases the spawn ammount if the elapsed time is greater than the saved time plus the spawn increase wait time
    private void IncreaseSpawnAmmount()
    {
        if (elapsedTime > savedTime + spawnIncreaseWaitTime)
        {
            savedTime = elapsedTime;
            EventSystemRP.OnIncreaseSpawnAmmount?.Invoke(); //invokes the onIncreaseSpawnAmmount event
        }
    }
}
