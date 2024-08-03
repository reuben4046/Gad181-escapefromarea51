using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthRP : MonoBehaviour {
    //slider showing player health
    public Slider healthBar;
    
    //health bar value 100%
    float healthBarValue = 1.0f;

    //subscribing to the event and unsubscribing from the event
    void OnEnable() {
        EventSystemRP.OnPlayerHit += OnPlayerHit;
    }

    void OnDisable(){
        EventSystemRP.OnPlayerHit -= OnPlayerHit;
    }

    //decreases the health bar value
    private void OnPlayerHit() {
        DecreaseHealthBarValue();
    } 

    //decreases the health bar value
    private void DecreaseHealthBarValue() {
        healthBarValue -= 0.34f;
        healthBar.value = healthBarValue;
        if (healthBarValue <= 0) {
            EventSystemRP.OnPlayerHealthZero?.Invoke();
        }
    }
}
