using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ShootingState : BaseEnemyState
{
    //reference to next state
    public GoToCoverState goToCoverState;

    //NavMeshAgent
    [SerializeField] NavMeshAgent agentEnemy;
    //PlayerReference
    [SerializeField] Transform target;

    //shooting
    [SerializeField] Transform gunTransform;
    bool canFire = true;
    float fireRate = 0.5f;
    float shootingTimer = 0f;
    bool shootingStateActive;

    private void OnEnable()
    {
        shootingStateActive = true;
        StartCoroutine(StopShooting());
    }

    private void OnDisable()
    {
        shootingStateActive = false;
        StopAllCoroutines();
    }

    
    void Update()
    {
        if (shootingStateActive)
        {
            CallShooting();
        }
    }

    void CallShooting()
    {
        ShootAtPlayer();
        agentEnemy.SetDestination(transform.position);
        
    }

    IEnumerator StopShooting()
    {
        yield return new WaitForSeconds(3f);
        FPSGameEvents.OnSwitchState?.Invoke(goToCoverState, this.stateManager);
    }

    //shoot at player
    protected void ShootAtPlayer()
    {
        FireTimer();
        transform.LookAt(target);
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit))
        {
            if (hit.transform == target && canFire)
            {
                ShootBullet();
            }
        }
    }


    protected void FireTimer()
    {
        if (canFire == false)
        {
            shootingTimer += Time.deltaTime;
            if (shootingTimer > fireRate)
            {
                canFire = true;
                shootingTimer = 0f;
            }
        }
    }

    protected void ShootBullet()
    {
        if (canFire)
        {
            EnemyBulletRP bullet = GetPooledEnemyBullet();
            bullet.transform.position = gunTransform.position;
            bullet.transform.LookAt(target);
            canFire = false;
        }
    }

    protected EnemyBulletRP GetPooledEnemyBullet()
    {
        EnemyBulletRP enemyBullet = ObjectPool.instance.GetPooledEnemyBullet();
        if (enemyBullet != null)
        {
            enemyBullet.gameObject.SetActive(true);
            return enemyBullet;
        }
        else
        {
            return null;
        }
    }

}
