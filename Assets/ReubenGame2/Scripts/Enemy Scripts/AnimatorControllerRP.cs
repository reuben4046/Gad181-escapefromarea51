using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorControllerRP : MonoBehaviour
{

    public Animator animator;

    void OnEnable()
    {
        FPSGameEvents.OnSwitchState += OnSwitchState;
    }

    void OnDisable()
    {
        FPSGameEvents.OnSwitchState -= OnSwitchState;
    }

    void OnSwitchState(BaseEnemyState state, EnemyStateManager enemy)
    {
        switch (state)
        {
            case GoToCoverState:
                animator.SetTrigger("Running");
                break;
            case ShootingState:
                animator.SetTrigger("Shooting");
                break;
            case MoveTowardsPlayerState:
                animator.SetTrigger("Running");
                break;
        }
    }
}
