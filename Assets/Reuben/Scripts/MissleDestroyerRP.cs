using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissleDestroyerRP : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        Destroy(gameObject); 
    }

    

}
