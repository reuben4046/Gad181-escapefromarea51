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
        // Collition code for Documentation Pickup
        if (Col.gameObject.tag == "Document")
        {
            Debug.Log("Evidence collected!");
            docs = docs + 1;
            ScoreManager.docCount += 1;
            //   Col.gameObject.SetActive(false);
            Destroy(Col.gameObject, 1f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
