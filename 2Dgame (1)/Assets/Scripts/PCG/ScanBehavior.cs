using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScanBehavior : MonoBehaviour
{
  
    void Update()
    {
        if(transform.childCount > 0)
        {

        }
        else
        {
            GameObject.Find("PathfinderObject").GetComponent<AstarPath>().Scan();
            Destroy(this.gameObject);
        }
    }
}
