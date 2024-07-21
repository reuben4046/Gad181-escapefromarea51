using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionBlastRP : MonoBehaviour {
    public CircleCollider2D blastTrigger;

    // this script isnt actually used in my game however I hope to implement an explosion blast radius in project 3 
    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Missle") {
            var missile = other.gameObject.GetComponent<Missile>();
            EventSystemRP.OnCaughtInExplosion?.Invoke(missile);

            Destroy(gameObject, 1f);
        }
    }

}
