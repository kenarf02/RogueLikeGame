using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileModifier : MonoBehaviour
{
    public Sprite[] tileTexture;

    SpawnObject spawnObject;

    void Start()
    {
        InvokeRepeating("changeTileTexture", 5f, 1f);
        spawnObject = gameObject.GetComponentInParent<SpawnObject>();
        spawnObject.isTile = true;
    }

    
    void changeTileTexture()
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
                        gameObject.GetComponent<SpriteRenderer>().sprite = tileTexture[0];
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
                        gameObject.GetComponent<SpriteRenderer>().sprite = tileTexture[1];
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
                        gameObject.GetComponent<SpriteRenderer>().sprite = tileTexture[2];
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
                        gameObject.GetComponent<SpriteRenderer>().sprite = tileTexture[3];
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
                        gameObject.GetComponent<SpriteRenderer>().sprite = tileTexture[4];
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
                        gameObject.GetComponent<SpriteRenderer>().sprite = tileTexture[5];
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
                        gameObject.GetComponent<SpriteRenderer>().sprite = tileTexture[6];
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
                        gameObject.GetComponent<SpriteRenderer>().sprite = tileTexture[7];
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
                        gameObject.GetComponent<SpriteRenderer>().sprite = tileTexture[8];
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
                        gameObject.GetComponent<SpriteRenderer>().sprite = tileTexture[9];
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
                        gameObject.GetComponent<SpriteRenderer>().sprite = tileTexture[10];
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
                        gameObject.GetComponent<SpriteRenderer>().sprite = tileTexture[11];
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
                        gameObject.GetComponent<SpriteRenderer>().sprite = tileTexture[12];
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
                        gameObject.GetComponent<SpriteRenderer>().sprite = tileTexture[13];
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
                        gameObject.GetComponent<SpriteRenderer>().sprite = tileTexture[14];
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
                        gameObject.GetComponent<SpriteRenderer>().sprite = tileTexture[15];
                    }
                }
            }
        }
        CancelInvoke("changeTileTexture");
    }
  
}
