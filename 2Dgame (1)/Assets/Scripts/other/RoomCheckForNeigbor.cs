using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomCheckForNeigbor : MonoBehaviour
{
    public LayerMask layermask;

public bool leftn,rightn,upn,down;


public void raycast(){
   int minX = -30;
   int minY= -30;
   int maxX=30;
   int maxY=30;
Debug.DrawRay(transform.position + transform.up * 0.55f, Vector2.up* 25,Color.red,100f);
        if (Physics2D.Raycast(transform.position + transform.up * 0.55f, Vector2.up, 20f,layermask))
        {         
            upn = true; 
        if(transform.position.y==maxY){
            upn = false;
        }
        
        }
Debug.DrawRay(transform.position - transform.up * 0.55f, Vector2.down* 25, Color.red,100f);
        if (Physics2D.Raycast(transform.position - transform.up * 0.55f, Vector2.down, 20f,layermask))
        {
              down = true;
            if(transform.position.y == minY){
                down = false;
            }
                
        }
 Debug.DrawRay(transform.position - transform.right * 0.55f, Vector2.left* 25,Color.red,100f);
        if (Physics2D.Raycast(transform.position - transform.right * 0.55f, Vector2.left,20f,layermask))
        {
            leftn = true;
            if(transform.position.x == minX){
                leftn = false;
            }
        }
  Debug.DrawRay(transform.position + transform.right * 0.55f, Vector2.right* 20, Color.red,100f);
        if (Physics2D.Raycast(transform.position + transform.right * 0.55f, Vector2.right, 20f,layermask))
        {

                 rightn = true;
                if(transform.position.x == maxX){
                    rightn = false;
                }
        }
        }
}

