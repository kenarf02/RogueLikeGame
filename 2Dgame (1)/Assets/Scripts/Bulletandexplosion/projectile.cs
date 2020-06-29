using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class projectile : MonoBehaviour
{
    public float lifeTime;
    public AudioSource src = null;
    public AudioClip Bang = null;
    public float speed;
    public GameObject DestroyEffect = null;
    public GameObject DestroyStain = null;
    public GameObject[] holes;
    private GameObject cameramain;
    [SerializeField] private GameObject player;
    [SerializeField] private MoveScript move;
    bool iscollision;
    int counter=0;
    private void Start()
    {
        holes = GameObject.FindGameObjectsWithTag("Hole");
        foreach (GameObject hole in holes){
            Physics2D.IgnoreCollision(hole.gameObject.GetComponent<Collider2D>(), this.gameObject.GetComponent<Collider2D>());
        }
        if (this.gameObject.tag != "Acid")
        {
            player = GameObject.FindGameObjectWithTag("Player");
            move = player.GetComponent<MoveScript>();
            lifeTime = move.Range;
        }

        src = GameObject.Find("GameManager").GetComponent<AudioSource>();
        if (src != null)
        {
            src.clip = Bang;
        }
        else
        {
            Bang = null;
        }
        Invoke("DestroyProjectile", lifeTime);
    }

    void Update()
    {
        transform.Translate(Vector2.up * 1 * speed * Time.deltaTime);
    }
    void DestroyProjectile()
    {
  
        if (src != null&&Bang!=null)
        {
           src.PlayOneShot(Bang);
    
        }
        if (DestroyEffect != null)
        {
            Instantiate(DestroyEffect, transform.position, Quaternion.identity);
        }
        if (DestroyStain != null)
        {
            Instantiate(DestroyStain, transform.position, Quaternion.identity);

        }
   

            Destroy(gameObject);
        
    }
  
    void OnCollisionEnter2D(Collision2D other)
    {
             if(gameObject.tag == "BlowEngineer"){
if(counter <=2){
    if(!iscollision){
Debug.Log("Bounce");
iscollision =true;
counter++;
      transform.up *=-1;
               transform.Rotate(new Vector3(0,0,Random.Range(-30,30)));
    }
    
                }else{
DestroyProjectile();
                }
         }
      if(other.gameObject.tag == "Wall"||other.gameObject.tag == "Untagged"||other.gameObject.tag == "Player")
        {
            if(gameObject.tag != "BlowEngineer"){
     DestroyProjectile();   
            }
        }

        if (other.gameObject.tag == "Zombie" || other.gameObject.tag == "Boss" || other.gameObject.tag == "AccessCard" || other.gameObject.tag=="EngineerBullet")
        {
            if (this.gameObject.tag != "Barrett" || gameObject.tag != "Acid")
            {
                DestroyProjectile();
            }
            else
            {
                Physics2D.IgnoreCollision(other.gameObject.GetComponent<Collider2D>(), GetComponent<Collider2D>());
            }
        }
        
    }
    private void OnCollisionStay2D(Collision2D other) {
   
    }
    private void OnCollisionExit2D(Collision2D other) {
         if(gameObject.tag == "BlowEngineer"){
iscollision = false;
         }
    }
    }

  

