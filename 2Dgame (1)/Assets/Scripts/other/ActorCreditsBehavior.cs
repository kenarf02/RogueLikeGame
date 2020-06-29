using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ActorCreditsBehavior : MonoBehaviour
{
    private void OnEnable()
    {
        if (GameObject.Find("CreditsObj"))
        {
            gameObject.GetComponent<Animator>().Play(GameObject.Find("CreditsObj").GetComponent<Credits>().currentanimationName);
            if (GameObject.Find("CreditsObj").GetComponent<Credits>().currentanimationName == "SoldierAnim")
            {
                transform.localScale = new Vector3(1.1f, 1.1f, .8f);
                GetComponent<RectTransform>().position += new Vector3(0, -20, 0);
            }
            else
            {
                transform.localScale = new Vector3(0.9f, 0.9f, .8f);
                GetComponent<RectTransform>().position += new Vector3(0, -28, 0);
            }
        }
        else
        {
            return;
        }
    }
}
