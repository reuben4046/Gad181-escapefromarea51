using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public Text coinText;
    public static int coinCount;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Displays how many coins you have in the UI
        coinText.text = "Coins: " + Mathf.Round(coinCount) + "/5";
    }
}
