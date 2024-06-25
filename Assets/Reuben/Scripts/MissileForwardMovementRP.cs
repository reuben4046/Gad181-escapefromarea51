using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileForwardMovementRP : MonoBehaviour
{

   [SerializeField] private float speed = 10f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ForwardForce();
    }

    private void ForwardForce()
    {
        transform.Translate(new Vector2(0, 1) * speed * Time.deltaTime);
    }

}
