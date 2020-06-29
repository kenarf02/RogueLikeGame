using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeManager : MonoBehaviour
{
    float timer;
    public float Rate;
    public GameObject[] spikes;
    private void OnEnable()
    {
        timer = Rate;
    }
    void Update()
    {
        if(timer <= 0)
        {
            foreach(GameObject spike in spikes)
            {
                spike.GetComponent<SpikesBehavior>().ChangeState();
                Debug.Log("Essa");
                timer = Rate;
            }
        }
        else
        {
            timer -= Time.deltaTime;
        }
    }
}
