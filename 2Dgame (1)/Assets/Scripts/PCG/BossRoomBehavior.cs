using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRoomBehavior : MonoBehaviour
{
    
    public bool isCard;
    public bool inRange;
   public GameObject DoorsToRoom;
    public GameObject CardObj;
    public bool PlayerHasCard = false;
    public bool bossturnedon;
    public GameObject informText;
    GameObject txt;
    public GameObject bossroom;
    public bool doorinrange;
    private GameUIBehavior gui;
    GameObject player;
    public bool wasdialogueon;
    public GameObject arrow;
    public GameObject BossBar;
    public GameObject BossName;
    public bool WereDoorsOpen;
    [SerializeField] GameObject FloatText;
    public GameObject BeforeBossUI;
    void Start()
    {
        player = GameObject.Find("Player");
        FloatText = Instantiate(informText, player.transform.position, Quaternion.identity);
        FloatText.GetComponent<TextMesh>().text = "Press E to Open the Door";
        transform.SetParent(GameObject.FindGameObjectWithTag("Level").transform);
        FloatText.SetActive(false);
    }
    public void LateUpdate()
    {
        
        if (DoorsToRoom != null)
        {

            if ((DoorsToRoom.transform.position - player.transform.position).magnitude < 13f && PlayerHasCard == false&&wasdialogueon ==false&&player.GetComponent<MoveScript>().roomison == false)
            {
                foreach (Transform child in DoorsToRoom.transform)
                {
                    if ((child.transform.position - player.transform.position).magnitude < 3f)
                    {
                        gui = player.GetComponent<GameUIBehavior>();
                        gui.movescript.dialogueon = true;
                        this.gameObject.transform.GetChild(0).gameObject.GetComponent<Dialogue>().StartDialogue();
                        wasdialogueon = true;
                        //CAN AS WELL CHANGE THE GRAPHICS TO THE DOOR:
                        GameObject.Find("ShopKeeper").SetActive(false);
                    }
                }
                
            }
            if ((DoorsToRoom.transform.position - player.transform.position).magnitude < 13f && PlayerHasCard == true  && player.GetComponent<MoveScript>().roomison == false&& WereDoorsOpen == false)
            {
                
                    if ((DoorsToRoom.transform.position - player.transform.position).magnitude < 13f)
                    {
                        FloatText.SetActive(true);
                        FloatText.transform.position = player.transform.position;
                        FloatText.GetComponent<TextMesh>().text = "Press E to Open the doors";
                        if (Input.GetButtonDown("Interact"))
                        {
                            DoorsToRoom.SetActive(false);
                            PlayerHasCard = true;
                            isCard = true;
                            WereDoorsOpen = true;
                            FloatText.SetActive(false);
                        }
                    }
                    else
                    {
                        FloatText.SetActive(false);
                    }
                }
            
            else
            {
                FloatText.SetActive(false);
            }
        }
    }
    public void Activate()
    {
        bossroom = GameObject.FindGameObjectWithTag("BossRoom");
        txt = Instantiate(informText, GameObject.Find("Player").transform.position, Quaternion.identity);
        Debug.Log("BossRoom Active");
        isCard = false;
        GameObject BossRoom = GameObject.FindGameObjectWithTag("BossRoom");
        SpawnCard();
        GameObject Doors()
        {
            GameObject placehold = null;
            foreach (Transform Child in BossRoom.transform)
            {
                if (Child.transform.gameObject.name == "Doors")
                {
                    placehold = Child.gameObject;
                }
            }
            return (placehold);
        }
        DoorsToRoom = Doors();

    }

  public void PlayerOpenDoor()
    {
        Debug.Log(DoorsToRoom);
        //DoorsToRoom.SetActive(false);
        isCard = true;
        PlayerHasCard = true;
    }
    public void SpawnCard()
    {
        GameObject card;
        if (isCard == false)
        {
            if (GameObject.FindGameObjectsWithTag("AccessCard").Length >= 1)
            {
                foreach(GameObject go in GameObject.FindGameObjectsWithTag("AccessCard"))
                {
                    Destroy(go);
                }
        }
           card= Instantiate(CardObj, new Vector2(Random.Range(-40, 40), Random.Range(-40, 40)), Quaternion.identity);
            isCard = true;
            card.GetComponent<AccessCardBehavior>().StartCoroutine("check");
            card = null;
        }
    }
}
