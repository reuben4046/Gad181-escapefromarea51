using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RulesText : MonoBehaviour
{   
    [SerializeField] TextMeshProUGUI rulesText;

    void Start()
    {
        rulesText.enabled = true;
        StartCoroutine(DisableAfter(3f));
    }

    IEnumerator DisableAfter(float time)
    {
        yield return new WaitForSeconds(time);
        rulesText.enabled = false;
    }
}
