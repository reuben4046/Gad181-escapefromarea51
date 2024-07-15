using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEnderRP : MonoBehaviour
{
    public SpriteRenderer playerRenderer;

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
        StartCoroutine(WaitSeconds());
    }

    private IEnumerator WaitSeconds()
    {
        playerRenderer.enabled = false;
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("GameOver");
    }
}
