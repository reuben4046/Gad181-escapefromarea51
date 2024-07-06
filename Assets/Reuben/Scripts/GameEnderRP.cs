using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEnderRP : MonoBehaviour
{

    private void OnEnable()
    {
        EventSystemRP.OnPlayerHit += OnPlayerHit;
    }

    private void OnDisable()
    {
        EventSystemRP.OnPlayerHit -= OnPlayerHit;
    }   

    private void OnPlayerHit()
    {
        Destroy(gameObject);
    }
}
