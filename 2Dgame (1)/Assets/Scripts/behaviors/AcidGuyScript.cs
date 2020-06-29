using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcidGuyScript : MonoBehaviour
{
    public GameObject acidpuddle;
    public GameObject acidbullet;
    public Transform shootpoint;
    public AudioClip splat;
    void Start()
    {
        InvokeRepeating("Attack", 1f, 2f);
    }
    void Attack()
    {
        int randomnumber;
        randomnumber = Random.Range(0, 5);
        
        if(randomnumber == 0)
        {
            AudioSource.PlayClipAtPoint(splat, this.transform.position);
            Instantiate(acidpuddle, transform.position, Quaternion.identity);
            Debug.Log("puddle");          //puddle
        }
       else
        {
            Instantiate(acidbullet, shootpoint.transform.position,transform.rotation);
            Debug.Log("puddle");
        }
    }
}
