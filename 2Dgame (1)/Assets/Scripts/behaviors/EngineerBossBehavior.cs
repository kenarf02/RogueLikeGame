using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EngineerBossBehavior : MonoBehaviour
{
    public enum state
    {
        shoot,
        followPlayer,
            CutSceneState
    }
    public GameObject Door;
    //drop prefab
    public GameObject dropitem;
    ItemScript ItemList;
    Item droppedit;
    public float Damage;
   [SerializeField] state bossState;
    public float HP;
    [SerializeField] float MaxHp;
    public GameObject BulletPrefab;
    public List<GameObject> BulletList;
    public Transform ShootPoint;
    [SerializeField] private int bulletnumber;
    public int[] BulletStages;
    public float MoveSpeed;
    public GameObject player;
    [SerializeField] bool shooting;
    public AudioClip ugh;
    public GameObject Blood;
    public GameObject floatingtext;
    public GameObject bloodParticle;
    AudioSource src;
    public AudioClip shot;
    GameObject BossBar;
    GameObject BossNameText;
    public string Name;
    private Animator anim;
    private bool isCutsceneOn;
    public GameObject CashPrefab;
    private GameObject CutsceneObj;
    public Sprite thisface;
    public AudioClip thisclip;
    void OnEnable()
    {
        CutsceneObj = GameObject.Find("GameManager").GetComponent<BossRoomBehavior>().BeforeBossUI;
        anim = GetComponent<Animator>();
        bulletnumber = BulletStages[0];
        BossBar = GameObject.Find("GameManager").GetComponent<BossRoomBehavior>().BossBar;
        BossNameText = GameObject.Find("GameManager").GetComponent<BossRoomBehavior>().BossName;
        BossBar.SetActive(true);
        BossNameText.SetActive(true);
        BossNameText.GetComponent<Text>().text = Name;
        BossBar.GetComponent<Slider>().maxValue = MaxHp;
        BossBar.GetComponent<Slider>().value = MaxHp;
        player = GameObject.FindGameObjectWithTag("Player");
        MaxHp = HP;
        src = GetComponent<AudioSource>();
        bossState = state.CutSceneState;
        isCutsceneOn = true;
        StartCoroutine(custcene());
       
    }
    void Update()
    {
        if(HP <= 0)
        {
            Victory();

        }
        if (BulletList.Count > 0)
        {
            foreach (GameObject go in BulletList)
            {
                if (go == null)
                {
                    BulletList.Remove(go);
                }
            }
        }
        Debug.DrawRay(ShootPoint.position,- transform.up);
        if (HP <= MaxHp && HP >= MaxHp * 0.9f)
        {
            bulletnumber = BulletStages[0];
        }
        else if (HP < MaxHp * 0.9f && HP >= MaxHp * 0.75f)
        {
            bulletnumber = BulletStages[1];
        }
        else if (HP < MaxHp * 0.75f && HP >= MaxHp * 0.5f)
        {
            bulletnumber = BulletStages[2];
        }
        else if (HP < MaxHp * 0.5f && HP >= MaxHp * 0.25f)
        {
            bulletnumber = BulletStages[3];
        }
        else if (HP < MaxHp * 0.25f && HP >= MaxHp * 0.5f)
        {
            bulletnumber = BulletStages[4];
        }
        else if(HP<MaxHp *0.25f)
        {
            bulletnumber = BulletStages[5];
        }
        if(BulletList.Count < bulletnumber && !isCutsceneOn)
        {
            bossState = state.shoot;
        }else if(BulletList.Count < bulletnumber && isCutsceneOn)
        {
            bossState = state.CutSceneState;
        }
        else
        {
            bossState = state.followPlayer;
        }
        switch (bossState)
        {
            case state.shoot:
                if (shooting != true)
                {
                    StartCoroutine("ShootState");
                }
                break;
            case state.followPlayer:
               FollowPlayer();
                break;
            case state.CutSceneState:
                //do nothing
                break;
        }
    }
public IEnumerator ShootState() {
        anim.SetTrigger("Walk");
        if (bossState != state.CutSceneState)
        {
            shooting = true;

            if (BulletList.Count < bulletnumber)
            {
                int countoflist = BulletList.Count;

                for (int i = countoflist; i < bulletnumber; i++)
                {
                    yield return new WaitForSeconds(0.5f);
                    GameObject newbullet = Instantiate(BulletPrefab, ShootPoint.transform.position, Quaternion.identity);
                    BulletList.Add(newbullet);
                    newbullet.GetComponent<EngineerBulletBehavior>().Dir = -this.transform.up;
                    newbullet.transform.rotation = Quaternion.Euler(0, 0, transform.rotation.z);
                    src.PlayOneShot(shot);

                }
                shooting = false;
                anim.SetTrigger("Walk");
                bossState = state.followPlayer;
                StopCoroutine("ShootState");
            }
        }
        else
        {
            shooting = false;
            anim.SetTrigger("Walk");
            bossState = state.followPlayer;
            StopCoroutine("ShootState");
        }
    }
    void FollowPlayer()
    {
        shooting = false;
        transform.position = Vector3.MoveTowards(this.transform.position, player.transform.position, MoveSpeed * Time.deltaTime);
        Vector3 difference = player.transform.position - transform.position;
        float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rotZ + 90);
    }
    public void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "EngineerBullet")
        {
            TakeDamage();
            BulletList.Remove(other.gameObject);
            Destroy(other.gameObject);
        }else if(other.gameObject.tag == "Blow")
        {
            GameObject floatingtextplacehold;
            floatingtextplacehold = Instantiate(floatingtext, this.transform.position + new Vector3(0, 0.7f, 0), Quaternion.identity);
            floatingtextplacehold.GetComponent<TextMesh>().text = "Immune";
            floatingtextplacehold.GetComponent<TextMesh>().color = new Color(0, 0.6f, 1, 1);

        }
    }
    void Victory()
    {
        int k = Random.Range(0, 4);
        for (int i = 0; i < k; i++)
        {
            GameObject temp = Instantiate(CashPrefab, this.transform.position + new Vector3(Random.Range(0, 2), Random.Range(0, 2), 0), Quaternion.identity);
            temp.GetComponent<CoinBehavior>().value = 100;
        }
        BossBar.SetActive(false);
        BossNameText.SetActive(false);
        ItemList = GameObject.Find("GameManager").GetComponent<ItemScript>();
        int randomList = Random.Range(0, 3);
        if (randomList == 0)
        {
            droppedit = ItemList.Weapons[Random.Range(1, ItemList.Weapons.Count - 1)] as Weapon;
        }
        if (randomList == 1)
        {
            droppedit = ItemList.Passives[Random.Range(1, ItemList.Passives.Count - 1)] as Passive;
        }
        if (randomList == 2)
        {
            droppedit = ItemList.Usables[Random.Range(1, ItemList.Usables.Count - 1)] as UsableItem;
        }
        foreach (GameObject go in BulletList)
        {
            Destroy(go);
        }
        GameObject placehold;
        placehold = Instantiate(dropitem, transform.parent.position - new Vector3(0,3,0), Quaternion.identity);
        placehold.GetComponent<DropBehavior>().thisitem = droppedit;
        placehold.GetComponent<SpriteRenderer>().sprite = droppedit.Shopsprite;
        placehold.transform.SetParent(GameObject.FindGameObjectWithTag("Level").transform);
        Instantiate(Door, transform.parent.position, Quaternion.identity, GameObject.FindGameObjectWithTag("Level").transform);
        Destroy(gameObject);
        //while boss is destroyed run this void in order to spawn item and do other stuff
    }
    void TakeDamage()
    {

        GameObject floatingtextplacehold;
        floatingtextplacehold = Instantiate(floatingtext, this.transform.position + new Vector3(0, 0.7f, 0), Quaternion.identity);
        Instantiate(Blood, this.transform.position, Quaternion.identity);
        Instantiate(bloodParticle, this.transform.position, Quaternion.identity);
        HP -= Damage;
        floatingtextplacehold.GetComponent<TextMesh>().text = Damage.ToString();
        floatingtextplacehold.GetComponent<TextMesh>().color = Color.red;
        src.PlayOneShot(ugh);
        Debug.Log("TakenDamage"+Damage);
        BossBar.GetComponent<Slider>().value = HP;
    }
    private IEnumerator custcene()
    {
        player.GetComponent<MoveScript>().isCutscene = true;
        anim.SetTrigger("Walk");
        bossState = state.CutSceneState;
        Camera cutscenecam = GameObject.Find("Boss Camera").GetComponent<Camera>();
        var cammain = Camera.main;
        cutscenecam.transform.position = Vector3.Lerp(cutscenecam.transform.position, new Vector3(transform.position.x, transform.position.y, -10), 50);
        cutscenecam.transform.position = new Vector3(cutscenecam.transform.position.x, transform.position.y, -10);
        cammain.enabled = false;
        CutsceneObj.GetComponent<BeforeBossScript>().BossSound = thisclip;
        CutsceneObj.GetComponent<BeforeBossScript>().Bosshead.GetComponent<Image>().sprite = thisface;
        yield return new WaitForSeconds(2f);
        CutsceneObj.SetActive(true);
        yield return new WaitForSeconds(4f);
        CutsceneObj.SetActive(false);
        cammain.enabled = true;
        bossState = state.followPlayer;
        isCutsceneOn = false;
        player.GetComponent<MoveScript>().isCutscene = false;
        anim.SetTrigger("Walk");
        StopAllCoroutines();
    }
}
