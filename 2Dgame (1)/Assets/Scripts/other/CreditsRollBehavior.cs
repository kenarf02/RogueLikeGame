using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class CreditsRollBehavior : MonoBehaviour
{
    void OnEnable()
    {
        Time.timeScale = 1.0f;
        StartCoroutine(start());
    }
   IEnumerator start()
    {
        yield return new WaitForSecondsRealtime(26f);
        SceneManager.LoadScene(0);
    }

}
