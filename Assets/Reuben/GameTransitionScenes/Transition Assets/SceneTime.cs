using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTime : MonoBehaviour {

    //this script waits for 3 seconds and then loads the next scene
    private float waitTime = 3f;

    // Start is called before the first frame update
    void Start() {
        StartCoroutine(Wait());
    }

    IEnumerator Wait() {
        yield return new WaitForSeconds(waitTime);
        LoadNextScene();
    }

    private void LoadNextScene() {
        SceneManager.LoadScene("GameStartRP");
    }
}
