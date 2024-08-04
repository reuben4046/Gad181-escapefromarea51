using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateManager : MonoBehaviour
{
    [SerializeField] List<BaseEnemyState> states = new List<BaseEnemyState>();
    [SerializeField] BaseEnemyState startState;

    [SerializeField] List<CoverRP> covers = new List<CoverRP>();

    private void Awake()
    {
        FPSGameEvents.OnCoverStart += OnCoverStart;
        FPSGameEvents.OnSwitchState += OnSwitchState;
    }

    private void OnDisable()
    {
        FPSGameEvents.OnSwitchState -= OnSwitchState;
    }

    private void OnCoverStart(CoverRP cover)
    {
        covers.Add(cover);
    }

    private void Start()
    {
        foreach (BaseEnemyState state in states)
        {
            state.gameObject.SetActive(false);
        }
        startState.gameObject.SetActive(true);

    }

    private void OnSwitchState(BaseEnemyState State, EnemyStateManager enemy)
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
