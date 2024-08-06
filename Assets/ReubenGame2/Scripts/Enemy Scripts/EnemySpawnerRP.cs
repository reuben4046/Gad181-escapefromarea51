using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerRP : MonoBehaviour
{

    PlayerTarget playerTarget;
    [SerializeField] List<Transform> spawnPoints = new List<Transform>();

    float spawnPointXPos = -22f;
    float spawnPointSwapRange = 20f;
    float swapWaitTime = 0.5f;

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

    void OnEnemyDeath(TargetRP target)
    {
        enemies.RemoveAt(0);
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnEnemies());
        StartCoroutine(CheckSpawnPos());
    }


    //Spawning Enemies
    IEnumerator SpawnEnemies()
    {
        while (true)
        {
            yield return new WaitForSeconds(2f);
            if (enemies.Count < 3)
            {
                // EnemyStateManager enemy = GetPooledEnemy();
                EnemyStateManager enemy = InstanciateEnemy();
                Vector3 spawnPosition = GetRandomSpawnPoint();
                enemy.transform.position = spawnPosition;
            }
        }
    }

    EnemyStateManager InstanciateEnemy()
    {
        EnemyStateManager enemy = Instantiate(enemyPrefab);
        enemies.Add(enemy);
        return enemy;
    }

    //had to remove pooling for enemies due to issues with pooled enemis not receiving or sending events

    // EnemyStateManager GetPooledEnemy()
    // {
    //     EnemyStateManager enemy = ObjectPool.instance.GetPooledEnemy();
    //     if (enemy != null)
    //     {
    //         enemy.gameObject.SetActive(true);
    //         enemies.Add(enemy);
    //         return enemy;
    //     }

    //     return null;
    // }

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

    void OnPlayerSpawned(PlayerTarget player)
    {
        playerTarget = player;
    }

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

    bool swap = false;
    float SwitchSpawnPos()
    {
        swap = !swap;
        return swap ? spawnPointXPos : -spawnPointXPos;
    }

}
