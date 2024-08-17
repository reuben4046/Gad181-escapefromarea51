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

    public void Tutorial()
    {
        Debug.Log("Taking you to the Tutorial");
        SceneManager.LoadScene(15);
    }

    public void ChapterSelect()
    {
        Debug.Log("Taking you to the Chapter Select");
        SceneManager.LoadScene(20);
    }

    public void Back()
    {
        Debug.Log("Taking you back to the Title screen");
        SceneManager.LoadScene(0);
    }

    public void Tutorial1()
    {
        Debug.Log("Taking you to the First Tutorial");
        SceneManager.LoadScene(16);
    }

    public void Tutorial2()
    {
        Debug.Log("Taking you to the Second Tutorial");
        SceneManager.LoadScene(17);
    }

    public void Tutorial3()
    {
        Debug.Log("Taking you to the Third Tutorial");
        SceneManager.LoadScene(18);
    }

    public void Tutorial4()
    {
        Debug.Log("Taking you to the Forth Tutorial");
        SceneManager.LoadScene(19);
    }
}
