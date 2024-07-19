using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyAnnouncement : MonoBehaviour
{
    public AudioSource audioSource;

    public void OnTriggerEnter(Collider Col)
    {
        if (Col.tag == "Player" && !audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }
}
