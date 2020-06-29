using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikesBehavior : MonoBehaviour
{
  enum State
    {
        In,Out
    }
    State currstate;
    private Animator anim;
    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    public void ChangeState()
    {
        if (currstate == State.In){
            anim.SetBool("Out", true);
            currstate = State.Out;
                }
        else
        {
            anim.SetBool("Out", false);
            currstate = State.In;
        }
    }
    private void OnTriggerStay2D (Collider2D collision)
    {
        if(currstate == State.Out)
        {
            if(collision.gameObject.tag == "Player")
            {
                if (collision.gameObject.GetComponent<MoveScript>().isBlinking == false)
                {
                    collision.gameObject.GetComponent<MoveScript>().TakeDamage();

                }
                }
                else if(collision.gameObject.tag == "Zombie")
            {
                if(collision.gameObject.GetComponent<ZombieBehaviorScript>() != null)
                {
                    collision.gameObject.GetComponent<ZombieBehaviorScript>().TakeDamage(50f);
                }
            }
        }
        else
        {
            //do nothing
        }
    }
}
