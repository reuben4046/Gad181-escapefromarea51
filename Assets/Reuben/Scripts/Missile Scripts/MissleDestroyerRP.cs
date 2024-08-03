using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileDestroyerRP : MonoBehaviour {
    //this script controlls what happens when the missile collides with something

    Vector2 lastPosition; //last possition of the missile when it collides with something

    public Missile missile;

    private void OnCollisionEnter2D(Collision2D collision) {

        //if the missile collides with the player
        if (collision.gameObject.tag == "Player") {
            EventSystemRP.OnPlayerHit?.Invoke();
        }

        //what the missile does everytime it collides with something
        EventSystemRP.OnPlayExplosionSound?.Invoke();

        lastPosition = collision.GetContact(0).point;
        EventSystemRP.OnGetLastPosition?.Invoke(lastPosition);

        //broadcasts the event
        EventSystemRP.OnMissileDestroyed?.Invoke(missile);
        
        Destroy(gameObject);
    }


    //this is not used in my game currently however I hope to implement an explosion blast radius in project 3
    //
    //
    //subscribing and unsubscribing to events
    // private void OnEnable() {
    //     EventSystemRP.OnCaughtInExplosion += OnCaughtInExplosion;
    // }

    // private void OnDisable() {
    //     EventSystemRP.OnCaughtInExplosion -= OnCaughtInExplosion;
    // }

    // private void OnCaughtInExplosion(Missile missile) {
    //     EventSystemRP.OnMissileDestroyed?.Invoke(missile);
    //     Destroy(gameObject);
    // }

}
