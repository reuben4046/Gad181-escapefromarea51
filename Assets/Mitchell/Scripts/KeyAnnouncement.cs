using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyAnnouncement : MonoBehaviour
{
    public AudioSource audioClip;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void OnTriggerEnter(Collider Col)
    {
        if (Col.tag == "Player")
        {
            audioClip.Play();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
