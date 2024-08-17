using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStartSceneRP : MonoBehaviour
{
    [SerializeField] GameModeInfo gameModeInfo;

    public void OnStoryModeButton()
    {
        gameModeInfo.isStoryMode = true;
        SceneManager.LoadScene("ReubenMiniGame");
    }

    public void OnEndlessModeButton()
    {
        gameModeInfo.isStoryMode = false;
        SceneManager.LoadScene("ReubenMiniGame");
    }
}
