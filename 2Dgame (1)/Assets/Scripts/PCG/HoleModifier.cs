using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoleModifier : MonoBehaviour
{
    public Sprite[] holeTexture;

    SpawnObject spawnObject;

    void Start()
    {
        InvokeRepeating("changeHoleTexture", 5f, 1f);
        spawnObject = gameObject.transform.GetComponentInParent<SpawnObject>();
        spawnObject.isTile = true;
    }


    void changeHoleTexture()
    {
        // 0
        if (spawnObject.upB == true)
        {
            if (spawnObject.downB == true)
            {
                if (spawnObject.leftB == true)
                {
                    if (spawnObject.rightB == true)
                    {
                        gameObject.GetComponent<SpriteRenderer>().sprite = holeTexture[0];
                    }
                }
            }
        }
        // 1
        if (spawnObject.upB == false)
        {
            if (spawnObject.downB == true)
            {
                if (spawnObject.leftB == false)
                {
                    if (spawnObject.rightB == false)
                    {
                        gameObject.GetComponent<SpriteRenderer>().sprite = holeTexture[1];
                    }
                }
            }
        }
        // 2
        if (spawnObject.upB == false)
        {
            if (spawnObject.downB == false)
            {
                if (spawnObject.leftB == true)
                {
                    if (spawnObject.rightB == false)
                    {
                        gameObject.GetComponent<SpriteRenderer>().sprite = holeTexture[2];
                    }
                }
            }
        }
        // 3
        if (spawnObject.upB == false)
        {
            if (spawnObject.downB == false)
            {
                if (spawnObject.leftB == false)
                {
                    if (spawnObject.rightB == true)
                    {
                        gameObject.GetComponent<SpriteRenderer>().sprite = holeTexture[3];
                    }
                }
            }
        }
        // 4
        if (spawnObject.upB == true)
        {
            if (spawnObject.downB == false)
            {
                if (spawnObject.leftB == false)
                {
                    if (spawnObject.rightB == false)
                    {
                        gameObject.GetComponent<SpriteRenderer>().sprite = holeTexture[4];
                    }
                }
            }
        }
        // 5
        if (spawnObject.upB == false)
        {
            if (spawnObject.downB == true)
            {
                if (spawnObject.leftB == true)
                {
                    if (spawnObject.rightB == false)
                    {
                        gameObject.GetComponent<SpriteRenderer>().sprite = holeTexture[5];
                    }
                }
            }
        }
        // 6
        if (spawnObject.upB == false)
        {
            if (spawnObject.downB == true)
            {
                if (spawnObject.leftB == true)
                {
                    if (spawnObject.rightB == true)
                    {
                        gameObject.GetComponent<SpriteRenderer>().sprite = holeTexture[6];
                    }
                }
            }
        }
        // 7
        if (spawnObject.upB == false)
        {
            if (spawnObject.downB == false)
            {
                if (spawnObject.leftB == true)
                {
                    if (spawnObject.rightB == true)
                    {
                        gameObject.GetComponent<SpriteRenderer>().sprite = holeTexture[7];
                    }
                }
            }
        }
        // 8
        if (spawnObject.upB == true)
        {
            if (spawnObject.downB == false)
            {
                if (spawnObject.leftB == true)
                {
                    if (spawnObject.rightB == true)
                    {
                        gameObject.GetComponent<SpriteRenderer>().sprite = holeTexture[8];
                    }
                }
            }
        }
        // 9
        if (spawnObject.upB == true)
        {
            if (spawnObject.downB == false)
            {
                if (spawnObject.leftB == true)
                {
                    if (spawnObject.rightB == false)
                    {
                        gameObject.GetComponent<SpriteRenderer>().sprite = holeTexture[9];
                    }
                }
            }
        }
        // 10
        if (spawnObject.upB == true)
        {
            if (spawnObject.downB == true)
            {
                if (spawnObject.leftB == true)
                {
                    if (spawnObject.rightB == false)
                    {
                        gameObject.GetComponent<SpriteRenderer>().sprite = holeTexture[10];
                    }
                }
            }
        }
        // 11
        if (spawnObject.upB == false)
        {
            if (spawnObject.downB == false)
            {
                if (spawnObject.leftB == false)
                {
                    if (spawnObject.rightB == false)
                    {
                        gameObject.GetComponent<SpriteRenderer>().sprite = holeTexture[11];
                    }
                }
            }
        }
        // 12
        if (spawnObject.upB == false)
        {
            if (spawnObject.downB == true)
            {
                if (spawnObject.leftB == false)
                {
                    if (spawnObject.rightB == true)
                    {
                        gameObject.GetComponent<SpriteRenderer>().sprite = holeTexture[12];
                    }
                }
            }
        }
        // 13
        if (spawnObject.upB == true)
        {
            if (spawnObject.downB == false)
            {
                if (spawnObject.leftB == false)
                {
                    if (spawnObject.rightB == true)
                    {
                        gameObject.GetComponent<SpriteRenderer>().sprite = holeTexture[13];
                    }
                }
            }
        }
        // 14
        if (spawnObject.upB == true)
        {
            if (spawnObject.downB == true)
            {
                if (spawnObject.leftB == false)
                {
                    if (spawnObject.rightB == false)
                    {
                        gameObject.GetComponent<SpriteRenderer>().sprite = holeTexture[14];
                    }
                }
            }
        }
        // 15
        if (spawnObject.upB == true)
        {
            if (spawnObject.downB == true)
            {
                if (spawnObject.leftB == false)
                {
                    if (spawnObject.rightB == true)
                    {
                        gameObject.GetComponent<SpriteRenderer>().sprite = holeTexture[15];
                    }
                }
            }
        }
    }
}
