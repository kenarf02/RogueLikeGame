using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirstrikeScript : MonoBehaviour
{
    public GameObject rocket;
    public AudioClip bang;
    void Start()
    {
        Invoke("Die", 0.22f);
        
            }
    void Die()
    {
        Instantiate(rocket,this.gameObject.transform.position,Quaternion.identity);
       GetComponent<AudioSource>().PlayOneShot (bang);

        if (GetComponent<AudioSource>().isPlaying == false)
        {
            Destroy(this.gameObject);
        }
       
    }
}
