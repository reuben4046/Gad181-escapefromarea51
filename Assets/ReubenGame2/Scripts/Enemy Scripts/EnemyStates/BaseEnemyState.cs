using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseEnemyState : MonoBehaviour
{
    [SerializeField] protected EnemyStateManager stateManager;
  
    protected virtual void StartState()
    {

    }
   
}
