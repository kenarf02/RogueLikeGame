using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
public class ZombieBehaviorScript : MonoBehaviour {
	[SerializeField] private float Speed = 0;
	protected Transform Player;
    public GameObject coin;
	public GameObject Blood;
	public float HP = 100f;
	Spawner spawnerScript;
	protected MoveScript movescript;
	public GameObject Bloodstain;
[SerializeField]private AudioSource src;
	public AudioClip ugh;
	private Animator anim;
	protected bool stop = false;
    public GameObject floatingtext;
    //EliteObjs
    public GameObject HunnedPrefab,RewardPrefab,HealthUPPrefab,BoomPrefab,AmmoPrefab;
	void Start () {
		anim = GetComponent<Animator> ();
		Player = GameObject.FindGameObjectWithTag ("Player").transform;
		src = GetComponent<AudioSource> ();
		movescript = Player.GetComponent<MoveScript> ();
        if(GetComponent<EliteZombieBehavior>()!= null){
 if(GetComponent<EliteZombieBehavior>().elite == true){
    GameObject elitelight = Instantiate(Resources.Load<GameObject>("Prefabs/EliteParticles/ZombieLight"),transform.position,Quaternion.identity,transform);
                EliteZombieBehavior elite = GetComponent<EliteZombieBehavior>();
                SpriteRenderer sr = GetComponent<SpriteRenderer>();
                switch(elite.type){
case Elitetype.Health:
sr.color = new Color(1f,0.1933962f,0.1933962f);
elitelight.GetComponent<UnityEngine.Experimental.Rendering.Universal.Light2D>().color = new Color(1f,0.1933962f,0.1933962f);
HP += 50;
//add some cool particle effect
break;
case Elitetype.Boom:
sr.color = new Color (0.3773585f,0.3773585f,0.3773585f);
elitelight.GetComponent<UnityEngine.Experimental.Rendering.Universal.Light2D>().color = new Color(0.3773585f,0.3773585f,0.3773585f);
GetComponent<AIPath>().maxSpeed +=1;
//add some cool particle effect
break;
case Elitetype.Money:
sr.color = new Color (0.6138519f,1f,0.495283f);
elitelight.GetComponent<UnityEngine.Experimental.Rendering.Universal.Light2D>().color = new Color(0.6138519f,1f,0.495283f);
GetComponent<AIPath>().maxSpeed +=1;
HP +=100;
//add some cool particle effect
break;
case Elitetype.Ammo:
sr.color = new Color (0.8773585f,0.4157616f,0f);
elitelight.GetComponent<UnityEngine.Experimental.Rendering.Universal.Light2D>().color = new Color(0.8773585f,0.4157616f,0f);
HP +=100;
break;
case Elitetype.Item:
sr.color = new Color (1f,0.9584457f,0f);
elitelight.GetComponent<UnityEngine.Experimental.Rendering.Universal.Light2D>().color = new Color(1f,0.9584457f,0f);
HP +=200;
//add some cool particle effect
break;
 }      
    }
        }
    }
    void Update()
    {
        /*
        Vector3 difference = Player.transform.position - transform.position;
		float rotZ = Mathf.Atan2 (difference.y, difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler (0, 0, rotZ + 90);
		movescript = Player.GetComponent<MoveScript> ();
		
        if (!stop) {
            transform.position = Vector2.MoveTowards(transform.position, Player.transform.position, Speed * Time.deltaTime);
		}*/
        // Physics2D.IgnoreLayerCollision(11, 11);
        if (HP <= 0)
        {
              if (this.gameObject.GetComponent<BlowZombieBehavior>() == null)
              {
            Die();
               }
        }
    }


