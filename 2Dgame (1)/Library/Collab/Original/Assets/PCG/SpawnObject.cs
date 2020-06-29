using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObject : MonoBehaviour
{
    public GameObject[] objects;
    int rand;

    public bool isTile;

    public bool upB;
    public bool downB;
    public bool leftB;
    public bool rightB;

    void Start()
    {
        int rand = Random.Range(0, objects.Length);
        GameObject instance = (GameObject)Instantiate(objects[rand], transform.position, Quaternion.identity);
        instance.transform.parent = transform;
        StartCoroutine("Raycast");
    }

    void checkForNeighbours()
    {

    }
    public IEnumerator Raycast()
    {
        int i = 0;
        while (i <= 5)
        {
            shootRaycasts();
            yield return new WaitForSeconds(2f);
            i++;
        }
        if(i ==5)
        {
            yield return null;
        }
    }
        void shootRaycasts()
    {/*
        if (Physics2D.Raycast(transform.position + transform.up * 0.55f, Vector2.up, 0.25f))
        {
            // DRAW RAY
            RaycastHit2D[] up = Physics2D.RaycastAll(transform.position + transform.up * 0.55f, Vector2.up, 0.25f);
            foreach (RaycastHit2D upcol in up)
            {
                if (upcol.collider.tag == "Wall")
                {
                    upB = true;
                }
            }
        }
        
        if (Physics2D.Raycast(transform.position - transform.up * 0.55f, Vector2.down, 0.25f))
        {
            RaycastHit2D[] down = Physics2D.RaycastAll (transform.position - transform.up * 0.55f, Vector2.down, 0.25f);
            foreach (RaycastHit2D downcol in down)
            {
                if (downcol.collider.tag == "Wall")
                {
                    downB = true;
                }
            }
        }

        if (Physics2D.Raycast(transform.position - transform.right * 0.55f, Vector2.left, 0.25f))
        {
            RaycastHit2D[] left = Physics2D.RaycastAll(transform.position - transform.right * 0.55f, Vector2.left, 0.25f);
            foreach (RaycastHit2D leftcol in left)
            {
                if (leftcol.collider.tag == "Wall")
                {
                    leftB = true;
                }
            }
        }
        
        if (Physics2D.Raycast(transform.position + transform.right * 0.55f, Vector2.right, 0.25f))
        {
            RaycastHit2D[] right = Physics2D.RaycastAll(transform.position + transform.right * 0.55f, Vector2.right, 0.25f);
            foreach (RaycastHit2D rightcol in right)
            {
                if (rightcol.collider.tag == "Wall")
                {
                    rightB = true;
                }
            }
        }
        */
     //op tostuje złoto
    
        
        int layerMask = 1 << 11;
        layerMask = ~layerMask;
        if (Physics2D.Raycast(transform.position + transform.up * 0.55f, Vector2.up, 0.5f,layerMask))
        {
            // DRAW RAY
            //RaycastHit2D up = Physics2D.Raycast(transform.position + transform.up * 0.55f, Vector2.up, 0.25f);

            upB = true;
            
            /*if (up.collider.tag == "Wall")
                {
                    upB = true;
                }*/
        }

        if (Physics2D.Raycast(transform.position - transform.up * 0.55f, Vector2.down, 0.5f,layerMask))
        {
            downB = true;
            //RaycastHit2D down = Physics2D.Raycast(transform.position - transform.up * 0.55f, Vector2.down, 0.25f);
                /*if (down.collider.tag == "Wall")
                {
                    downB = true;
                }*/
        }

        if (Physics2D.Raycast(transform.position - transform.right * 0.55f, Vector2.left, 0.5f,layerMask))
        {
            leftB = true;
            /*RaycastHit2D left = Physics2D.Raycast(transform.position - transform.right * 0.55f, Vector2.left, 0.25f);
                if (left.collider.tag == "Wall")
                {
                    leftB = true;
                }*/
        }

        if (Physics2D.Raycast(transform.position + transform.right * 0.55f, Vector2.right, 0.5f,layerMask))
        {
            rightB = true;
            /*RaycastHit2D right = Physics2D.Raycast(transform.position + transform.right * 0.55f, Vector2.right, 0.25f);
                if (right.collider.tag == "Wall")
                {
                    rightB = true;
                }*/
        }
        if (isTile == false)
        {
            if (upB == true)
            {
                if (downB == true)
                {
                    if (leftB == true)
                    {
                        if (rightB == true)
                        {
                            //GameObject addTile = (GameObject)Instantiate(objects[0], transform.position, Quaternion.identity);
                            //addTile.transform.parent = transform;
                            Debug.Log("Changed transform!!");
                        }
                    }
                }
            }
        }
        else
        {
            return;
        }
    }
}
