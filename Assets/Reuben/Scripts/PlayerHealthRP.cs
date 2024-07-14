using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthRP : MonoBehaviour
{

    public Slider healthBar;

    float healthBarValue = 1.0f;

    void OnEnable()
    {
        EventSystemRP.OnPlayerHit += OnPlayerHit;
    }

    void OnDisable()
    {
        EventSystemRP.OnPlayerHit -= OnPlayerHit;
    }

    private void OnPlayerHit()
    {
        DecreaseHealthBarValue();
    } 

    private void DecreaseHealthBarValue()
    {
        healthBarValue -= 0.34f;
        healthBar.value = healthBarValue;
        if (healthBarValue <= 0)
        {
            EventSystemRP.OnPlayerHealthZero?.Invoke();
        }
    }
}
