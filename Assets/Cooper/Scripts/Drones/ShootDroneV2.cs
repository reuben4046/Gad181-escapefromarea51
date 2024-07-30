using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootDroneV2 : MonoBehaviour
{

    [SerializeField] Camera cam;
    [SerializeField] private Gamemanager droneTracker;
    // Start is called before the first frame update
    void Start()
    {
        droneTracker = this.GetComponent<Gamemanager>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartCoroutine(Shooting());
        }
            
    }

    IEnumerator Shooting()
    {
        yield return new WaitForSeconds(1.5f);
        if (Physics.Raycast(cam.ScreenPointToRay(Input.mousePosition), out RaycastHit hit))
        {
            Collider bc = hit.collider;
            if (bc != null)
            {
                droneTracker.numberOfdrones -= 1;
                Destroy(bc.gameObject);
            }
        }


    }

}
