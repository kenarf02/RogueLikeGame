using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCanvasScript : MonoBehaviour
{
    GameObject player;
    private void OnEnable()
    {
        player = GameObject.Find("Player");
    }
    void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, player.transform.position, 10f * Time.deltaTime);
    }
}
