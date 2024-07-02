using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileDestroyerRP : MonoBehaviour
{

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //broadcasts the event
        EventSystemRP.OnMissileDestroyed?.Invoke(this.gameObject);


        Destroy(gameObject);

    }
}
