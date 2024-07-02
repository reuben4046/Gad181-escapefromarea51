using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Chapter : MonoBehaviour
{

    public void BeginChapter()
    {
        SceneManager.LoadScene("Add Number Here");
        Debug.Log("Beginning Chapter");
    }
    public void QuitGame()
    {
        Debug.Log("Quitting Game");
        Application.Quit();
    }
}
