using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileDestroyerRP : MonoBehaviour
{

    Vector2 lastPosition;

    public CircleCollider2D blastTrigger;

    private void Start()
    {
        blastTrigger.enabled = false;
    }

    private IEnumerator OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.tag == "Player")
        {
            EventSystemRP.OnPlayerHit?.Invoke();
        }

        EventSystemRP.OnPlayExplosionSound?.Invoke();

        blastTrigger.enabled = true;
        Debug.Log(!blastTrigger.enabled);

        //broadcasts the event
        EventSystemRP.OnMissileDestroyed?.Invoke(GetComponent<Missile>());

        lastPosition = collision.GetContact(0).point;
        EventSystemRP.OnGetLastPosition?.Invoke(lastPosition);

        yield return new WaitForSeconds(0.05f);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("inthetrigger");

        if (collision.gameObject.tag == "Missle")
        {
            EventSystemRP.OnPlayExplosionSound?.Invoke();

            blastTrigger.enabled = true;
            Debug.Log(!blastTrigger.enabled);

            //broadcasts the event
            EventSystemRP.OnMissileDestroyed?.Invoke(GetComponent<Missile>());

            lastPosition = transform.position;
            EventSystemRP.OnGetLastPosition?.Invoke(lastPosition);

            Destroy(gameObject);
        }
    }
}
