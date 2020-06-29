using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
public class MoveScript : MonoBehaviour
{
    [HideInInspector] public float basemovespeed;
    [Header("weapons")]
    bool inrangeToPickup;
    public GameObject itemtopickup;
    public Item iteminrangeshop;
    public float totalbullets;
    public float bulletsinclip;
    public Weapon primary;
    public Weapon secondary;
    public bool paused;
    public bool MapOn;
    public bool dialogueon;
    public Weapon EquippedWeapon;
    [Header("Player attributes")]
    public float HP;
    public int HPcontainers = 0;
    public UsableItem activeusable;
    [SerializeField] public float MoveSpeed = 0;
    public float Damage = 1;
    public float bonusDmg = 0;
    public float FireRate;
    private float timeBtwShot;
    [SerializeField] public float currentTimeActiveitem;
    public GameObject projectile = null;
    public float Range;
    [SerializeField] private SpriteRenderer looks;
    public GameObject dropitem;
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
    public GameObject PickupText;
    GameUIBehavior Uimanage;
    public GameObject particles;
    public Transform shotpoint;
    public GameObject Blood;
    public GameObject vampirismparticle;
    private Animator camAnim;
    private Animator selfanim;
    private GameObject cameramain;
    Rigidbody2D rb;
    public GameObject Bloodstain;
    public bool isCutscene;
    public AudioClip pullout;
    public AudioSource src;
    public AudioSource srctwo;
    //AudioClip ReloadSound;
    public AudioClip ugh;
    public AudioClip HeartBeat;
    public AudioClip DropSound;
    public AudioClip steps;
    public AudioClip pickupsound;
    public AudioClip vampiresound;
    [SerializeField] private AudioClip shot;
    public AudioClip empty;
    public ParticleSystem shells;
    GameObject Spawner;
    Spawner spawn;
    public bool roomison;
    public bool PlayerHasCannibalism;
    //op tostuje złoto
    float bulletstoreload;
    public float sprayRotation;
    float maxsway;
    float minsway;
    public List<Item> InventoryList;
    [SerializeField] private bool isshooting;
    public bool wasdialogueShop;
    public bool hasShield;
    private bool shieldloading;
    public int shields;
    public bool hasBalaclava;
    public GameObject balaclava;
    public bool hascharger;
    public bool hasMagnet;
    public bool hasDiscount;
    public GameObject GasParticle;
    [Header("Gui")]
    GameUIBehavior guib;
    public List<GameObject> visitedRooms;
    public GameObject currentroom;
    public GameObject legs;
    private Animator legsanimator;
    public LayerMask movemask;
    void Start()
    {
        
        InventoryList = new List<Item>();
        basemovespeed = MoveSpeed;
        Application.targetFrameRate = 60;
        guib = GetComponent<GameUIBehavior>();
        selfanim = GetComponent<Animator>();
        looks = GetComponent<SpriteRenderer>();
        Spawner = GameObject.Find("GameManager");
        itemList = Spawner.GetComponent<ItemScript>();
        itemList.Initialize();
        src = GetComponent<AudioSource>();
        cameramain = GameObject.FindGameObjectWithTag("MainCamera");
        camAnim = cameramain.GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        primary = itemList.Weapons[1];
        secondary = null;
        EquippedWeapon = primary;
        activeusable = null;
        legsanimator = legs.GetComponent<Animator>();
        if (activeusable != null)
        {
            currentTimeActiveitem = activeusable.regentime;
        }
        srctwo = Spawner.GetComponent<AudioSource>();
    visitedRooms = new List<GameObject>();
    }

