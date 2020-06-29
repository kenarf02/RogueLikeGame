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

    void OnEnable()
    {
        //Material material = new Material(Shader.Find("Sprites/Diffuse"));
        int rand = Random.Range(0, objects.Length);
        GameObject instance = (GameObject)Instantiate(objects[rand], transform.position, Quaternion.identity);
       
        instance.transform.parent = transform;
        if (this.gameObject.tag != "Zombie")
        {
            StartCoroutine("Raycast");
        }
    }

    public IEnumerator Raycast()
    {
        int i = 0;
        while (i <= 10)
        {
            shootRaycasts();
            yield return new WaitForSeconds(0.2f);
            i++;
        }
    }

    void shootRaycasts()
    {
        
        int layerMask = 1 << 11;
        layerMask = ~layerMask;
        if (Physics2D.Raycast(transform.position + transform.up * 0.55f, Vector2.up, 0.2f,layerMask))
        {
            // DRAW RAY
            //RaycastHit2D up = Physics2D.Raycast(transform.position + transform.up * 0.55f, Vector2.up, 0.25f);

            upB = true;
            
            /*if (up.collider.tag == "Wall")
                {
                    upB = true;
                }*/
        }

        if (Physics2D.Raycast(transform.position - transform.up * 0.55f, Vector2.down, 0.2f,layerMask))
        {
            downB = true;
            //RaycastHit2D down = Physics2D.Raycast(transform.position - transform.up * 0.55f, Vector2.down, 0.25f);
                /*if (down.collider.tag == "Wall")
                {
                    downB = true;
                }*/
        }

        if (Physics2D.Raycast(transform.position - transform.right * 0.55f, Vector2.left, 0.2f,layerMask))
        {
            leftB = true;
            /*RaycastHit2D left = Physics2D.Raycast(transform.position - transform.right * 0.55f, Vector2.left, 0.25f);
                if (left.collider.tag == "Wall")
                {
                    leftB = true;
                }*/
        }

        if (Physics2D.Raycast(transform.position + transform.right * 0.55f, Vector2.right, 0.2f,layerMask))
        {
            rightB = true;
            /*RaycastHit2D right = Physics2D.Raycast(transform.position + transform.right * 0.55f, Vector2.right, 0.25f);
                if (right.collider.tag == "Wall")
                {
                    rightB = true;
                }*/
        }
        if (gameObject.tag != "Template")
        {
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
                                GameObject addTile = (GameObject)Instantiate(objects[0], transform.position, Quaternion.identity);
                                addTile.transform.parent = transform;
                                //Debug.Log("Changed transform!!");
                            }
                        }
                    }
                }
            }
        }
        else
        {
            StopAllCoroutines();
            return;
        }
    }
}
