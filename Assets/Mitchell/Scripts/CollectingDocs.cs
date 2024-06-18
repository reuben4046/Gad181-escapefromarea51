using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CollectingDocs : MonoBehaviour
{
    public int docs;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void OnTriggerEnter(Collider Col)
    {
        // Collition code for Coin Pickup
        if (Col.gameObject.tag == "Docs")
        {
            Debug.Log("Evidence collected!");
            docs = docs + 1;

            //   Col.gameObject.SetActive(false);
            Destroy(Col.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
