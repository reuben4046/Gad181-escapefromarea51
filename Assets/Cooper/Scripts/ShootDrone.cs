using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ShootDrone : MonoBehaviour
{
    void OnMouseDown()
    {
        Destroy(gameObject);
    }

}
