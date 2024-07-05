using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileDestroyerRP : MonoBehaviour
{

    Vector2 lastPosition;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //broadcasts the event
        EventSystemRP.OnMissileDestroyed?.Invoke(this.gameObject);

        lastPosition = collision.GetContact(0).point;
        EventSystemRP.OnGetLastPosition?.Invoke(lastPosition);
        
        Destroy(gameObject);

    }
}
