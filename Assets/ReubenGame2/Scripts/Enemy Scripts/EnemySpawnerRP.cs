using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemySpawnerRP : MonoBehaviour
{

    PlayerTarget playerTarget;
    [SerializeField] List<Transform> spawnPoints = new List<Transform>();

    float spawnPointXPos1 = -22f;
    float spawnPointSwapRange = 20f;
    float swapWaitTime = 0.5f;

    int enemysSpawned = 0;

    [SerializeField] EnemyStateManager enemyPrefab;
    List<EnemyStateManager> enemies = new List<EnemyStateManager>();

    void Awake()
    {
        FPSGameEvents.OnPlayerSpawned += OnPlayerSpawned;
        FPSGameEvents.OnEnemyDeath += OnEnemyDeath;
    }

    void OnDisable()
    {
        FPSGameEvents.OnPlayerSpawned -= OnPlayerSpawned;
        FPSGameEvents.OnEnemyDeath -= OnEnemyDeath;
    }

    //removes an enemy from the list when an enemy dies
    void OnEnemyDeath(TargetRP target)
    {
        enemies.RemoveAt(0);
    }

    // Start is called before the first frame update
    void Start()
    {
        SetFirstPos();
        StartCoroutine(SpawnEnemies());
        StartCoroutine(CheckSpawnPos());
    }

    //setting the first position of the spawnpoints
    void SetFirstPos()
    {
        foreach (Transform point in spawnPoints)
        {
            point.position = new Vector3(spawnPointXPos1, point.position.y, point.position.z);
        }
    }

    //Spawning Enemies contiuously making sure there is always 3 enemies in the scene 
    IEnumerator SpawnEnemies()
    {
        while (true)
        {
            yield return new WaitForSeconds(2f);
            if (enemies.Count < 3)
            {
                EnemyStateManager enemy = InstanciateEnemy();
                Vector3 spawnPosition = GetRandomSpawnPoint();
                enemy.transform.position = spawnPosition;
                enemysSpawned++;
                GameWinCheck();
            }
        }
    }

    //checking if the player has won
    void GameWinCheck()
    {
        if (enemysSpawned >= 11)
        {
            SceneManager.LoadScene("WinScene");
        }
    }

    //instanciates an enemy and adds it to the list
    EnemyStateManager InstanciateEnemy()
    {
        EnemyStateManager enemy = Instantiate(enemyPrefab);
        enemies.Add(enemy);
        return enemy;
    }

    //returns a random spawnpoint for the enemy to be spawned at
    Vector3 GetRandomSpawnPoint()
    {
        Transform randomSpawnPoint = spawnPoints[Random.Range(0, spawnPoints.Count)];

        return randomSpawnPoint.position;
    }

    //Position Swapping
    IEnumerator CheckSpawnPos()
    {
        while (true)
        {
            yield return new WaitForSeconds(swapWaitTime);
            MoveSpawnPoints();
        }
    }

    //gets player reference when player spawned so that the spawnpoints can be swapped when the player gets close
    void OnPlayerSpawned(PlayerTarget player)
    {
        playerTarget = player;
    }

    //swaps spawn pos when the player gets too close 
    void MoveSpawnPoints()
    {
        foreach (Transform point in spawnPoints)
        {
            Vector3 distace = point.position - playerTarget.transform.position;
            if (distace.magnitude < spawnPointSwapRange)
            {
                point.position = new Vector3(SwitchSpawnPos(), point.position.y, point.position.z);
            }
        }
    }

    //swaps the position every time the function is called
    bool swap = false;
    float SwitchSpawnPos()
    {
        swap = !swap;
        return swap ? spawnPointXPos1 : spawnPointXPos1;
    }

}
