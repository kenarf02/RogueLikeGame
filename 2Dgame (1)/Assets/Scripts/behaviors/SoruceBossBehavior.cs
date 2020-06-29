using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SoruceBossBehavior : MonoBehaviour
{
    enum ShootingState
    {
        Ray,
        Bullets
    }
    enum State
    {
        spinningstate,
        shootingstate,
        CutSceneState
    }
    public GameObject floatingtext;
    public GameObject[] spots;
 //   public GameObject[] LaserRenderers;
   [SerializeField] private Vector2[] shotspots;
    public float Health;
    public new string name;
    public GameObject Bullet;
    public int MaxHealth;
    MoveScript movescript;
    public GameObject player;
    public GameObject BossBar;
    public GameObject BossNameText;
    private ItemScript ItemList;
    private Item droppedit;
    public GameObject dropitem;
    public GameObject Door;
    [SerializeField] private int ZombiesAlive;
    State currstate;
    ShootingState ShootingSt;
    [SerializeField] private float timer;
    public float shootingtime;//How much will the wheel spin
    [SerializeField] private float rotSpeed;
    public const float RaySpeed=2.0f;//CHANGEABLE
    public const float BulletSpeed=2.0f;//CHANGEABLE
    private AudioSource src;
    public const int DAMAGE=50;
    float BulletTimer;
    private GameObject CutsceneObj;
    public Sprite thisface;
    public AudioClip thisclip;
    private void OnEnable()
    {
        CutsceneObj = GameObject.Find("GameManager").GetComponent<BossRoomBehavior>().BeforeBossUI;
        player = GameObject.Find("Player");
        BossBar = GameObject.Find("GameManager").GetComponent<BossRoomBehavior>().BossBar;
        BossNameText = GameObject.Find("GameManager").GetComponent<BossRoomBehavior>().BossName;
        BossBar.SetActive(true);
        BossNameText.SetActive(true);
        src = GetComponent<AudioSource>();
        BossNameText.GetComponent<Text>().text = name;
        BossBar.GetComponent<Slider>().maxValue = MaxHealth;
        BossBar.GetComponent<Slider>().value = MaxHealth;
        //InitializeSpinState();
        currstate = State.shootingstate;
        ShootingSt = ShootingState.Bullets;
        StartCoroutine(custcene());
        currstate = State.CutSceneState;
       /* foreach(GameObject lr in LaserRenderers)
        {
            lr.GetComponent<LineRenderer>().SetPosition(0, lr.transform.position);
        }*/
    }
    private void Update()
    {
       if(currstate == State.shootingstate)
        {
            Shooting();
            Debug.Log("Spinning");
        }
        else if (currstate == State.spinningstate)
        {
            if (ZombiesAlive > 0)
            {
                SpinningState();
            }
            else
            {
                StartCoroutine(ChangingStateDelay());
            }
        }else if(currstate == State.CutSceneState)
        {
            //do shit
        }
        if (Health <= 0)
        {
            Victory();
        }
    }
    private void OnCollisionEnter2D(Collision2D col)
    {
        /*
       if(col.gameObject.tag == "Blow")
        {
           TakeDamage();
        }
        */
    }
    public void TakeDamage()
    {
        if (currstate == State.spinningstate)
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
            Health -= DAMAGE;
            floatingtextplacehold.GetComponent<TextMesh>().text = movescript.Damage.ToString();
            //TODO: ADD SOUNDS, BLOOD ETC TO TAKING DAMAGE
        } 
        else
        {
            GameObject floatingtextplacehold;
            floatingtextplacehold = Instantiate(floatingtext, this.transform.position + new Vector3(0, 0.7f, 0), Quaternion.identity);
            floatingtextplacehold.GetComponent<TextMesh>().text = "Immune";
        }
        BossBar.GetComponent<Slider>().value = Health;
        return;
    }
    void InitializeSpinState()
    {
        currstate = State.spinningstate;
        foreach (GameObject spot in spots)
        {
            spot.GetComponent<BallBossGo>().RayObj.SetActive(false);
        }
        foreach (GameObject spot in spots)
        {
            spot.GetComponent<BallBossGo>().InitializeRope();
        }
        foreach (GameObject spot in spots)
        {
            ZombiesAlive += 1;
        }
    }
    void SpinningState()
    {
      
    }
    void Shooting()
    {
        if (timer <= shootingtime)
        {
            timer += Time.deltaTime;
            transform.Rotate(0, 0, 100 * Time.deltaTime * rotSpeed);

            if (ShootingSt == ShootingState.Ray)
            {
                for (int i = 0; i < spots.Length; i++)
                {
                    foreach (GameObject spot in spots)
                    {
                        spot.GetComponent<BallBossGo>().RayObj.SetActive(true);
                    }
                }
            }
            else if (ShootingSt == ShootingState.Bullets)
            {
                foreach (GameObject spot in spots)
                {
                    spot.GetComponent<BallBossGo>().InitializeShotSpeed();
                    spot.GetComponent<BallBossGo>().IsGoing = true;
                }
            }

        }
        else
        {
            foreach (GameObject spot in spots)
            {
                spot.GetComponent<BallBossGo>().IsGoing = false;
            }
            InitializeSpinState();
            Debug.Log("finito");
            timer = 0.0f;
            return;
        }
        
    }
    void Victory()
    {
        BossBar.SetActive(false);
        BossNameText.SetActive(false);
        Instantiate(Door, transform.position + new Vector3(0, 1, 0), Quaternion.identity, GameObject.FindGameObjectWithTag("Level").transform);
        Destroy(gameObject);
    }
    private IEnumerator ChangingStateDelay()
    {
        yield return new WaitForSeconds(2.5f);
        currstate = State.shootingstate;
        int rand = Random.Range(0, 2);
        if (rand == 0)
        {
            ShootingSt = ShootingState.Bullets;
        }
        else
        {
            ShootingSt = ShootingState.Ray;
        }
        StopAllCoroutines();
    }
    public void SubstractAliveZombies()
    {
        ZombiesAlive--;
    }
    private IEnumerator custcene()
    {
        player.GetComponent<MoveScript>().isCutscene = true;
        Camera cutscenecam = GameObject.Find("Boss Camera").GetComponent<Camera>();
        var cammain = Camera.main;
        cutscenecam.transform.position = Vector3.Lerp(cutscenecam.transform.position, new Vector3(transform.position.x, transform.position.y, -10), 50);
        cutscenecam.transform.position = new Vector3(cutscenecam.transform.position.x, transform.position.y, -10);
        cammain.enabled = false;
        CutsceneObj.GetComponent<BeforeBossScript>().BossSound = thisclip;
        CutsceneObj.GetComponent<BeforeBossScript>().Bosshead.GetComponent<Image>().sprite = thisface;
        yield return new WaitForSeconds(3f);
        CutsceneObj.SetActive(true);
        yield return new WaitForSeconds(2f);
        CutsceneObj.SetActive(false);
        cammain.enabled = true;
        currstate = State.shootingstate;
        player.GetComponent<MoveScript>().isCutscene = false;
        StopAllCoroutines();
    }
}
