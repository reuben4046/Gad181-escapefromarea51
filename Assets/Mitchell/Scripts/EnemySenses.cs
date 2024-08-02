using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemySenses : MonoBehaviour
{

    public float viewRadius;
    public float viewAngle;
    public bool canSeePlayer = false;

    public LayerMask targetPlayer;
    public LayerMask obstacleMask;

    public GameObject player;

    public bool startCapture = true;

    public float timeTillCapture;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 playerDirection = (player.transform.position - transform.position).normalized;

        if (Vector3.Angle(transform.forward, playerDirection) < viewAngle / 2)
        {
            Debug.Log("You're in angle!");
            float distanceToTarget = Vector3.Distance(transform.position, player.transform.position);
            if (distanceToTarget <= viewRadius)
            {
                Debug.Log("You're in distance!");
                if (Physics.Raycast(transform.position, playerDirection, distanceToTarget, obstacleMask) == false)
                {
                    Debug.Log("I can see you directly!!!");
                    canSeePlayer = true;
                    if (startCapture = true)
                    {
                        startCapture = false;
                        StartCoroutine(WaitTillCaught());
                    }
                }
                else
                {
                    canSeePlayer = false;
                }
            }
        }
    }

    public IEnumerator WaitTillCaught()
    {
        // suspend execution for 5 seconds
        yield return new WaitForSeconds(timeTillCapture);
        if (canSeePlayer)
        {
            Debug.Log("CAUGHT!!");
            SceneManager.LoadScene(5);
        }
        else
        {
            startCapture = true;
        }

    }
}