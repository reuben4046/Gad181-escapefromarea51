using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoverToList : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        FPSGameEvents.OnCoverStart?.Invoke(this);
    }

}
