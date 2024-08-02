using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBar_Ui : MonoBehaviour
{
    [SerializeField] Slider healthSlider;

    void Start()
    {
        healthSlider.maxValue = 100f;
        healthSlider.value = 100f;
    }

    void OnEnable()
    {
        FPSGameEvents.OnPlayerTargetHit += OnPlayerTargetHit;
    }

    void OnDisable()
    {
        FPSGameEvents.OnPlayerTargetHit -= OnPlayerTargetHit;
    }

    void OnPlayerTargetHit(float damage)
    {
        healthSlider.value -= damage;
    }
}
