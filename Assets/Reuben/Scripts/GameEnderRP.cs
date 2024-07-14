using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEnderRP : MonoBehaviour
{

    private void OnEnable()
    {
        EventSystemRP.OnPlayerHealthZero += OnPlayerHealthZero;
    }

    private void OnDisable()
    {
        EventSystemRP.OnPlayerHealthZero -= OnPlayerHealthZero;
    }   

    private void OnPlayerHealthZero()
    {
        Destroy(gameObject);
    }
}
