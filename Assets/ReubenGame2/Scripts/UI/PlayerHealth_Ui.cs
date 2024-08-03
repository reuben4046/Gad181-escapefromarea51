using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth_Ui : MonoBehaviour
{
    [SerializeField] Slider healthSlider;

    [SerializeField] Image bloodPanel;

    void Start()
    {
        healthSlider.maxValue = 100f;
        healthSlider.value = 100f;
    }

    void OnEnable()
    {
        FPSGameEvents.OnUpdatePlayerHealth += OnUpdatePlayerHealth;
    }

    void OnDisable()
    {
        FPSGameEvents.OnUpdatePlayerHealth -= OnUpdatePlayerHealth;
    }

    void OnUpdatePlayerHealth(float health)
    {
        healthSlider.value = health;
        SetBloodPanelOpacity(health);
    }   
         
    void SetBloodPanelOpacity(float health)
    {
        Color panelColor = bloodPanel.color;
        float opacity = 1f - (float)health / 100f;
        panelColor.a = opacity;
        bloodPanel.color = panelColor;
    }

}
