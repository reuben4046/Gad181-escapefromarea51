using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimerRP : MonoBehaviour {
    //this script manages the timer 

    //public reference to saved information which is a scriptable object that I keep in my project
    public SavedInformationRP savedInformation;

    [SerializeField] private TextMeshProUGUI timerText;
    
    private float elapsedTime;

    //saved time is the time that the player survived. this is saved in the saved information scriptable object
    private float savedTime = 0f;

    [SerializeField] private float spawnIncreaseWaitTime = 60f;

    bool updateTime = true;

    private void OnEnable() {
        EventSystemRP.OnPlayerHealthZero += OnPlayerHealthZero;
    }

    private void OnDisable() {
        EventSystemRP.OnPlayerHealthZero -= OnPlayerHealthZero;
    }

    private void OnPlayerHealthZero() {
        updateTime = false;
    }

    // Start is called before the first frame update
    void Start() {
        elapsedTime = 0f;
        EventSystemRP.OnIncreaseSpawnAmmount?.Invoke();
    }

    // Update is called once per frame
    void Update() {
        if (updateTime) {
            //increases the elapsed time by the delta time and converts the elapsed time to minutes and seconds
            elapsedTime += Time.deltaTime;
            int minutes = Mathf.FloorToInt(elapsedTime / 60);
            int seconds = Mathf.FloorToInt(elapsedTime % 60);
            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);

            savedInformation.timeSurvived = timerText.text;

            IncreaseSpawnAmmount();  
        }

    }

    //increases the spawn ammount if the elapsed time is greater than the saved time plus the spawn increase wait time
    private void IncreaseSpawnAmmount() {
        if (elapsedTime > savedTime + spawnIncreaseWaitTime) {
            savedTime = elapsedTime;
            EventSystemRP.OnIncreaseSpawnAmmount?.Invoke(); //invokes the onIncreaseSpawnAmmount event
        }
    }
}
