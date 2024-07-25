using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{

    public static ObjectPool instance;

    private List<PlayerBullet> pooledBullets = new List<PlayerBullet>();
    private int bulletAmountToPool = 50;

    [SerializeField] private PlayerBullet bulletPrefab;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    void Start()
    {
        for (int i = 0; i < bulletAmountToPool; i++)
        {
            PlayerBullet playerBullet = Instantiate(bulletPrefab);
            playerBullet.gameObject.SetActive(false);
            pooledBullets.Add(playerBullet);
        }
    }

    public PlayerBullet GetPooledBullet()
    {
        for (int i = 0; i < pooledBullets.Count; i++)
        {
            if (!pooledBullets[i].gameObject.activeInHierarchy)
            {
                return pooledBullets[i];
            }
        }

        return null;
    }
}
