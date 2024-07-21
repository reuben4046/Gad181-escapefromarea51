using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionSpawner : MonoBehaviour {
    public GameObject explosionPrefab;

    private GameObject explosionAnimation;
    
    [SerializeField] Transform ExplosionHolder;

    private float explosionTime = 0.5f;


    void OnEnable() {
        EventSystemRP.OnGetLastPosition += OnGetLastPosition;
    }

    void OnDisable() {
        EventSystemRP.OnGetLastPosition -= OnGetLastPosition;
    }

    //gets the point of contact on the collision of the missile and then spawns the explosion at that point with a random rotation
    private void OnGetLastPosition(Vector2 position) {
        float randomNumber = Random.Range(0f, 360f);
        explosionAnimation = Instantiate(explosionPrefab, position, Quaternion.Euler(0f, 0f, randomNumber), ExplosionHolder);
        
        Destroy(explosionAnimation, explosionTime); //destroys the explosion animation after explosionTime seconds 
    }

}
