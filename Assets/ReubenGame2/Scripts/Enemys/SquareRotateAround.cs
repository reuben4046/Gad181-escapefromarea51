using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquareRotateAround : MonoBehaviour
{
    Vector3 coverPointSize = new Vector3(1, 1, 1);
    float coverSizeX = 2f;
    float coverSizeY = 2f;
    Vector3 coverSize;

    public Transform target;
    
    void Start()
    {
        coverSize = new Vector3(coverSizeX, coverSizeY, 1);
    }

    void Update()
    {
        UpdateCoverPointPosition();
    }
    
    private void UpdateCoverPointPosition() 
    {
        coverSizeX = Mathf.Clamp(coverSize.x, coverPointSize.x, 1 - coverPointSize.x);
        coverSizeY = Mathf.Clamp(coverSize.y, coverPointSize.y, 1 - coverPointSize.y);

        var worldPosition = coverSize;
        worldPosition.z = 0;
        transform.position = worldPosition;

        Vector3 direction = target.transform.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 90f;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }
}
