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
            SceneManager.LoadScene("Caught");
        }
    }

    public void RestartGame()
    {
        // Restarts the game all over.
        ScoreManager.docCount = 0;
        SceneManager.LoadScene("Mitchell's MiniGame");
    }

    public void ContinueGame()
    {
        Debug.Log("Taking you to the final MiniGame!");
        SceneManager.LoadScene("Game3");
    }

    public void QuitGame()
    {
        Debug.Log("Quitting Game");
        Application.Quit();
    }

    public void GameStart()
    {
        Debug.Log("Taking you to the First MiniGame!");
        SceneManager.LoadScene("Game1");
    }

    public void Tutorial()
    {
        Debug.Log("Taking you to the Tutorial");
        SceneManager.LoadScene("HowToPlay");
    }

    public void ChapterSelect()
    {
        Debug.Log("Taking you to the Chapter Select");
        SceneManager.LoadScene("ChapterSelect");
    }

    public void Back()
    {
        Debug.Log("Taking you back to the Title screen");
        SceneManager.LoadScene("Title");
    }

    public void Tutorial1()
    {
        Debug.Log("Taking you to the First Tutorial");
        SceneManager.LoadScene("Tutorial 1");
    }

    public void Tutorial2()
    {
        Debug.Log("Taking you to the Second Tutorial");
        SceneManager.LoadScene("Tutorial 2");
    }

    public void Tutorial3()
    {
        Debug.Log("Taking you to the Third Tutorial");
        SceneManager.LoadScene("Tutorial 3");
    }

    public void Tutorial4()
    {
        Debug.Log("Taking you to the Forth Tutorial");
        SceneManager.LoadScene("Tutorial 4");
    }
}
