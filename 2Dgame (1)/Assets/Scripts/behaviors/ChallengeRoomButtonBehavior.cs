using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChallengeRoomButtonBehavior : MonoBehaviour
{
    private GameObject thisroom;
    public GameObject floatingtextprefab;
    private GameObject text;
    private GameObject player;
    public GameObject spawner;
   [SerializeField] private GameObject[] spawners;
    public Sprite red;
    public Sprite green;
   [SerializeField] private bool isactive;
    public float timer;
    public GameObject zombieCount;
    public GameObject[] rewards;
    public float maxtime;
    private void Start()
    {
        text = Instantiate(floatingtextprefab, this.transform.position + new Vector3(0, 2, 0), Quaternion.identity);
        timer = maxtime;
        thisroom = this.transform.parent.gameObject;
        player = GameObject.Find("Player");
        spawner = thisroom.GetComponent<RoomActive>().spawners;
        spawners = new GameObject[spawner.transform.childCount];
        for(int i =0; i< spawner.transform.childCount; i++)
        {
            spawners[i] = spawner.transform.GetChild(i).gameObject;
        }
    }

    private void Update()
    {
        if((player.transform.position-this.transform.position).magnitude <= 5&&isactive == false)
        {
          
            text.GetComponent<TextMesh>().text = "Press E to accept the challenge";
            if (Input.GetButtonDown("Interact")){
                ActivateRoom();
                GetComponent<SpriteRenderer>().sprite = red;
                isactive = true;
            }
        }
        else if(isactive == false && (player.transform.position - this.transform.position).magnitude <= 5)
        {
            text.SetActive(false);
        } else if (isactive == true )
        {
            text.GetComponent<TextMesh>().text = "Time left:" + Mathf.RoundToInt(timer);
            if (isactive && timer >= 0)
        {
            timer -= Time.deltaTime;
        }else if (timer <= 0)
        {
            
            StopAllCoroutines();
                if (zombieCount.transform.childCount == 0)
                {
                    RoomActive rm = thisroom.GetComponent<RoomActive>();
                    isactive = false;
                    rm.roomcomplete = true;
                    GetComponent<SpriteRenderer>().sprite = green;
                    rm.spawners.SetActive(false);
                    rm.player.GetComponent<MoveScript>().roomison = false;
                    rm.Doors.SetActive(false);
                    text.SetActive(false);
                  GameObject drop= Instantiate(rewards[Random.Range(0, rewards.Length)], transform.position, Quaternion.identity);
                    Destroy(gameObject);
                }
                else
                {
                    text.GetComponent<TextMesh>().text = zombieCount.transform.childCount.ToString()+" Zombies left";
                }
        }
        }
        
    }
    private void ActivateRoom()
    {
        RoomActive rm = thisroom.GetComponent<RoomActive>();
        rm.roomcomplete = false;
        rm.spawners.SetActive(true);
        rm.player.GetComponent<MoveScript>().roomison = true;
        rm.Doors.SetActive(true);
        StartCoroutine(Wave());
    }
    private IEnumerator Wave()
    {
        spawners[Random.Range(0, spawners.Length)].GetComponent<ZombieSpawnerChallenge>().SpawnZombie();
        yield return new WaitForSeconds(1.75f);
        StartCoroutine(Wave());
        yield return null;
    }
}
