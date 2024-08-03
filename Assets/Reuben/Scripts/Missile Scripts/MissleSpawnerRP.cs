using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissleSpawnerRP : MonoBehaviour {
    //spawn position that the prefab is spawned at
    private Vector2 spawnPosition;

    [SerializeField] Transform missileHolder;

    //prefab reference
    public Missile misslePrefab;


    [Header("Gizmos")]
    [SerializeField] private float gizPositionX = 0f;
    [SerializeField] private float gizPositionY = 0f;


    [Header("Spawn")]

    [SerializeField] private float xRange = 25f;
    [SerializeField] private float yRange = 20f;

    [SerializeField] private float smallYRangeMax;
    [SerializeField] private float smallYRangeMin;

    [SerializeField] private float xBounds;

    //list of spawned missles
    public List<Missile> spawnedMissiles = new List<Missile>();

    //keeps coroutine running in a loop
    private bool corotineRunning = true;


    //ammount of missiles that can be spawned every spawnInterval Seconds
    private float spawnAmmount = 0f;
    private float spawnInterval = 3f;

    //max ammount of missiles that can be spawned 
    private float maxSpawnAmmount = 3f;

    //subscribing and unsubscribing to events
    private void OnEnable() {
        EventSystemRP.OnMissileDestroyed += OnMissileDestroyed;
        EventSystemRP.OnIncreaseSpawnAmmount += OnIncreaseSpawnAmmount;
    }

    private void OnDisable( ) {
        EventSystemRP.OnMissileDestroyed -= OnMissileDestroyed;
        EventSystemRP.OnIncreaseSpawnAmmount -= OnIncreaseSpawnAmmount;
    }



    //this function is called when the onincreaseSpawnAmmount event is triggered
    //increasing the ammount of spawned missles if it is less than maxSpawnAmmount
    private void OnIncreaseSpawnAmmount() {
        if (spawnAmmount < maxSpawnAmmount) {
            spawnAmmount ++;
        }
    }

    //start
    void Start() {
        StartCoroutine(SpawnMissles());
        xBounds = xRange - 17.5f;

        smallYRangeMax = yRange;
        smallYRangeMin = yRange - 10f;
    }

    //this function is called when the onmissiledestroyed event is triggered
    private void OnMissileDestroyed(Missile missile) {
        spawnedMissiles.Remove(missile);
    }


    //coroutine that spawns the missles every spawnInterval seconds
    private IEnumerator SpawnMissles() {
        //loops the coroutine
        while (corotineRunning) { 

            //waits SpawnInterval seconds
            yield return new WaitForSeconds(spawnInterval); 

            //loops spawnAmmount times
            for (int i = 0; i < spawnAmmount; i++) {
                SpawnMissle();
            }
        }
    }

    //spawns the missile prefab at a random spawn position 
    private void SpawnMissle() {
        spawnPosition = PickRandomSpawn();
        Missile missile = Instantiate(misslePrefab, spawnPosition, Quaternion.identity, missileHolder);
        spawnedMissiles.Add(missile);

        EventSystemRP.OnMissileSpawned?.Invoke(missile);
    }
    
    //Picks a random spawn position picking x first and if the x is too close to 0, then a y is picked that will make it so that the spawn 
    //position is still out of the veiw of the camera. 
    private Vector2 PickRandomSpawn() {
        float randX = Random.Range(-xRange, xRange);
        float randY;
        if (randX < xBounds && randX > -xBounds) {
            bool positiveY = Random.Range(0, 2) == 1;
            if (positiveY) {
                randY = Random.Range(smallYRangeMin, smallYRangeMax);
            } else {
                randY = Random.Range(-smallYRangeMin, -smallYRangeMax);
            }
        } else {
            randY = Random.Range(-yRange, yRange);
        }

        return new Vector2(transform.position.x + randX, transform.position.y + randY);
    }

    //gizmos used for figuring out and visualisng the spawn area coordinates
    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, new Vector3(xRange*2, yRange*2, 0));

        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(transform.position, new Vector3(xRange * 2 - xBounds, yRange * 2 - xBounds, 0));

        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(spawnPosition, 0.5f);

        Gizmos.color = Color.green;
        Gizmos.DrawSphere(new Vector2(gizPositionX, gizPositionY), 0.2f);
    }

}
