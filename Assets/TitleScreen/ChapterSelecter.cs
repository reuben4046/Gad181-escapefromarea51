using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChapterSelecter : MonoBehaviour
{
    public void Game1()
    {
        Debug.Log("Taking you to the First Game");
        SceneManager.LoadScene("Game1");
    }

    public void Game2()
    {
        Debug.Log("Taking you to the Second Game");
        SceneManager.LoadScene("Game2");
    }

    public void Game3()
    {
        Debug.Log("Taking you to the Third Game");
        SceneManager.LoadScene("Game3");
    }

    public void Game4()
    {
        Debug.Log("Taking you to the Forth Game");
        SceneManager.LoadScene("Game4");
    }
}
