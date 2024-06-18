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
        if (collision.gameObject.tag == "Doc")
        {
            docPickup++;
            Debug.Log(docPickup);
            Debug.Log("Evidence Picked Up!");
            if (docPickup >= 5)
            {
                SceneManager.LoadScene(1);
            }
        }
    }

}
