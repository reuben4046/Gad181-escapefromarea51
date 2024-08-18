using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOverScene : MonoBehaviour
{

    public SavedInformationRP savedInformation;

    public TextMeshProUGUI timeText;

    public void onClick()
    {
        SceneManager.LoadScene("ReubenMiniGame"); 
    }

    void Start()
    {
        timeText.text = "Time survived: " + savedInformation.timeSurvived;
    }

}
