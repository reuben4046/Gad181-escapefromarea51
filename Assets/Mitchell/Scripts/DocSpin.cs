using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DocSpin : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Animates the Document Spin by rotation.
        transform.Rotate(0, 0, 1);
    }
}
