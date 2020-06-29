using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    public GameObject[] zombie;

    void Start()
    {
        int n = Random.Range(0, 2);
        Instantiate(zombie[n], transform.position, Quaternion.identity);
    }

    void Update()
    {
        
    }
}
