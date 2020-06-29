using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public class MoveScript : MonoBehaviour
{
    [Header("weapons")]
    public Item iteminrangeshop;
    public float totalbullets;
    public float bulletsinclip;
    public Weapon primary;
    public Weapon secondary;
    public bool paused;
    public bool dialogueon;
    public Weapon EquippedWeapon;
    [Header("Player attributes")]
    public float HP;
    public int HPcontainers = 0;
    public UsableItem activeusable;
    [SerializeField] public float MoveSpeed = 0;
    public float Damage = 1;
    public float FireRate;
    private float timeBtwShot;
    [SerializeField] public float currentTimeActiveitem;
    public GameObject projectile = null;
    public float Range;
    [SerializeField] private SpriteRenderer looks;
    public Sprite deadSprite;
    [Header("Movement")]
    [SerializeField] float v, h;
    Vector3 Move;
    [SerializeField] private float mgnt;
    public ItemScript itemList;
    public float offset;
    public bool IsDead = false;
    bool Reloading = false;
    public int money;
    public bool isBlinking;

    GameUIBehavior Uimanage;
    public GameObject particles;
    public Transform shotpoint;
    public GameObject Blood;
    private Animator camAnim;
    private Animator selfanim;
    private GameObject cameramain;
    Rigidbody2D rb;
    public GameObject Bloodstain;

    public AudioClip pullout;
    public AudioSource src;
    //AudioClip ReloadSound;
    public AudioClip ugh;
    public AudioClip steps;
    [SerializeField]private AudioClip shot;
    public AudioClip empty;
    public ParticleSystem shells;
    GameObject Spawner;
    Spawner spawn;

    //op tostuje złoto
    float bulletstoreload;
    public float sprayRotation;
    float maxsway;
    float minsway;
    [SerializeField] private bool isshooting;
    void Start()
    {
        selfanim = GetComponent<Animator>();
        looks = GetComponent<SpriteRenderer>();
        Spawner = GameObject.Find("GameManager");
        itemList = Spawner.GetComponent<ItemScript>();
        src = GetComponent<AudioSource>();
        cameramain = GameObject.FindGameObjectWithTag("MainCamera");
        camAnim = cameramain.GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        primary = itemList.Weapons[3];
        secondary = itemList.Weapons[4];
        EquippedWeapon = primary;
        activeusable = itemList.Usables[2];
        currentTimeActiveitem = activeusable.regentime;

    }

    void Update()
    {
        maxsway = EquippedWeapon.maxSway;
        minsway = EquippedWeapon.minSway;
        totalbullets = EquippedWeapon.TotalBullets;
        Damage = EquippedWeapon.Damage;
        FireRate = EquippedWeapon.RateOfFire;
        bulletsinclip = EquippedWeapon.BulletsInClip;
        projectile = EquippedWeapon.bulletPrefab;
        Range = EquippedWeapon.Range;
        shot = EquippedWeapon.gunSound;
        bulletstoreload = (EquippedWeapon.bulletsincliplimit - EquippedWeapon.currentclip);
        looks.sprite = EquippedWeapon.looks;
        shotpoint.transform.localPosition = EquippedWeapon.ShootingSpot;
        //WeaponSwitch
        if (/*switch it later to button*/ Input.GetButtonDown("Primary"))
        {
            AudioSource.PlayClipAtPoint(pullout, this.transform.position);
            EquippedWeapon = primary;
            Debug.Log("Primary"+EquippedWeapon.Name);
        }
        if (/*switch it later to button*/Input.GetButtonDown("Secondary")&&secondary != null)
        {
            AudioSource.PlayClipAtPoint(pullout, this.transform.position);
            EquippedWeapon = secondary;
            Debug.Log("Secondary"+EquippedWeapon.Name);
        }else if (secondary == null)
        {
            EquippedWeapon = primary;
        }
        //use usable

        if (currentTimeActiveitem >= activeusable.regentime)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                activeusable.OnUse();
                currentTimeActiveitem = 0;
            }
        }
        else
        {
            currentTimeActiveitem += Time.deltaTime;
        }
        
        //rotation control
        if (!IsDead)
        {
            if (paused == false && dialogueon == false)
            {
                Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
                float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Euler(0, 0, rotZ + offset);
            }
        }
        //Get Axes for movement
        if (IsDead != true)
        {
            v = Input.GetAxis("Vertical");
            h = Input.GetAxis("Horizontal");
            Move = new Vector3(h, v, 0);
        }
        /////animation management
        if (Move.magnitude != 0 && !src.isPlaying)
        {
            if (IsDead == false && !paused&& dialogueon == false)
            {
                src.loop = true;
                src.clip = steps;
                src.Play();
            }
            
        }
        else if (Move.magnitude == 0 && src.isPlaying)
        {
            src.Pause();
            src.clip = null;
        }
        if (!IsDead)
        {
            if (paused == false&& dialogueon == false)
            {
                transform.Translate(Vector3.ClampMagnitude(Move,1f)*Time.deltaTime*MoveSpeed, Space.World);
            }
        }
        //die management 
        if (HP <= 0)
        {
            Die();
        }
        //shooting management
        if (timeBtwShot <= 0)
        {
            if (!IsDead)
            {
                if (paused == false && dialogueon == false)
                {
                    if (Input.GetButtonDown("Reload"))
                    {

                        StartCoroutine("Reload");
                    }
                    if (Input.GetButton("Fire1"))
                    {
                        if (EquippedWeapon.currentclip > 0)
                        {
                            if (Reloading == false)
                            {
                                AudioSource.PlayClipAtPoint(shot, this.transform.position);
                                sprayRotation = Random.Range(minsway, maxsway);
                                Instantiate(projectile, shotpoint.position, transform.rotation * Quaternion.Euler(0, 0, sprayRotation));
                                Instantiate(particles, shotpoint.position, transform.rotation);
                                EquippedWeapon.currentclip--;
                                timeBtwShot = FireRate;
                                camAnim.SetTrigger("Shake");
                           
                            }
                        
                        }
                        else
                        {
                            StartCoroutine("Reload");
                        }
                    }
                   

                }
            }
        }
        else
        {
            timeBtwShot -= Time.deltaTime;
        }
      /*  if (Input.GetButtonDown("Fire1") && Reloading == false)
        {
            isshooting = true;
        }
        if (Input.GetButtonUp("Fire1"))
        {
            isshooting = false;
        }
        if (isshooting == true)
        {
            if (!shells.isPlaying && Reloading == false)
            {
               var emission = shells.emission;
                emission.rateOverTime =1/EquippedWeapon.RateOfFire;
               // shells.maxParticles =;
                shells.Play();
             
            }
            
        }
        else*/
        {
            shells.Stop();
        }
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Ammorefillbox")
        {
            EquippedWeapon.TotalBullets += 10;
            AudioSource.PlayClipAtPoint(pullout, this.transform.position);
            Destroy(other.gameObject);
        }
        if (other.gameObject.tag == "Zombie" || other.gameObject.tag == "Acid" || other.gameObject.tag == "Bomb") 
        {
            if (IsDead == false&&isBlinking == false)
            {
                TakeDamage();
            }
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if ( other.gameObject.tag == "Acid")
        {
            if (IsDead == false && isBlinking == false)
            {
                TakeDamage();
            }
        }
    }
    void Die()
    {
        IsDead = true;
        GetComponent<SpriteRenderer>().sprite = deadSprite;
        Invoke("ShowDie", 3f);
        src.loop = false;
        src.Pause();
    }
  void ShowDie()
    {
        Uimanage = GetComponent<GameUIBehavior>();
        Uimanage.ShowDie();
    }

    void OnItemPickup()
    {

    }
    public void TakeDamage()
    {
        src.loop = false;
        AudioSource.PlayClipAtPoint(ugh, this.transform.position);
        HP -= 1;
        Instantiate(Blood, this.transform.position, Quaternion.identity);
        Instantiate(Bloodstain, this.transform.position, Quaternion.identity);
        camAnim.SetTrigger("Shake");
        StartCoroutine("blinkingstate");
    }
    public IEnumerator blinkingstate()
    {

        this.gameObject.layer = 13;
        selfanim.SetTrigger("Blink");
        isBlinking = true;
        MoveSpeed *= 2;
        yield return new WaitForSecondsRealtime(1f);
        isBlinking = false;
        this.gameObject.layer = 8;
        MoveSpeed /= 2;
    }
    public void instantiaterckt(GameObject rckt,Vector3 pos)
    {
        Instantiate(rckt, pos, Quaternion.identity);
    }
    public IEnumerator Reload()
    {
        if (IsDead == false)
        {
            if (Reloading == false)
            {
                
                if (EquippedWeapon.currentclip < EquippedWeapon.bulletsincliplimit)
                {
                    Reloading = true;
                    shells.Stop();
                    if (EquippedWeapon.TotalBullets !=  EquippedWeapon.currentclip && bulletstoreload <= EquippedWeapon.TotalBullets && EquippedWeapon.TotalBullets != 0)
                    {
                        AudioSource.PlayClipAtPoint(EquippedWeapon.reloadSound, this.transform.position);
                        yield return new WaitForSeconds(EquippedWeapon.reloadSound.length);
                        EquippedWeapon.TotalBullets = EquippedWeapon.TotalBullets - (bulletstoreload);
                        EquippedWeapon.currentclip= EquippedWeapon.BulletsInClip;

                        yield return new WaitForSeconds(0.25f);
                        Reloading = false;
                    }

                    else if (bulletstoreload > EquippedWeapon.TotalBullets && EquippedWeapon.TotalBullets != 0)
                    {

                        AudioSource.PlayClipAtPoint(EquippedWeapon.reloadSound, this.transform.position);
                        yield return new WaitForSeconds(EquippedWeapon.reloadSound.length);
                        EquippedWeapon.currentclip = (EquippedWeapon.TotalBullets);
                        EquippedWeapon.TotalBullets = 0;

                        yield return new WaitForSeconds(0.25f);
                        Reloading = false;
                    }

                    else if (EquippedWeapon.currentclip == 0 && EquippedWeapon.TotalBullets == 0)
                    {
                        AudioSource.PlayClipAtPoint(empty, this.transform.position);
                        yield return new WaitForSeconds(FireRate);
                        Reloading = false;
                    }
                    
                }
            }
        }
    }
}



