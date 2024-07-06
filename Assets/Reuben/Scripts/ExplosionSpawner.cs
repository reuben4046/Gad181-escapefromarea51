using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionSpawner : MonoBehaviour
{

    public GameObject explosionPrefab;

    private GameObject explosionAnimation;

    [SerializeField] Transform ExplosionHolder;

    //list of explosions (created so that I can destroy the oldest explosion when the list gets too long)
    private List<GameObject> explosionList = new List<GameObject>();

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
        explosionAnimation = Instantiate(explosionPrefab, position, Quaternion.Euler(0f, 0f, randomNumber), ExplosionHolder);
        explosionList.Add(explosionAnimation);

        destroyOldestExplosion();
    }

    private void destroyOldestExplosion()
    {
        if (explosionList.Count > 5)
        {
            Destroy(explosionList[0]);
            explosionList.RemoveAt(0);
        }
    }
}
