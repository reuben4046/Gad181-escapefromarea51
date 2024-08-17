using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameWinTimerRp : MonoBehaviour
{
    [SerializeField] GameModeInfo gameModeInfo;

    float gamePlayTime = 60f;

    void Start()
    {
        if (gameModeInfo.isStoryMode)
        {
            StartCoroutine(WaitTillGameWin());
        }
    }

    IEnumerator WaitTillGameWin()
    {
        yield return new WaitForSeconds(gamePlayTime);
        SceneManager.LoadScene("GameWin1RP");
    }
}
