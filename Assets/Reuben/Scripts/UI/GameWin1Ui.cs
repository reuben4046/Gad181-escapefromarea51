using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameWin1Ui : MonoBehaviour
{
    public void OnContinueButton()
    {
        SceneManager.LoadScene("Game4");
    }
}
