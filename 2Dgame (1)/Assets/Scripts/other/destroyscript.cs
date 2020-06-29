using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroyscript : MonoBehaviour
{
    public float time;
    void Start()
    {
        Destroy(this.gameObject,time);
    }

    void OnCollisionStay2D(Collision2D col)
    {
        if(col.gameObject.tag == "Acid" && col.gameObject.name != "Acid ball")
        {
            Destroy(col.gameObject);
            Debug.Log("Overlapping!");
        }
    }
}
