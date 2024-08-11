using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseEnemyState : MonoBehaviour
{
    [SerializeField] protected EnemyStateManager stateManager;
  
    protected virtual void StartState()
    {

    }
   //this is the base state that all states derive from. it is usefull because it means that all states will be able to be looped through as states
}
