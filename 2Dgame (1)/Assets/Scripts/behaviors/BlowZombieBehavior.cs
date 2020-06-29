using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlowZombieBehavior : ZombieBehaviorScript
{
    private AudioSource src;
    public GameObject BlowItem;
    ZombieBehaviorScript zb;
    void Start()
    {
        src = GetComponent<AudioSource>();
        Player = GameObject.FindGameObjectWithTag("Player").transform;
        zb = GetComponent<ZombieBehaviorScript>();
        movescript = Player.GetComponent<MoveScript>();
    }
    void Update()
    {
        

        if (zb.HP <= 0)
        {
               Die();
        }
    }

   public override void Die()
    {
        
        base.Die();
        Instantiate(BlowItem, this.gameObject.transform.position, this.transform.rotation);
    }
    private new void OnCollisionEnter2D(Collision2D col)
    {
        movescript = Player.GetComponent<MoveScript>();
        base.OnCollisionEnter2D(col);
    }
    private new void OnCollisionStay2D(Collision2D col)
    {
        movescript = Player.GetComponent<MoveScript>();
    }

}
