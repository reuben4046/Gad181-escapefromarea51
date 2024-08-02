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
                // float newX = SwapPosition();
                float newX = SwapPosSwitch();
                transform.localPosition = new Vector3(newX, transform.localPosition.y, transform.localPosition.z);
            }
        }
    }

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

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 0.5f);
    }

}
