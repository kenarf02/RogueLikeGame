using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LoadingScreenBehavior : MonoBehaviour
{
    public Text LoadingText;
    public string[] textTexts;

    void OnEnable()
    {
        StartCoroutine(ChangeFunnyTexts());
    }
    IEnumerator ChangeFunnyTexts()
    {
        string currentText;
        
        if (GameObject.Find("PathfinderObject").GetComponent<WaitForLoad>().loaded == false)
        {
            for(int i =0; i<=10; i++)
            {
                currentText = textTexts[Random.Range(0, textTexts.Length)];
                LoadingText.text = currentText.ToUpper();
                yield return new WaitForSeconds(0.3f);
            }    
        }
        else
        {
       yield return null;
            StopAllCoroutines();
       
        }
    }
}
