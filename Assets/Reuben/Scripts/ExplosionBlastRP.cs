using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionBlastRP : MonoBehaviour
{
    public CircleCollider2D blastTrigger;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Missle")
        {
            var missile = other.gameObject.GetComponent<Missile>();
            EventSystemRP.OnCaughtInExplosion?.Invoke(missile);

            Destroy(gameObject, 0.2f);
        }
    }

    // IEnumerator WaitSeconds()
    // {
    //     yield return new WaitForSeconds(0.5f);
    //     blastTrigger.enabled = false;
    //     Debug.Log(!blastTrigger.enabled);
    // }
}
