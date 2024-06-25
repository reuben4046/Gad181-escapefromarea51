using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissleSpawnerRP : MonoBehaviour
{
    [Header("Gizmos")]
    [SerializeField] private float gizmoMaxXRange = 50;
    [SerializeField] private float gizmoMaxYRange = 40;

    [SerializeField] private float gizmoMinXRange = 35;
    [SerializeField] private float gizmoMinYRange = 25;

    [SerializeField] private float gizPositionX = 0f;
    [SerializeField] private float gizPositionY = 0f;


    [Header("Spawn")]
    [SerializeField] private float xRangeMax = 25f;
    [SerializeField] private float xRangeMin = -25f;

    [SerializeField] private float yRangeMax = 20f;
    [SerializeField] private float yRangeMin = -20f;

    [SerializeField] private float smallYRangeMax = 20f;
    [SerializeField] private float smallYRangeMin = 12f;
    
    private Vector2 spawnPosition;

    public GameObject misslePrefab;

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

            Instantiate(misslePrefab, spawnPosition, Quaternion.identity);
        }
    }

    private Vector2 PickRandomSpawn()
    {
        float randX = Random.Range(xRangeMin, xRangeMax);
        float randY = Random.Range(yRangeMin, yRangeMax);
        if (randX < 17.5f && randX > -17.5f)
        {
            bool positiveY = Random.Range(0, 2) == 1;
            if (positiveY)
            {
                randY = Random.Range(smallYRangeMin, smallYRangeMax);
            } 
            else
            {
                randY = Random.Range(-smallYRangeMin, -smallYRangeMax);
            }
        }
        else
        {
            randY = Random.Range(yRangeMin, yRangeMax);
        }

        return new Vector2(randX, randY);
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, new Vector3(gizmoMaxXRange, gizmoMaxYRange, 0));

        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(transform.position, new Vector3(gizmoMinXRange, gizmoMinYRange, 0));

        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(spawnPosition, 0.5f);

        Gizmos.color = Color.green;
        Gizmos.DrawSphere(new Vector2(gizPositionX, gizPositionY), 0.2f);
    }

}
