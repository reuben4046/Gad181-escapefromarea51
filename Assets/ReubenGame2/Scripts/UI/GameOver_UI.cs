using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver_UI : MonoBehaviour
{
    [SerializeField] GameObject gameOverPanel;

    void OnEnable()
    {
        FPSGameEvents.OnPlayerDeath += OnPlayerDeath;
    }

    void OnDisable()
    {
        FPSGameEvents.OnPlayerDeath -= OnPlayerDeath;
    }

    void OnPlayerDeath()
    {
        gameOverPanel.SetActive(true);
    }
}
