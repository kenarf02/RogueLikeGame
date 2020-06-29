using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpideryBoyBehavior : MonoBehaviour
{
    private enum state{Movestate, Shotstate }
    public float Health;
    private GameObject Player;
    public Transform Shotpoint;
    public Vector3 MoveDir;
    public float moveSpeed;
    public float shotRate;
    private float timer;
    public GameObject floatingtext;
    public AudioClip ugh;
    public GameObject bullet;
    private AudioSource asrc;
    private RaycastHit2D ray;
    private bool isshooting;
    state Currstate;

    void Start()
    {
        asrc = GetComponent<AudioSource>();   
        Player = GameObject.Find("Player");
        timer = shotRate;
        MoveDir = transform.right;
    }

    void Update()
    {
        ray = Physics2D.Raycast(Shotpoint.transform.position,transform.up,10f);
        if(ray){
            if(ray.collider.tag == "Player"){
                Currstate = state.Shotstate;
            }else{
                Currstate = state.Movestate;
            }
        }
        DetectCollision();
        if (Currstate == state.Movestate)
        {
            if(!isshooting){
            Move();
            }
        }else if(Currstate == state.Shotstate ){
            if(!isshooting){
            StartCoroutine(Shot());
        }
        }
        Shoot();
        if(Health <= 0)
        {
            Die();
        }
    }
    private void Move()
    {
            transform.Translate(MoveDir * Time.deltaTime * moveSpeed);
    }
    private void Shoot()
    {
        if(timer <= 0)
        {
            timer = shotRate;
             Currstate = state.Shotstate;
           
        }
        else
        {
            timer -= Time.deltaTime;
        }
    }
    private void DetectCollision()
    {

    RaycastHit2D moveray = Physics2D.Raycast(Shotpoint.transform.position, MoveDir, 1f);
    if (moveray.collider != null)
    {
            if (Currstate != state.Shotstate)
            {
                MoveDir = -MoveDir;
            }
    }
    }
 private IEnumerator Shot(){
     isshooting = true;
     Instantiate(bullet,Shotpoint.transform.position,transform.rotation);
     yield return new WaitForSeconds(0.8f);
     Instantiate(bullet,Shotpoint.transform.position,transform.rotation);
    yield return new WaitForSeconds(0.8f);
     Instantiate(bullet,Shotpoint.transform.position,transform.rotation);
     yield return new WaitForSeconds(1f);
     Currstate = state.Movestate;
          isshooting = false;
     StopAllCoroutines();
 }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Blow"||collision.gameObject.tag=="Bomb");
        {
            TakeDamage();
        }
    }
    public void TakeDamage()
    {
       MoveScript movescript = Player.GetComponent<MoveScript>();
        GameObject floatingtextplacehold;
        movescript = Player.gameObject.GetComponent<MoveScript>();
        floatingtextplacehold = Instantiate(floatingtext, this.transform.position + new Vector3(0, 0.7f, 0), Quaternion.identity);
        if (movescript.Damage <= 25)
        {
            floatingtextplacehold.GetComponent<TextMesh>().color = Color.yellow;
        }
        else if (movescript.Damage > 25 && movescript.Damage <= 75)
        {
            Color orange = new Color(0.990566f, 0.6313726f, 0.3490196f, 1);
            floatingtextplacehold.GetComponent<TextMesh>().color = orange;
        }
        else if (movescript.Damage > 75)
        {
            floatingtextplacehold.GetComponent<TextMesh>().color = Color.red;
        }
        floatingtextplacehold.GetComponent<TextMesh>().text = movescript.Damage.ToString();
        floatingtextplacehold = null;
        Health -= movescript.Damage;
        asrc.PlayOneShot(ugh);
    }
    void Die()
    {
        Destroy(gameObject);
    }
    
}

