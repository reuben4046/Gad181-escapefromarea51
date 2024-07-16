using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileDestroyerRP : MonoBehaviour
{

    Vector2 lastPosition;

    public Missile missile;

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.tag == "Player")
        {
            EventSystemRP.OnPlayerHit?.Invoke();
        }

        EventSystemRP.OnPlayExplosionSound?.Invoke();

        //broadcasts the event
        EventSystemRP.OnMissileDestroyed?.Invoke(missile);

        lastPosition = collision.GetContact(0).point;
        EventSystemRP.OnGetLastPosition?.Invoke(lastPosition);

        
        Destroy(gameObject);
    }

    private void OnEnable() 
    {
        EventSystemRP.OnCaughtInExplosion += OnCaughtInExplosion;
    }

    private void OnDisable() 
    {
        EventSystemRP.OnCaughtInExplosion -= OnCaughtInExplosion;
    }

    private void OnCaughtInExplosion(Missile missile)
    {
        EventSystemRP.OnMissileDestroyed?.Invoke(missile);
        Destroy(gameObject);
    }

}
