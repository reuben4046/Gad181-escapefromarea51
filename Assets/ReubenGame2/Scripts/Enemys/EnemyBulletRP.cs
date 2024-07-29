using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletRP : MonoBehaviour
{
    float speed = 100f;

    void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
    }
    
    void OnEnable()
    {
        StartCoroutine(DisableAfter(4f));
    }

    IEnumerator DisableAfter(float time)
    {
        yield return new WaitForSeconds(time);
        gameObject.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("HIT PLAYER");
        }
        gameObject.SetActive(false);
        // Target target = other.gameObject.GetComponent<Target>();
        // if (target != null)
        // {
        //     FPSGameEvents.OnTargetHit.Invoke(other.gameObject.GetComponent<Target>());        
        // }

    }
}