    public void OnCollisionEnter2D(Collision2D col)
        {
            if (col.gameObject.tag == "Player")
            {

/*           if (movescript.IsDead == true)
                {
                    stop = true;
                }*/      
            }
            if (col.gameObject.tag == "Zombie")
            {

              /*  if (movescript.IsDead == true)
                {
                    stop = true;

                }
                */
            }
            if (col.gameObject.tag == "Ammorefillbox")
            {
                Destroy(col.gameObject);
            }
            if (col.gameObject.tag == "Blow" || col.gameObject.tag == "Barrett" || col.gameObject.tag == "BlowEngineer")
            {
            movescript = Player.GetComponent<MoveScript>();
            TakeDamage(movescript.Damage);
            }
            if (col.gameObject.tag == "Bomb")
            {
            Player = GameObject.FindGameObjectWithTag("Player").transform;
            GameObject floatingtextplacehold;
                movescript = Player.gameObject.GetComponent<MoveScript>();
                floatingtextplacehold = Instantiate(floatingtext, this.transform.position + new Vector3(0, 0.7f, 0), Quaternion.identity);
                if (col.gameObject.GetComponent<Rigidbody2D>().mass <= 25)
                {
                    floatingtextplacehold.GetComponent<TextMesh>().color = Color.yellow;
                }
                else if (col.gameObject.GetComponent<Rigidbody2D>().mass > 25 && col.gameObject.GetComponent<Rigidbody2D>().mass <= 75)
                {
                    Color orange = new Color(0.990566f, 0.6313726f, 0.3490196f, 1);
                    floatingtextplacehold.GetComponent<TextMesh>().color = orange;
                }
                else if (col.gameObject.GetComponent<Rigidbody2D>().mass > 75)
                {
                    floatingtextplacehold.GetComponent<TextMesh>().color = Color.red;
                }
                floatingtextplacehold.GetComponent<TextMesh>().text = col.gameObject.GetComponent<Rigidbody2D>().mass.ToString();
                floatingtextplacehold = null;
                HP -= col.gameObject.GetComponent<Rigidbody2D>().mass;
                Instantiate(Blood, this.transform.position, Quaternion.identity);
                Instantiate(Bloodstain, this.transform.position, Quaternion.identity);
                src.PlayOneShot(ugh);
            }
        
    }
        
    public void OnTriggerEnter2D(Collider2D collision)
    {
        
            if (collision.gameObject.tag == "Gas")
            {
                TakeDamage(50f);
            }
        Debug.Log(collision.gameObject.tag);
        
    }
    public virtual void Die(){
//ACTIVATES ONLY IF THE ZOMBIE IS THE ELITE
        if(GetComponent<EliteZombieBehavior>()){
            if(GetComponent<EliteZombieBehavior>().elite == true){
                EliteZombieBehavior elite = GetComponent<EliteZombieBehavior>();
                switch(elite.type){
case Elitetype.Health:
Instantiate(HealthUPPrefab,this.transform.position,Quaternion.identity);break;
case Elitetype.Boom:
Instantiate(BoomPrefab,this.transform.position,transform.rotation);break;
case Elitetype.Money:
Instantiate(HunnedPrefab,this.transform.position,Quaternion.identity);
break;
case Elitetype.Item:
Instantiate(RewardPrefab,this.transform.position,Quaternion.identity);
break;
case Elitetype.Ammo:
Instantiate(AmmoPrefab,this.transform.position,Quaternion.identity);
break;
                }
            }else{
                 Instantiate(coin,this.transform.position,Quaternion.identity);
            }
        }else{
            Instantiate(coin,this.transform.position,Quaternion.identity);
        }
        //ACTIVATES EITHER WAY
        if(Player.GetComponent<MoveScript>().PlayerHasCannibalism == true)
        {
            Debug.Log("Player has cannibalism");
            int randomint = Random.Range(0, 100);
            if(randomint > 95)
            {
                if (movescript.HP < movescript.HPcontainers)
                {
                    Debug.Log("Health restored");
                    movescript.ActivateVampirismEffect();
                    //play some cool animation and sound of drinking blood or whatevs
                }
            }
            else
            {
                //do nothing
            }
        }
		Destroy (this.gameObject);
	}
    public void TakeDamage(float dmg)
    {
        GameObject floatingtextplacehold;
        floatingtextplacehold = Instantiate(floatingtext, this.transform.position + new Vector3(0, 0.7f, 0), Quaternion.identity);
        if (dmg <= 25)
        {
            floatingtextplacehold.GetComponent<TextMesh>().color = Color.yellow;
        }
        else if (dmg > 25 && dmg <= 75)
        {
            Color orange = new Color(0.990566f, 0.6313726f, 0.3490196f, 1);
            floatingtextplacehold.GetComponent<TextMesh>().color = orange;
        }
        else if (dmg > 75)
        {
            floatingtextplacehold.GetComponent<TextMesh>().color = Color.red;
        }
        floatingtextplacehold.GetComponent<TextMesh>().text = dmg.ToString();
        floatingtextplacehold = null;
        HP -=dmg;
        Instantiate(Blood, this.transform.position, Quaternion.identity);
        Instantiate(Bloodstain, this.transform.position, Quaternion.identity);
        if (src != null)
        {
            src.PlayOneShot(ugh);
        }
        else
        {
            src = GetComponent<AudioSource>();
            src.PlayOneShot(ugh);
        }
    }
}
