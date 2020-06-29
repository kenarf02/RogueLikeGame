using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlayer : MonoBehaviour
{
    public GameObject player;
    void Start()
    {
        player = GameObject.Find("Player");
        if (player != null)
        {
            player.transform.position = gameObject.transform.position;
        }
    }
}
