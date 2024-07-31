using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour
{
    [SerializeField] List<BaseEnemyState> states = new List<BaseEnemyState>();
    [SerializeField] BaseEnemyState startState;

    private void OnEnable()
    {
        FPSGameEvents.OnSwitchState += OnSwitchState;
    }

    private void OnDisable()
    {
        FPSGameEvents.OnSwitchState -= OnSwitchState;
    }

    private void Awake()
    {
        foreach (BaseEnemyState state in states)
        {
            state.gameObject.SetActive(false);
        }
        startState.gameObject.SetActive(true);

    }

    private void OnSwitchState(BaseEnemyState State, StateManager enemy)
    {
        if (enemy != this)
        {
            return;
        }
        foreach (BaseEnemyState state in states)
        {
            if (state != State)
            {
                state.gameObject.SetActive(false);
            }
            else
            {
                state.gameObject.SetActive(true);
            }
        }
    }
}
