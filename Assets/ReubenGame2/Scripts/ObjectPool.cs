using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{

    public static ObjectPool instance;

    [Header("PlayerBullet")]
    [SerializeField] Transform playerBulletParent;
    [SerializeField] private PlayerBullet playerBulletPrefab;
    private List<PlayerBullet> pooledPlayerBullets = new List<PlayerBullet>();
    private int playerBulletAmountToPool = 50;


    [Header("EnemyBullet")]
    [SerializeField] Transform enemyBulletParent;
    [SerializeField] private EnemyBulletRP enemyBulletPrefab;
    private List<EnemyBulletRP> pooledEnemyBullets = new List<EnemyBulletRP>();
    private int enemyBulletAmountToPool = 50;

    // [Header("Enemy")]
    // //[SerializeField] Transform enemyParent;
    // [SerializeField] private EnemyStateManager enemyPrefab;
    // private List<EnemyStateManager> pooledEnemies = new List<EnemyStateManager>();
    // private int enemyAmountToPool = 5;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    void Start()
    {
        for (int i = 0; i < playerBulletAmountToPool; i++)
        {
            PlayerBullet playerBullet = Instantiate(playerBulletPrefab, playerBulletParent);
            playerBullet.gameObject.SetActive(false);
            pooledPlayerBullets.Add(playerBullet);
        }
        for (int i = 0; i < enemyBulletAmountToPool; i++)
        {
            EnemyBulletRP enemyBullet = Instantiate(enemyBulletPrefab, enemyBulletParent);
            enemyBullet.gameObject.SetActive(false);
            pooledEnemyBullets.Add(enemyBullet);
        }
        //for (int i = 0; i < enemyAmountToPool; i++)
        //{
        //    EnemyStateManager enemy = Instantiate(enemyPrefab);
        //    enemy.gameObject.SetActive(false);
        //    pooledEnemies.Add(enemy);
        //}
    }

    public PlayerBullet GetPooledPlayerBullet()
    {
        for (int i = 0; i < pooledPlayerBullets.Count; i++)
        {
            if (!pooledPlayerBullets[i].gameObject.activeInHierarchy)
            {
                return pooledPlayerBullets[i];
            }
        }

        return null;
    }

    public EnemyBulletRP GetPooledEnemyBullet()
    {
        for (int i = 0; i < pooledEnemyBullets.Count; i++)
        {
            if (!pooledEnemyBullets[i].gameObject.activeInHierarchy)
            {
                return pooledEnemyBullets[i];
            }
        }

        return null;
    }

    // public EnemyStateManager GetPooledEnemy()
    // {
    //     for (int i = 0; i < pooledEnemies.Count; i++)
    //     {
    //         if (!pooledEnemies[i].gameObject.activeInHierarchy)
    //         {
    //             return pooledEnemies[i];
    //         }
    //     }

    //     return null;
    // }
}
