using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletRP : MonoBehaviour
{
    float speed = 100f;
    float damage = 10f;

    void Update()
    {
        //moving the bullet forward every frame
        transform.position += transform.forward * speed * Time.deltaTime;
    }
    
    void OnEnable()
    {
        StartCoroutine(DisableAfter(2f));
    }

    //disables the bullet after 2 seconds 
    IEnumerator DisableAfter(float time)
    {
        yield return new WaitForSeconds(time);
        gameObject.SetActive(false);
    }

    //called when the bullet hits something, and fires an event sending the damage to the player
    void OnTriggerEnter(Collider other)
    {
        gameObject.SetActive(false);
        PlayerTarget target = other.gameObject.GetComponent<PlayerTarget>();
        if (target != null)
        {
            FPSGameEvents.OnPlayerTargetHit.Invoke(damage);
        }
    }
}
