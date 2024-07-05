using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionSpawner : MonoBehaviour
{

    public GameObject explosionPrefab;

    private GameObject explosionAnimation;

    private float explosionTime = 1f;

    void OnEnable()
    {
        EventSystemRP.OnGetLastPosition += OnGetLastPosition;
    }

    void OnDisable()
    {
        EventSystemRP.OnGetLastPosition -= OnGetLastPosition;
    }

    private void OnGetLastPosition(Vector2 position)
    {
        float randomNumber = Random.Range(0f, 360f);
        explosionAnimation = Instantiate(explosionPrefab, position, Quaternion.Euler(0f, 0f, randomNumber));

        StartCoroutine(DestroyExplosionPrefab());
    }

    IEnumerator DestroyExplosionPrefab()
    {
        yield return new WaitForSeconds(explosionTime);
        Destroy(explosionAnimation);
    }
}
