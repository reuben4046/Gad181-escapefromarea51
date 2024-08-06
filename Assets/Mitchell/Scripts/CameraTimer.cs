using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CameraTimer : MonoBehaviour
{
    float secondtimer;
    public int second;
    [SerializeField] public float clockspeed = 1;
    public TextMeshProUGUI timerText;

    // Start is called before the first frame update
    void Start()
    {
        // clocktext = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        secondtimer += Time.deltaTime * clockspeed;

        if (secondtimer >= 1f)
        {
            second--;

            secondtimer = 0;

            timerText.text = $"Time Remaining: {second}";
        }
    }
}
