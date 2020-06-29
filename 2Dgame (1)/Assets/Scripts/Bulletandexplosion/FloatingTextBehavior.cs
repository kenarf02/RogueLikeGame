using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingTextBehavior : MonoBehaviour
{
    public float destroytime = 0.35f;
    public Vector3 offset = new Vector3(0, 0.7f,0);
    void Start()
    {
        Destroy(gameObject, destroytime);
    }

}
