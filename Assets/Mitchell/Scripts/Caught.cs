using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Caught : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            SceneManager.LoadScene(5);
        }
    }

    public void RestartGame()
    {
        // Restarts the game all over.
        ScoreManager.docCount = 0;
        SceneManager.LoadScene(6);
    }

    public void ContinueGame()
    {
        Debug.Log("Taking you to the final MiniGame!");
        SceneManager.LoadScene(8);
    }

    public void QuitGame()
    {
        Debug.Log("Quitting Game");
        Application.Quit();
    }

    public void GameStart()
    {
        Debug.Log("Taking you to the First MiniGame!");
        SceneManager.LoadScene(1);
    }
}
