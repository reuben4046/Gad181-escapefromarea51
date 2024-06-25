using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Caught : MonoBehaviour
{
    public AudioSource playsound;
    private void OnTriggerEnter(Collider other)
    {
        SceneManager.LoadScene(0);
        playsound.Play();
    }

    public void RestartGame()
    {
        // Restarts the game all over.
        ScoreManager.docCount = 0;
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        Debug.Log("Quitting Game");
        Application.Quit();
    }
}
