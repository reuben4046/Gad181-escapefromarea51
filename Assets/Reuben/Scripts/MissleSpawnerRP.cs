using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissleSpawnerRP : MonoBehaviour
{
    //spawn position that the prefab is spawned at
    private Vector2 spawnPosition;

    //prefab reference
    public GameObject misslePrefab;


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
    
    
    //keeps coroutine running in a loop
    private bool corotineRunning = true;

    private float spawnInterval = 3f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnMissles());
    }

    
    //coroutine that spawns the missles every spawnInterval seconds
    private IEnumerator SpawnMissles()
    {
        while (corotineRunning)
        { 
            yield return new WaitForSeconds(spawnInterval);

            spawnPosition = PickRandomSpawn();
            Instantiate(misslePrefab, spawnPosition, Quaternion.identity);
        }
    }

    //Picks a random spawn position picking x first and if the x is too close to 0, then a y is picked that will make it so that the spawn 
    //position is still out of the veiw of the camera. 
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

        return new Vector2(transform.position.x + randX, transform.position.y + randY);
    }

    //gizmos used for figuring out and visualisng the spawn area coordinates
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
