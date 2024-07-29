using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemyState : State
{
    public override State RunCurrentState()
    {
        return this;
    }
}
