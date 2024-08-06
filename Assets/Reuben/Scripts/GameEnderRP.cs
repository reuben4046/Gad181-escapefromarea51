using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEnderRP : MonoBehaviour {

    // this script ends the game when the OnPlayerHealthZero event is triggered
    public SpriteRenderer playerRenderer;

    private float timeTillEndGame = 0.5f;

    //subscribing to the event and unsubscribing from the event
    private void OnEnable() {
        EventSystemRP.OnPlayerHealthZero += OnPlayerHealthZero;
    }
    private void OnDisable() {
        EventSystemRP.OnPlayerHealthZero -= OnPlayerHealthZero;
    }   

    //starts coroutine that ends the game after timeTillEndGame seconds
    private void OnPlayerHealthZero() 
    {
        StartCoroutine(WaitSeconds());
    }

    private IEnumerator WaitSeconds() {
        playerRenderer.enabled = false;
        yield return new WaitForSeconds(timeTillEndGame);
        SceneManager.LoadScene("GameOver");
    }
}
