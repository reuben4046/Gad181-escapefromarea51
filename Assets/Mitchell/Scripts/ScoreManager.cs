using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public Text docText;
    public Text docTextOutline;
    public static int docCount;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Displays how many coins you have in the UI
        docText.text = "Docs: " + Mathf.Round(docCount) + "/6";
        docTextOutline.text = "Docs: " + Mathf.Round(docCount) + "/6";
    }
}
