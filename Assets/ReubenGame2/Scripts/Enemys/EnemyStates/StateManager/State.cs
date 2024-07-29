using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public abstract class State : MonoBehaviour
{
    public abstract State RunCurrentState();
    
}
