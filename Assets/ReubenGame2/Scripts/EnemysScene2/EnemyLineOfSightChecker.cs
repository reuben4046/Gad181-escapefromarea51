using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class EnemyLineOfSightChecker : MonoBehaviour
{
    public SphereCollider Collider;
    public float feildOfView = 90f;
    public LayerMask lineOfSightLayers;

    public delegate void GainSightEvent(Transform Target);
    public GainSightEvent OnGainSight;
    public delegate void LoseSightEvent(Transform Target);
    public LoseSightEvent OnLoseSight;

    private Coroutine CheckForLineOfSightCoroutine;

    void Awake()
    {
        Collider = GetComponent<SphereCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!CheckForLineOfSight(other.transform))
        {
            CheckForLineOfSightCoroutine = StartCoroutine(CheckForLineOfSightC(other.transform));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        OnLoseSight?.Invoke(other.transform);
        if (CheckForLineOfSightCoroutine != null)
        {
            StopCoroutine(CheckForLineOfSightCoroutine);
        }
    }

    private bool CheckForLineOfSight(Transform Target)
    {
        Vector3 direction = (Target.position - transform.position).normalized;
        float dotProduct = Vector3.Dot(transform.forward, direction);
        if (dotProduct >= Mathf.Cos(feildOfView))
        {
            if (Physics.Raycast(transform.position, direction, out RaycastHit hit, Collider.radius, lineOfSightLayers))
            {
                OnGainSight?.Invoke(Target);  
                return true;
            }
        }

        return false;
    }

    private IEnumerator CheckForLineOfSightC(Transform Target)
    {
        WaitForSeconds wait = new WaitForSeconds(0.5f);

        while (!CheckForLineOfSight(Target))
        {
            yield return wait;
        }

    }
}
