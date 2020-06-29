using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodStainRandomize : MonoBehaviour
{
    public Sprite[] sprite;
    void Start()
    {
        GetComponent<SpriteRenderer>().sprite = sprite[Random.Range(0, sprite.Length)];
        transform.Rotate(0,0,Random.Range(0,360));
        transform.SetParent( GameObject.FindGameObjectWithTag("Level").transform);
    }

    
}
