using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretScript : MonoBehaviour
{
    public int MinRot, MaxRot;
    public Transform gun;
    public Quaternion targetRot;
   [SerializeField] private bool sawplayer;
        //AudioSource.PlayClipAtPoint(shotclip, transform.position);
   [SerializeField] private bool isrotating;
    RaycastHit2D hit;
    public float rotSpeed;
    public LineRenderer lr;
   [SerializeField] Vector3[] positions;
    [SerializeField] private GameObject player;
    public float HP;
    public GameObject bullet;
    public float firerate;
    float timer;
    public AudioClip ugh;
        private AudioSource src;
  [SerializeField]  MoveScript movescript;
    public GameObject floatingtext;
    public GameObject Blood;
    public GameObject bloodstain;
    int mask;
    void Start()
    {
        src = GetComponent<AudioSource>();
        timer = firerate;
        targetRot = (Quaternion.Euler(0, 0, Random.Range(MinRot, MaxRot)));
        Debug.Log(targetRot.eulerAngles);
        positions = new Vector3[2];
        lr.material.color = Color.red;
        player = GameObject.Find("Player");
        movescript = player.GetComponent<MoveScript>();
        mask =  ~(1 << 11);

}

void Update()
    {
        if (HP <= 0)
        {
            //Instantiate(Blood, this.transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        hit = Physics2D.Raycast(gun.position, transform.up, 15f, mask) ;
        if (hit.collider != null)
        {
            positions[1] = new Vector3(hit.transform.position.x, hit.transform.position.y, 0) ;
        }
  
        positions[0] = gun.position;
        lr.SetPosition(1, positions[1]);
        lr.SetPosition(0, positions[0]);
        Debug.DrawRay(transform.position,transform.up,Color.red);
        if(hit.collider != null)
        {
            if(hit.collider.gameObject.tag == "Player")
            {
                sawplayer = true;
                player = hit.collider.gameObject;
            }
            else
            {
                player = null;
                sawplayer = false;
            }
        }
        if (sawplayer == true)
        {
            SawPlayerBehavior();
        }
        else if (sawplayer == false)
        {
            NonSawPlayerBehavior();
        }
    }
    void NonSawPlayerBehavior()
    {
        if (isrotating != true) {
            targetRot = Quaternion.Euler(0, 0, Random.Range(MinRot, MaxRot));
            isrotating = true;
        }if(isrotating == true)
        {
            transform.Rotate(0, 0, rotSpeed * Time.deltaTime);
            if(transform.rotation == targetRot)
            {
                Debug.Log("Done!");
                isrotating = false;

            }
        }
    }
    void SawPlayerBehavior()
    {
        if (player != null) {
            Vector3 diff = player.transform.position- transform.position;
            float rotZ = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, rotZ -90);
            Shoot();
        }
    }
    void Shoot()
    {
        GameObject placeholderbullet;
        if (timer <= 0)
        {
            placeholderbullet = Instantiate(bullet, gun.position, transform.rotation);
            timer = firerate;
        }
        else
        {
            timer -= Time.deltaTime;
        }
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.tag == "Blow" || col.gameObject.tag == "Barrett")
        {
            TakeDamage(movescript.Damage);
        }
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
        HP -= dmg;
        Instantiate(Blood, this.transform.position, Quaternion.identity);
        Instantiate(bloodstain, this.transform.position, Quaternion.identity);
        AudioSource.PlayClipAtPoint(ugh, this.transform.position);
    }

}
