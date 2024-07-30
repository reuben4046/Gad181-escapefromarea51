using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySenses : MonoBehaviour
{

    public float viewRadius;
    public float viewAngle;
    public bool canSeePlayer = false;

    public LayerMask targetPlayer;
    public LayerMask obstacleMask;

    public GameObject player;
    public Fatster enemy;

    // Start is called before the first frame update
    void Start()
    {
        enemy = GetComponentInChildren<Fatster>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 playerDirection = (player.transform.position - transform.position).normalized;

        if (Vector3.Angle(transform.forward, playerDirection) < viewAngle / 2)
        {
            float distanceToTarget = Vector3.Distance(transform.position, player.transform.position);
            if (distanceToTarget <= viewRadius)
            {
                if (Physics.Raycast(transform.position, playerDirection, distanceToTarget, obstacleMask) == false)
                {
                    Debug.Log("I can see you!");
                    canSeePlayer = true;
                    if (enemy != null)
                    {
                        enemy.RotateTowardsPlayer();
                        enemy.MoveTowardsPlayer();
                    }

                }
                if (Physics.Raycast(transform.position, playerDirection, out RaycastHit hit))
                {
                    if (hit.point == player.transform.position)
                    {
                        canSeePlayer= true;
                    } else { canSeePlayer = false; }
                } 
                
            }
        }
    }
}
