using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class YouWon : MonoBehaviour
{
    private int docPickup;

    // Collition code if the Player collects all 5 Coins to win.
    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Document")
        {
            docPickup++;
            Debug.Log(docPickup);
            if (docPickup >= 6)
            {
                SceneManager.LoadScene("YouWon");
            }
        }
    }

}
