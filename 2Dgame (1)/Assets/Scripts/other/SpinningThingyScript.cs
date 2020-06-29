using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinningThingyScript : MonoBehaviour
{
    //            UnityEngine.Experimental.Rendering.LWRP.Light2D The2DLights = transform.parent.gameObject.GetComponent<UnityEngine.Experimental.Rendering.LWRP.Light2D>();

    void OnBecameVisible()
    {
        foreach(Transform child in transform)
        {
            if (child.gameObject.GetComponent<UnityEngine.Experimental.Rendering.Universal.Light2D>() != null)
                child.gameObject.GetComponent<UnityEngine.Experimental.Rendering.Universal.Light2D>().enabled = true;
                    gameObject.GetComponent<Animator>().SetTrigger("On");
        }
    }
    void OnBecameInvisible()
    {
        foreach (Transform child in transform)
        {
            child.gameObject.GetComponent<UnityEngine.Experimental.Rendering.Universal.Light2D>().enabled = false;
            gameObject.GetComponent<Animator>().Play("NewState");
        }
    }
}
