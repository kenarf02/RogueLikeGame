using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccessCardBehavior : MonoBehaviour
{
    GameObject doors;
    GameObject player;
    GameUIBehavior gui;
    bool wasdialogueon;
    bool thisdialogueplayed;
    private AudioSource src;
        public AudioClip pickup;
    public AudioClip opendoor;
    MoveScript movescript;
    public GameObject arrow;
    void Start()
    {
     
        player = GameObject.Find("Player");
        movescript = player.GetComponent<MoveScript>();
        doors = GameObject.Find("GameManager");
        arrow = doors.GetComponent<BossRoomBehavior>().arrow;
    }
    void Update()
    {
        if ((this.gameObject.transform.position - player.transform.position).magnitude <= 5&&!thisdialogueplayed&& wasdialogueon&&!movescript.dialogueon)
        {
            wasdialogueon = doors.GetComponent<BossRoomBehavior>().wasdialogueon;
            if ( player.GetComponent<MoveScript>().roomison != true)
            {
                gui = player.GetComponent<GameUIBehavior>();
                gui.movescript.dialogueon = true;
                doors.gameObject.transform.GetChild(1).gameObject.GetComponent<Dialogue>().StartDialogue();
                thisdialogueplayed = true;
                GameObject.Find("ShopKeeper").SetActive(false);
            }
           
        }
        else if ((this.gameObject.transform.position - player.transform.position).magnitude <= 5 && !thisdialogueplayed && !wasdialogueon&&!movescript.dialogueon)
        {
            wasdialogueon = doors.GetComponent<BossRoomBehavior>().wasdialogueon;
            if (player.GetComponent<MoveScript>().roomison != true)
            {
                gui = player.GetComponent<GameUIBehavior>();
                gui.movescript.dialogueon = true;
                doors.gameObject.transform.GetChild(2).gameObject.GetComponent<Dialogue>().StartDialogue();
                thisdialogueplayed = true;
                GameObject.Find("ShopKeeper").SetActive(false);
            }

        }
        else if(((this.gameObject.transform.position - player.transform.position).magnitude <= 15)|| arrow.GetComponent<ArrowLookScript>().PlayerHasNav == true)
        {
            arrow.SetActive(true);
            arrow.GetComponent<ArrowLookScript>().target = this.gameObject;
        }
        else
        { 
            arrow.SetActive(false);
        }

    }
    void OnTriggerEnter2D(Collider2D col)
    {

        if (col.gameObject.tag != "Blow" && col.gameObject.tag != "Boss" && col.gameObject.tag != "Player" && col.gameObject.tag != "Zombie" && col.gameObject.tag != "Acid"&& col.gameObject.tag != "Coin" && col.gameObject.tag != "Bomb")
        {
            respawn(col.gameObject.tag);
        }
        if (col.gameObject.tag == "Player")
        {
            if (player.GetComponent<MoveScript>().roomison != true)
            {
                Debug.Log("Picked up");
                GameObject.Find("GameManager").GetComponent<BossRoomBehavior>().isCard = false;
                GameObject.Find("GameManager").GetComponent<BossRoomBehavior>().PlayerOpenDoor();
                //play sound of doors being open
                AudioSource.PlayClipAtPoint(pickup, this.transform.position);
                if (wasdialogueon)
                {
                    arrow.SetActive(true);
                    arrow.GetComponent<ArrowLookScript>().target = doors.GetComponent<BossRoomBehavior>().DoorsToRoom;
                }
                else
                {
                    arrow.SetActive(false);
                }
                Destroy(this.gameObject);
                
            }
        }
        else
        {

        }
    }
    void respawn(string objtag)
    {
        GameObject.Find("GameManager").GetComponent<BossRoomBehavior>().isCard = false;
        GameObject.Find("GameManager").GetComponent<BossRoomBehavior>().SpawnCard();
        Destroy(this.gameObject);
        Debug.Log("Respawning");
        //Debug.LogError(objtag);
    }
    public void StartCheck() => StartCoroutine("check");
    IEnumerator check()
    {
        if (GameObject.FindGameObjectWithTag("BossRoom") != null && GameObject.FindGameObjectWithTag("StartRoom") != null && GameObject.FindGameObjectWithTag("Shop") != null)
        {
            if ((this.transform.position - GameObject.FindGameObjectWithTag("BossRoom").transform.position).magnitude <= 10 || (this.transform.position - GameObject.FindGameObjectWithTag("StartRoom").transform.position).magnitude <= 10 || (this.transform.position - GameObject.FindGameObjectWithTag("Shop").transform.position).magnitude <= 10)
            {
                respawn("Too near to boss");
            }
            StopAllCoroutines();
        }
        return null;

    }
  
}
