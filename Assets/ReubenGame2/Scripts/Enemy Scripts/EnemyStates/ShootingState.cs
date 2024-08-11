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


    //used like a start function, so it gets called when the state is entered
    private void OnEnable()
    {
        shootingStateActive = true;
        agentEnemy.SetDestination(transform.position);
        StartCoroutine(StopShooting());
        StartCoroutine(ContinuousPlayerDirectionCheck());
    }

    //makes sure all coroutines are not running
    private void OnDisable()
    {
        shootingStateActive = false;
        StopAllCoroutines();
    }

    // Update is called once per frame. this update is only called while the shooting state is active. 
    void Update()
    {
        if (shootingStateActive)
        {
            ShootAtPlayer();
            LookAtPlayer();
        }
    }

    void LookAtPlayer()
    {
        agentEnemy.transform.LookAt(target);
    }

    //waits a certain amount of time then switches to the next state
    IEnumerator StopShooting()
    {
        yield return new WaitForSeconds(3f);
        FPSGameEvents.OnSwitchState?.Invoke(goToCoverState, this.stateManager);
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

    //shoot timer
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

    //gets pooled bullet and puts it in the position of the gun and makes it face the player 
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

    //gets pooled bullet
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

    //get the direction of the target and returns that direction
    Vector3 GetDirectionOfTarget()
    {
        Vector3 direction = target.position - transform.position;
        direction.Normalize();
        return direction;
    }

    //returns a raycast hit. used to check if the player is in the shooting range 
    RaycastHit PlayerDirectionRaycast()
    {
        Vector3 targetDirection = GetDirectionOfTarget();
        Debug.DrawRay(transform.position, targetDirection, Color.red, 10f);
        Physics.Raycast(transform.position, targetDirection, out RaycastHit hit);

        return hit;
    }

}
