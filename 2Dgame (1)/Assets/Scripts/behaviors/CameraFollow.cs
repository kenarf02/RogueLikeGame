using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    /*public float cameraDistance = 1.0f;

    void Awake()
    {
        GetComponent<UnityEngine.Camera>().orthographicSize = ((Screen.height / 2) / cameraDistance);
    }*/

    void Update()
    {
        transform.position = new Vector3(player.position.x, player.position.y, transform.position.z);
    }


}
