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

    //sets the health slider and the opacity of the blood panel in relation to the players current health
    void OnUpdatePlayerHealth(float health)
    {
        healthSlider.value = health;
        SetBloodPanelOpacity(health);
    }   

    //sets the opacity of the blood panel
    void SetBloodPanelOpacity(float health)
    {
        Color panelColor = bloodPanel.color;
        float opacity = 1f - (float)health / 100f;
        panelColor.a = opacity;
        bloodPanel.color = panelColor;
    }

}
