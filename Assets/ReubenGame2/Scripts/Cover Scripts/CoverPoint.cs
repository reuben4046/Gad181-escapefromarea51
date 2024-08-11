using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoverPoint : MonoBehaviour
{
    Transform target;

    [SerializeField] float checkTimeInterval = .1f;

    [SerializeField] float xPos = 1f;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindWithTag("Player")?.transform; // ? is a null check
        if (target == null) {Debug.Log($"target= {target}");} //null check
        StartCoroutine(ContinuousPlayerDirectionCheck());
    }

    Vector3 GetDirectionOfTarget()
    {
        Vector3 direction = target.position - transform.position;
        direction.Normalize();
        return direction;
    }

    //making sure the CoverPoint is on the opposite side to the Target. 
    //casts a ray at the target (player) and returns the hit
    RaycastHit PlayerDirectionRaycast()
    {
        Vector3 targetDirection = GetDirectionOfTarget();
        Debug.DrawRay(transform.position, targetDirection, Color.red, 10f);
        Physics.Raycast(transform.position, targetDirection, out RaycastHit hit);

        return hit;
    }

    IEnumerator ContinuousPlayerDirectionCheck()
    {
        while (true)
        {
            yield return new WaitForSeconds(checkTimeInterval);
            RaycastHit hit = PlayerDirectionRaycast();
            if (hit.transform == target)
            {
                float newX = SwapPosSwitch();
                transform.localPosition = new Vector3(newX, transform.localPosition.y, transform.localPosition.z);
            }
        }
    }

    //swaps the position between -1 and 1 every time the function is called
    bool swap = false;
    float SwapPosSwitch()
    {
        switch (swap)
        {
            case true:
            {
                swap = false;
                return xPos;                
            }
            case false:
            {
                swap = true;
                return -xPos;                
            }
        }
    }

    //gizmos used for figuring out and visualisng the spawn area coordinates and to see if they are actually swapping positions
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 0.5f);
    }

}
