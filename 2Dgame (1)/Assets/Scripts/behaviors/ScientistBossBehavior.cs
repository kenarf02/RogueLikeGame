using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Pathfinding;
public class ScientistBossBehavior : MonoBehaviour
{
    [SerializeField] bool ongoingSpawn;
    public GameObject[] zombiePrefabs;
    public GameObject ammo;
    public float Speed;
   public float Health;
   [SerializeField] private GameObject player;
   public float FireRate;
   [SerializeField]private float timer;
   //it's basically shotpoint in player case, just 2 of them(as the boss has 2 hands ;))
   public GameObject[] placesInHands;
   public GameObject[] ZombieSpawnPoints;
   [SerializeField] private int ZombieCount;
   [SerializeField] private int ZombiesToSpawn;
   [SerializeField] private Transform ZombieParent;
    [SerializeField] private MoveScript movescript;
    public AudioClip ugh;
    public GameObject Blood;
    public GameObject floatingtext;
    public Transform SpawningPanel = null;
    public GameObject Bloodstain;
    public GameObject dropitem;
    private Item droppedit;
    private AudioSource src;
    [SerializeField] float basehp;
    private bool IsSpawning;
    private ItemScript ItemList;
    public GameObject Door;
    public States startstate = States.ShootState;
    [SerializeField] private bool stageOne,stageTwo,stageThree;
     GameObject BossBar;
     GameObject BossNameText;
    [SerializeField] private Vector3 backvec;
    private Animator anim;
    public string Name;
    private GameObject CutsceneObj;
    public Sprite thisface;
    public AudioClip thisclip;
    public GameObject CashPrefab;
    public enum States
{
    SpawnState,
    ShootState,
    CutSceneState
}
public States currstate;
  
void OnEnable(){
        stageOne = false;
        CutsceneObj = GameObject.Find("GameManager").GetComponent<BossRoomBehavior>().BeforeBossUI;
        ongoingSpawn = false;
        src = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
        BossBar = GameObject.Find("GameManager").GetComponent<BossRoomBehavior>().BossBar;
        BossNameText = GameObject.Find("GameManager").GetComponent<BossRoomBehavior>().BossName;
        BossBar.SetActive(true);
        BossNameText.SetActive(true);
        BossNameText.GetComponent<Text>().text = Name;
        BossBar.GetComponent<Slider>().maxValue = basehp;
        BossBar.GetComponent<Slider>().value = basehp;
        basehp = Health;
    ZombieSpawnPoints = GameObject.FindGameObjectsWithTag("BossSpawner");
    player = GameObject.Find("Player");
        StartCoroutine(custcene());
        currstate = States.CutSceneState;
}
void Update()
    {
        
            if (currstate == States.ShootState)
            {
                followPlayer();
                Shoot();
            }
            else if (currstate == States.SpawnState)
            {
                Spawn();
            }else if(currstate== States.CutSceneState)
        {
            //do nothing
        }
            if (Health <= 0)
            {
                Die();
            }
            else if (Health <= basehp * 0.75f && stageOne != true)
            {
                currstate = States.SpawnState;
                stageOne = true;
            }
            else if (Health <= basehp * 0.5f && stageTwo != true && stageOne == true)
            {
                currstate = States.SpawnState;
                stageTwo = true;
            }
            else if (Health <= basehp * 0.25f && stageThree != true && stageOne == true && stageTwo == true)
            {
                currstate = States.SpawnState;
                stageThree = true;
            }
        
}
void OnCollisionEnter2D(Collision2D other){
    if(other.gameObject.tag == "Blow"|| other.gameObject.tag == "BlowEngineer"||  other.gameObject.tag == "Barrett"){
TakeDamage();
}
}
void followPlayer(){
        GetComponent<AIDestinationSetter>().target = player.transform;
        GetComponent<AIPath>().canMove = true;
    /*Vector3 difference = player.transform.position - transform.position;
		float rotZ = Mathf.Atan2 (difference.y, difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler (0, 0, rotZ + 90);*/
}
void Shoot(){
        IsSpawning = false;
if(timer >= FireRate){
    foreach(GameObject handspot in placesInHands){
Quaternion rot = Quaternion.Euler(transform.rotation.x,transform.rotation.y,transform.rotation.z+45);
                Instantiate(ammo, handspot.transform.position,Quaternion.Euler(transform.rotation.eulerAngles+ new Vector3(0,0,0)));
    }
    timer = 0;
}else{
timer += Time.deltaTime;
}
}
void Spawn(){
        anim.SetTrigger("Walk");
        GetComponent<AIDestinationSetter>().target = null;
        GetComponent<AIPath>().canMove = false;
        //best to move the boss to some kind of control panel
        //boss gets immortal and starts spawning zombies to attack the player, when they are dead boss returns to shootstate
        //play animation of disappearing and then:
        if (SpawningPanel != null)
        {
            backvec = this.transform.position;
            transform.position = SpawningPanel.transform.position;
            transform.rotation = Quaternion.identity;
            anim.SetTrigger("Walk");
        }
        IsSpawning = true;
        tag = "Untagged";
    StartCoroutine("spawning");
    ZombieCount = ZombieParent.childCount;
        if (ZombieCount <= 0 && ongoingSpawn == true)
        {
            anim.SetTrigger("Walk");
            transform.position = backvec;
            currstate = States.ShootState;
            IsSpawning = false;
            ongoingSpawn = false;
            anim.SetTrigger("Walk");
            StopCoroutine("spawning");
            
        }
    }
public IEnumerator spawning(){
    
if(ongoingSpawn == false){
    ongoingSpawn = true;
    ZombiesToSpawn = Random.Range(5,10);
    //TODO: BALANCE NUMBER OF ZOMBIES FOR ONE SPAWN
 while(ZombieCount < ZombiesToSpawn){
     Instantiate(zombiePrefabs[Random.Range(0,zombiePrefabs.Length)],ZombieSpawnPoints[Random.Range(0,ZombieSpawnPoints.Length)].transform.position,Quaternion.identity,ZombieParent);
 //instantiate zombie in one (or maybe each of the spawnpoints) NEED TO SET IT AS ZOMBIEPARENT CHILD
 yield return new WaitForSeconds(0.25f);
 }
}
 yield return null;
}
public void Die(){

        Victory();
        Destroy(gameObject);
}
public void TakeDamage(){
        if (IsSpawning == false)
        {
            movescript = player.GetComponent<MoveScript>();
            GameObject floatingtextplacehold;
            movescript = player.gameObject.GetComponent<MoveScript>();
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
            Instantiate(Blood, this.transform.position, Quaternion.identity);
            Instantiate(Bloodstain, this.transform.position, Quaternion.identity);
            src.PlayOneShot(ugh);
        }
        else
        {
            GameObject floatingtextplacehold;
            floatingtextplacehold = Instantiate(floatingtext, this.transform.position + new Vector3(0, 0.7f, 0), Quaternion.identity);
            floatingtextplacehold.GetComponent<TextMesh>().color = new Color(0,0.6f,1,1);
            floatingtextplacehold.GetComponent<TextMesh>().text = "Immune";
        }
        BossBar.GetComponent<Slider>().value = Health;
    }
    void Victory()
    {
        int k = Random.Range(0, 4);
        Debug.LogWarning(k);
        for (int i = 0; i < k; i++)
        {
            GameObject temp = Instantiate(CashPrefab, this.transform.position + new Vector3(Random.Range(1, 3), Random.Range(1, 3), 0), Quaternion.identity);
            temp.GetComponent<CoinBehavior>().value = 100;
        }
        BossBar.SetActive(false);
        BossNameText.SetActive(false);
        ItemList = GameObject.Find("GameManager").GetComponent<ItemScript>();
        int randomList = Random.Range(0, 3) ;
        if(randomList == 0)
        {
            droppedit = ItemList.Weapons[Random.Range(1, ItemList.Weapons.Count-1)] as Weapon;
        }
        if (randomList == 1)
        {
            droppedit = ItemList.Passives[Random.Range(1, ItemList.Passives.Count - 1)] as Passive;
        }
        if (randomList == 2)
        {
            droppedit = ItemList.Usables[Random.Range(1, ItemList.Usables.Count - 1)] as UsableItem;
        }
     
        GameObject placehold;
        placehold = Instantiate(dropitem, transform.parent.position - new Vector3 (0,3,0), Quaternion.identity, GameObject.FindGameObjectWithTag("Level").transform);
        placehold.GetComponent<DropBehavior>().thisitem = droppedit;
        placehold.GetComponent<SpriteRenderer>().sprite = droppedit.Shopsprite;
        Instantiate(Door, transform.parent.position, Quaternion.identity, GameObject.FindGameObjectWithTag("Level").transform);
       //
        //while boss is destroyed run this void in order to spawn item and do other stuff
    }
    private IEnumerator custcene()
    {
        
        GetComponent<AIDestinationSetter>().target = null;
        GetComponent<AIPath>().canMove = false;
        anim.SetTrigger("Walk");
        player.GetComponent<MoveScript>().isCutscene = true;
        Camera cutscenecam = GameObject.Find("Boss Camera").GetComponent<Camera>();
        var cammain = Camera.main;
        cutscenecam.transform.position = Vector3.Lerp(cutscenecam.transform.position,new Vector3(transform.position.x,transform.position.y,-10), 50);
        cutscenecam.transform.position = new Vector3(cutscenecam.transform.position.x, transform.position.y, -10);
        cammain.enabled = false;
        CutsceneObj.GetComponent<BeforeBossScript>().BossSound = thisclip;
        CutsceneObj.GetComponent<BeforeBossScript>().Bosshead.GetComponent<Image>().sprite = thisface;
        yield return new WaitForSeconds(2f) ;
        CutsceneObj.SetActive(true);
        yield return new WaitForSeconds(4f);
        CutsceneObj.SetActive(false);
        cammain.enabled = true;
        currstate = States.ShootState;
        player.GetComponent<MoveScript>().isCutscene = false;
        anim.SetTrigger("Walk");
        StopAllCoroutines();
    }
}
