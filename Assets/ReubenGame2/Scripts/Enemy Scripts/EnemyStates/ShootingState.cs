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
    Transform target;

    //shooting
    [SerializeField] Transform gunTransform;
    bool canFire = true;
    float fireRate = 0.5f;
    float shootingTimer = 0f;
    bool shootingStateActive;

    float checkTimeInterval = 0.1f;


    void Awake()
    {
        target = GameObject.FindWithTag("Player")?.transform;
    }

    private void OnEnable()
    {
        shootingStateActive = true;
        agentEnemy.SetDestination(transform.position);
        StartCoroutine(StopShooting());
        StartCoroutine(ContinuousPlayerDirectionCheck());
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
            ShootAtPlayer();
            LookAtPlayer();
        }
    }

    IEnumerator StopShooting()
    {
        yield return new WaitForSeconds(3f);
        FPSGameEvents.OnSwitchState?.Invoke(goToCoverState, this.stateManager);
    }

    void LookAtPlayer()
    {
        transform.LookAt(target);
    }

    //shoot at player
    protected void ShootAtPlayer()
    {
        FireTimer();
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

    //only shoots while can still see the player
    IEnumerator ContinuousPlayerDirectionCheck()
    {
        while (true)
        {
            yield return new WaitForSeconds(checkTimeInterval);
            RaycastHit hit = PlayerDirectionRaycast();
            if (hit.transform != target)
            {
                FPSGameEvents.OnSwitchState?.Invoke(goToCoverState, this.stateManager);
            }
        }
    }

    Vector3 GetDirectionOfTarget()
    {
        Vector3 direction = target.position - transform.position;
        direction.Normalize();
        return direction;
    }

    //making sure the CoverPoint is on the opposite side to the Target. 
    RaycastHit PlayerDirectionRaycast()
    {
        Vector3 targetDirection = GetDirectionOfTarget();
        Debug.DrawRay(transform.position, targetDirection, Color.red, 10f);
        Physics.Raycast(transform.position, targetDirection, out RaycastHit hit);

        return hit;
    }

}
