using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundControllerRP : MonoBehaviour {
    //this script plays all the sounds needed in the game through events, allthough at the moment the only sound that needs scripting is the explosion sound. 
    public AudioSource explosionSound;

    private void OnEnable() {
        EventSystemRP.OnPlayExplosionSound += OnPlayExplosionSound;
    }

    private void OnDisable() {
        EventSystemRP.OnPlayExplosionSound -= OnPlayExplosionSound;
    }

    private void OnPlayExplosionSound() {
        explosionSound.Play();
    }

}
