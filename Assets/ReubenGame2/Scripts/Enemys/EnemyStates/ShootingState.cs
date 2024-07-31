using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ShootingState : BaseEnemyState
{
    public GoToCoverState goToCoverState;

    //NavMeshAgent
    [SerializeField] NavMeshAgent agentEnemy;
    //PlayerReference
    [SerializeField] Transform target;

    //Gun
    [SerializeField] Transform gunTransform;
    bool canFire = true;
    float fireRate = 0.5f;
    float shootingTimer = 0f;

    private void OnEnable()
    {
        CallShooting();
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    void CallShooting()
    {
        ShootAtPlayer();
        agentEnemy.SetDestination(transform.position);
        StartCoroutine(StopShooting());
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