    void Update()
    {
        bool heartbeating = false;
        maxsway = EquippedWeapon.maxSway;
        minsway = EquippedWeapon.minSway;
        totalbullets = EquippedWeapon.TotalBullets;
        Damage = EquippedWeapon.Damage + bonusDmg;
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
            src.PlayOneShot(pullout);
            EquippedWeapon = primary;
            if(hasBalaclava == true)
            {
                balaclava.transform.localPosition = new Vector3(0, 0.008f, 0);
               
            }
            Debug.Log("Primary" + EquippedWeapon.Name);
        }
        if (/*switch it later to button*/Input.GetButtonDown("Secondary") && secondary != null && secondary.Type == "Weapon")
        {
            src.PlayOneShot(pullout);
            EquippedWeapon = secondary;
            if (hasBalaclava == true)
            {
                balaclava.transform.localPosition = new Vector3(0, 0.008f, 0);
               
            }
            Debug.Log("Secondary" + EquippedWeapon.Name);
        }
        else if (secondary == null || secondary.Type != "Weapon")
        {
            secondary = null;
            EquippedWeapon = primary;
        }
        if (hasShield)
        {
            if(shields < HP && shieldloading == false)
            {
                shieldloading = true;
                StartCoroutine(giveshield());
            }
        }
        //use usable
        if (activeusable != null && dialogueon == false)
        {
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
        }//heart beating sound while 1HP
        if (!IsDead && HP == 1 && heartbeating == false)
        {
            if (src.isPlaying != true && !dialogueon)
            {
                src.clip = HeartBeat;
                src.loop = true;
                src.Play();
            }

        }
        else
        {
            src.clip = steps;
        }
        //rotation control
        if (!IsDead)
        {
            if (paused == false && dialogueon == false && MapOn == false)
            {
                Vector3 difference = cameramain.GetComponent<Camera>().ScreenToWorldPoint(Input.mousePosition) - transform.position;
                float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Euler(0, 0, rotZ + offset);
            }
        }
        //Get Axes for movement
        if (IsDead != true && !isCutscene)
        {
            v = Input.GetAxis("Vertical");
            h = Input.GetAxis("Horizontal");
            Move = new Vector3(h, v, 0);
        }else
        {
            Move = new Vector3(0, 0, 0);
        }
        /////animation management
        if (Move.magnitude != 0 )
        {
                            legsanimator.SetBool("Walk",true);
                            if(src.isPlaying ==false){

            if (IsDead == false && !paused && dialogueon == false && src.clip == steps && MapOn == false)
            {
                src.loop = true;
                src.clip = steps;
                src.Play();
            }
                            }

        }
        else if (Move.magnitude == 0  )
        {
            legsanimator.SetBool("Walk",false);
            if(src.clip == steps){
            src.clip = null;
            }
        }
        if (!IsDead)
        {
            if (paused == false && dialogueon == false  && MapOn == false)
            {
                transform.Translate(Vector3.ClampMagnitude(Move, 1f) * Time.deltaTime * MoveSpeed, Space.World);
            }
        }
        if (inrangeToPickup == true && itemtopickup != null)
        {
            if((itemtopickup.transform.position - this.gameObject.transform.position).magnitude <= 3&& itemtopickup.GetComponent<DropBehavior>().thisitem != null) { 
            PickupText.SetActive(true);
            PickupText.transform.position = itemtopickup.transform.position + new Vector3(0, 1, 0);
            PickupText.GetComponent<TextMesh>().text = "Press 'f' to pick up " + itemtopickup.GetComponent<DropBehavior>().thisitem.Name;
            PickupText.transform.eulerAngles = new Vector3(0, 0, 0);
            if (Input.GetKeyDown(KeyCode.F))
            {
                    if ((transform.position - itemtopickup.transform.position).magnitude < 3)
                    {
                        Debug.Log("Pickup");
                        if (itemtopickup.GetComponent<DropBehavior>().thisitem.Type == "Weapon")
                        {
                            if (secondary != null)
                            {
                                drop(secondary);
                                secondary = (itemtopickup.GetComponent<DropBehavior>().thisitem as Weapon);
                                Destroy(itemtopickup);
                                src.PlayOneShot(pickupsound);
                            }
                            else
                            {
                                secondary = (itemtopickup.GetComponent<DropBehavior>().thisitem as Weapon);
                                Destroy(itemtopickup);
                                src.PlayOneShot(pickupsound);
                            }
                        }
                        else if (itemtopickup.GetComponent<DropBehavior>().thisitem.Type == "Usable")
                        {
                            if (activeusable != null)
                            {
                                drop(activeusable);
                                activeusable = (itemtopickup.GetComponent<DropBehavior>().thisitem as UsableItem);
                                Destroy(itemtopickup);
                                src.PlayOneShot(pickupsound);
                            }
                            else
                            {
                                activeusable = itemtopickup.GetComponent<DropBehavior>().thisitem as UsableItem;
                                Destroy(itemtopickup);
                                src.PlayOneShot(pickupsound);
                            }
                        }
                        else if (itemtopickup.GetComponent<DropBehavior>().thisitem.Type
                            == "Passive")
                        {
                            Passive placehold = itemtopickup.GetComponent<DropBehavior>().thisitem as Passive;
                            placehold.OnPickup();

                            Destroy(itemtopickup);
                            placehold = null;
                        }
                    }
                }
            }
            else
            {
                inrangeToPickup = false; itemtopickup = null;
                PickupText.SetActive(false);
            }
        }
        else if (inrangeToPickup == false || itemtopickup == null)
        {
            PickupText.SetActive(false);
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
                if (paused == false && dialogueon == false && MapOn == false)
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
                                if (!isCutscene)
                                {
                                    
                                        src.PlayOneShot (shot);
                                    
                                    sprayRotation = Random.Range(minsway, maxsway);
                                    Instantiate(projectile, shotpoint.position, transform.rotation * Quaternion.Euler(0, 0, sprayRotation));
                                    Instantiate(particles, shotpoint.position, transform.rotation);
                                    EquippedWeapon.currentclip--;
                                    timeBtwShot = FireRate;
                                    camAnim.SetTrigger("Shake");
                                }
                            }

                        }
                        else if (EquippedWeapon.currentclip <= 0 && EquippedWeapon.TotalBullets >= 0)
                        {
                            StartCoroutine("Reload");
                        }
                        else
                        {
                            //do nothing
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
    private void OnCollisionStay2D(Collision2D other) {
        if(other.gameObject.tag == "Wall"||other.gameObject.tag =="Door"){
            MoveSpeed = 2f;
        }
    }
    private void OnCollisionExit2D(Collision2D other) {
         if(other.gameObject.tag == "Wall"||other.gameObject.tag =="Door"){
            MoveSpeed = basemovespeed;
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Door")
        {
            MoveSpeed = basemovespeed;
        }
        if (other.gameObject.tag == "Ammorefillbox")
        {
            EquippedWeapon.TotalBullets += 10;
            srctwo.PlayOneShot (pullout);
            Destroy(other.gameObject);
        }
        if (other.gameObject.tag == "Zombie" || other.gameObject.tag == "Acid" || other.gameObject.tag == "Bomb" || other.gameObject.tag == "Boss" || other.gameObject.tag == "EngineerBullet"||other.gameObject.tag == "SpinMachine")

        {
            if (IsDead == false && isBlinking == false)
            {
                TakeDamage();
            }
        }
        if(other.gameObject.tag == "Door")
        {
            if(isBlinking == true)
            {
                isBlinking = false;
                this.gameObject.layer = 8;
                MoveSpeed = basemovespeed;
                StopCoroutine(blinkingstate());
            }
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Acid")
        {
            if (IsDead == false && isBlinking == false)
            {
                TakeDamage();
            }
        }
        if (other.gameObject.tag == "DropItem" || other.gameObject.tag == "Reward")
        {
            Debug.Log("InRange");
            inrangeToPickup = true;
            itemtopickup = other.gameObject;
        }
        if(other.gameObject.tag == "HealthRefill"){
          if(HP<HPcontainers){
          HP = HPcontainers;
            srctwo.PlayOneShot(pickupsound);
            Destroy(other.gameObject);
          }
        }
          if(other.gameObject.tag == "AmmoRefill"){
            primary.TotalBullets += 100;
            primary.BulletsInClip = Mathf.FloorToInt(primary.bulletsincliplimit);
            if (secondary != null)
        {
            secondary.TotalBullets += 100;
                        secondary.BulletsInClip = Mathf.FloorToInt(secondary.bulletsincliplimit);

        }
        srctwo.PlayOneShot(pickupsound);
         Destroy(other.gameObject);
          }
    }
    void OntriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "DropItem" || other.gameObject.tag == "Reward")
        {
            inrangeToPickup = false;
            itemtopickup = null;
        }
    }
    void Die()
    {
        rb.velocity = new Vector2(0, 0);
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
        Time.timeScale = 0;
    }


    public void TakeDamage()
    {
        StartCoroutine("TakedamageCoroutine");
    }
    public IEnumerator blinkingstate()
    {

        this.gameObject.layer = 13;
        selfanim.SetTrigger("Blink");
        isBlinking = true;
        MoveSpeed *= 1.5f;
        yield return new WaitForSecondsRealtime(2f);
        isBlinking = false;
        this.gameObject.layer = 8;
        MoveSpeed = basemovespeed;
        StopCoroutine(blinkingstate());
    }
    public void instantiaterckt(GameObject rckt, Vector3 pos)
    {
        Instantiate(rckt, pos, Quaternion.identity);
    }
    public IEnumerator TakedamageCoroutine()
    {
        src.loop = false;
        guib.StartCoroutine("SetBloodTint");
        src.PlayOneShot(ugh);
        if(shields == 0)
        {
            HP -= 1;
        }
        else
        {
            shields--;
        }
        if (hascharger && activeusable != null)
        {
            if(currentTimeActiveitem +10 >= activeusable.regentime)
            {
                currentTimeActiveitem = activeusable.regentime;
            }
            else
            { 
                currentTimeActiveitem += 10;
            }
        }
        Instantiate(Blood, this.transform.position, Quaternion.identity);
        Instantiate(Bloodstain, this.transform.position, Quaternion.identity);
        camAnim.SetTrigger("Shake");
        StartCoroutine("blinkingstate");
        StopCoroutine(TakedamageCoroutine());
        yield return null;
    }
    public IEnumerator Reload()
    {
        if (IsDead == false)
        {
            if (Reloading == false)
            {

                if (EquippedWeapon.currentclip < EquippedWeapon.bulletsincliplimit)
                {
                    if (EquippedWeapon.TotalBullets > 0)
                    {
                        Reloading = true;
                        shells.Stop();
                        if (EquippedWeapon.TotalBullets != EquippedWeapon.currentclip && bulletstoreload <= EquippedWeapon.TotalBullets && EquippedWeapon.TotalBullets != 0)
                        {

                            Spawner.GetComponent<AudioSource>().PlayOneShot(EquippedWeapon.reloadSound);


                            yield return new WaitForSeconds(EquippedWeapon.reloadSound.length);
                            EquippedWeapon.TotalBullets = EquippedWeapon.TotalBullets - (bulletstoreload);
                            EquippedWeapon.currentclip = EquippedWeapon.BulletsInClip;

                            yield return new WaitForSeconds(0.25f);
                            Reloading = false;
                        }

                        else if (bulletstoreload > EquippedWeapon.TotalBullets && EquippedWeapon.TotalBullets != 0)
                        {

                            Spawner.GetComponent<AudioSource>().PlayOneShot(EquippedWeapon.reloadSound);

                            yield return new WaitForSeconds(EquippedWeapon.reloadSound.length);
                            EquippedWeapon.currentclip = (EquippedWeapon.TotalBullets);
                            EquippedWeapon.TotalBullets = 0;

                            yield return new WaitForSeconds(0.25f);
                            Reloading = false;
                        }
                        else if (EquippedWeapon.currentclip > 0 && EquippedWeapon.TotalBullets == 0)
                        {
                            StopCoroutine(Reload());
                        }

                        else if (EquippedWeapon.currentclip == 0 && EquippedWeapon.TotalBullets == 0)
                        {
                            if (src.isPlaying != true)
                            {
                                src.PlayOneShot(empty);
                            }
                            yield return new WaitForSeconds(FireRate);
                            Reloading = false;
                        }

                    }
                    else
                    {
                        StopCoroutine(Reload());
                    }
                }
            }
            StopCoroutine(Reload());
        }
    }

    public void drop(Item dropit)
    {
        if (dropit != null)
        {
            print(dropit.Name);
            if (EquippedWeapon == dropit)
            {
                EquippedWeapon = primary;
            }
            GameObject placehold = Instantiate(dropitem, shotpoint.position, Quaternion.identity);
            placehold.GetComponent<DropBehavior>().thisitem = dropit;
            print(placehold.GetComponent<DropBehavior>().thisitem.Name);
            placehold.GetComponent<SpriteRenderer>().sprite = dropit.Shopsprite;
            srctwo.PlayOneShot(DropSound);
        }
    }
    public void ActivateVampirismEffect()
    {
        if (HP < HPcontainers) {
            HP++;
        }
            Spawner.GetComponent<AudioSource>().PlayOneShot(vampiresound);
        
        Instantiate(vampirismparticle, transform.position, Quaternion.identity);
    }
    private IEnumerator giveshield()
    {
        yield return new WaitForSeconds(75f);
        shields++;
        shieldloading = false;
        StopCoroutine(giveshield());
    }
}



