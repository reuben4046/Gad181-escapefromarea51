using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour {    
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
        gameObject.SetActive(false);
        FPSGameEvents.OnTargetHit.Invoke(other.gameObject.GetComponent<Target>());
    }
}
