using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissleSpawnerRP : MonoBehaviour
{

    [SerializeField] private float maxXRange = 50;
    [SerializeField] private float maxYRange = 40;

    [SerializeField] private float minXRange = 35;
    [SerializeField] private float minYRange = 25;

    private Vector2 spawnPosition;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnMissles());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private bool corotineRunning = true;

    private IEnumerator SpawnMissles()
    {
        while (corotineRunning)
        { 
            yield return new WaitForSeconds(3f);
            spawnPosition = PickRandomSpawn();
            Debug.Log("Missle Spawned at " + spawnPosition);
           
        }
    }

    private Vector2 PickRandomSpawn()
    {
        bool addX = Random.Range(0, 2) == 1;
        bool addY = Random.Range(0, 2) == 1;

        float randX = Random.Range(minXRange, maxXRange);
        float randY = Random.Range(minYRange, maxYRange);

        float newXPos = transform.position.x + (addX ? randX : -randX);
        float newYPos = transform.position.y + (addY ? randY : -randY);

        return new Vector2(newXPos, newYPos);
    }


    [SerializeField] private float gizPositionX = 0f;
    [SerializeField] private float gizPositionY = 0f;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, new Vector3(maxXRange, maxYRange, 0));

        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(transform.position, new Vector3(minXRange, minYRange, 0));

        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(spawnPosition, 3f);

        Gizmos.color = Color.green;
        Gizmos.DrawSphere(new Vector2(gizPositionX, gizPositionY), 3f);
    }

}
