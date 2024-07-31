using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(fileName = "StateVariables", menuName = "StateVariables")]
public class StateVariables : ScriptableObject
{
    //PlayerReference
    public Transform target;

    public Camera cam;

    //NavMeshAgent
    public NavMeshAgent agentEnemy;

    //Gun
    public Transform gunTransform;
    public bool canFire = true;
    public float fireRate = 0.5f;
    public float shootingTimer = 0f;

    //Covers
    public List<CoverToList> covers = new List<CoverToList>();
    public List<Transform> coverPoints = new List<Transform>();

    public CoverToList currentCover = null;
    public Transform currentCoverPoint = null;
}
