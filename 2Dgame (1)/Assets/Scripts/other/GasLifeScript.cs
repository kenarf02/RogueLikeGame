using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GasLifeScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(waitForDie());

    }

 private IEnumerator waitForDie()
    {
        yield return new WaitForSeconds(4f);
        Destroy(this.gameObject);
    }
    
}
