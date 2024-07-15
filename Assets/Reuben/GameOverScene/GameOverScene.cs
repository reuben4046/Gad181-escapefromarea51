using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScene : MonoBehaviour
{
    public void onClick()
    {
        SceneManager.LoadScene("ReubenMiniGame"); 
    }
}
