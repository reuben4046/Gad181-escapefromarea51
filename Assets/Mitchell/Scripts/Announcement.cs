using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Announcement : MonoBehaviour
{
    public AudioSource audioSource;

    void Start()
    {
        //Fetch the AudioSource from the GameObject
        audioSource = GetComponent<AudioSource>();
    }

    public void OnTriggerEnter(Collider Col)
    {
        if (Col.tag == "Player" && !audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }
}
