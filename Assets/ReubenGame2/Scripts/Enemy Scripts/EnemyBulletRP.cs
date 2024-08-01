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
        StartCoroutine(DisableAfter(2f));
    }

    IEnumerator DisableAfter(float time)
    {
        yield return new WaitForSeconds(time);
        gameObject.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        gameObject.SetActive(false);
        PlayerTarget target = other.gameObject.GetComponent<PlayerTarget>();
        if (target != null)
        {
            FPSGameEvents.OnPlayerTargetHit.Invoke();
        }
    }
}
