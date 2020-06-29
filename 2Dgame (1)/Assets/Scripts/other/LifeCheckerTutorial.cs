using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeCheckerTutorial : MonoBehaviour
{
    TutorialBehavior tb;
    private void Start()
    {
        tb = GameObject.Find("tutorial room").GetComponent<TutorialBehavior>();
    }
    void Update()
    {
        if(transform.childCount == 0)
        {
            tb.DestroyBarricade();
            Destroy(gameObject);
        }
    }
}
