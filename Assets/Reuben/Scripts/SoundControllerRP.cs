using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundControllerRP : MonoBehaviour
{

    public AudioSource explosionSound;

    private void OnEnable()
    {
        EventSystemRP.OnPlayExplosionSound += OnPlayExplosionSound;
    }

    private void OnDisable()
    {
        EventSystemRP.OnPlayExplosionSound -= OnPlayExplosionSound;
    }

    private void OnPlayExplosionSound()
    {
        explosionSound.Play();
    }

}
