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
            SceneManager.LoadScene(0);
        }
    }

    public void RestartGame()
    {
        // Restarts the game all over.
        ScoreManager.docCount = 0;
        SceneManager.LoadScene(2);
    }

    public void ContinueGame()
    {
        Debug.Log("Taking you to the final MiniGame!");
        SceneManager.LoadScene("");
    }

    public void QuitGame()
    {
        Debug.Log("Quitting Game");
        Application.Quit();
    }
}
