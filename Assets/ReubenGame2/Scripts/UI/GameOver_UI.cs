using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    public void OnRestartButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void OnMainMenuButton()
    {
        SceneManager.LoadScene("Title");
    }
}
