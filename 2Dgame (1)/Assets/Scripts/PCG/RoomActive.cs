using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomActive : MonoBehaviour
{
    public bool roomcomplete;
    public GameObject Doors = null;
    public GameObject spawners = null;
    public GameObject player;
    public float distance;
    Vector2 playerPosition;
    Vector2 gameobjectPosition;
    public bool spawnEnemies;
    [SerializeField] int zombiecount;
    Dialogue dialogue;
    public GameObject gameManager;
    public GameObject fogOfWar;

    void Start()
    {
        gameManager = GameObject.Find("GameManager");
        spawnEnemies = false;
        player = GameObject.Find("Player");
        dialogue = gameManager.GetComponent<Dialogue>();
        Doors = GetDoors();
        if (Doors != null)
        {
            if (this.gameObject.tag == "BossRoom")
            {
                Doors.SetActive(true);
            }
        }
      
        //don't delete dafuq
        if(this.gameObject.tag == "BossRoom")
        {
            gameManager = GameObject.Find("GameManager");
            gameManager.GetComponent<BossRoomBehavior>().Activate();
        }
    }

    void Update()
    {
       
        if (player != null)
        {
            playerPosition = new Vector2(player.transform.position.x, player.transform.position.y);
             distance = (playerPosition - new Vector2(transform.position.x,transform.position.y)).magnitude;
        }
        if(!roomcomplete){
        if (roomcomplete == false)
        {
            gameobjectPosition = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y);
        }

        if (this.gameObject.tag == "Shop")
        {
            
            if (distance <= 5)
            {
                if (spawnEnemies == false)
                {
                    MoveScript mscr = player.GetComponent<MoveScript>();
                    Debug.Log("shoooop");
                    if (mscr.wasdialogueShop == false)
                    {
                        dialogue.StartDialogue();
                        spawnEnemies = true;
                        mscr.wasdialogueShop = true;
                    }
                    else
                    {
                        spawnEnemies = true;
                    }
                }
            }
        }else if(this.gameObject.tag == "Challenge")
        {
            spawnEnemies = false;
        }else if(this.gameObject.tag == "StartRoom"){
            GameObject.Find("Player").GetComponent<MoveScript>().visitedRooms.Add(this.gameObject);
        }
        else if ((this.gameObject.tag == "Room"||this.gameObject.tag == "BossRoom") && distance <= 9)
        {
            if (spawnEnemies == false)
            {

                roomcomplete = false;
                spawners.SetActive(true);
                spawnEnemies = true;
                GameObject.Find("Player").GetComponent<MoveScript>().roomison = true;
            
                if (Doors != null)
                {
                    Doors.SetActive(true);
                }
            }
        }
        if (spawnEnemies == true)
        {
            if (this.gameObject.tag == "Shop")
            {
                roomcomplete = true;
                GameObject.Find("Player").GetComponent<MoveScript>().roomison = false;
                GameObject.Find("Player").GetComponent<MoveScript>().visitedRooms.Add(this.gameObject);
            }else if(this.gameObject.tag == "Challenge")
            {
                roomcomplete = true;
                GameObject.Find("Player").GetComponent<MoveScript>().roomison = false;
                GameObject.Find("Player").GetComponent<MoveScript>().visitedRooms.Add(this.gameObject);
            }
            else
            {
                InvokeRepeating("CheckZombies", 1f, 0.5f);
            }
        }

            if (spawners.transform.childCount == 0&&spawners != null&& this.gameObject.tag != "Shop"&&distance<=10)
        {
            roomcomplete = true;
            GameObject.Find("Player").GetComponent<MoveScript>().visitedRooms.Add(this.gameObject);
            GameObject.Find("Player").GetComponent<MoveScript>().roomison = false;
            
        }
        if (roomcomplete == true && this.gameObject.tag !="Shop")
        {
            CancelInvoke();
            if (Doors != null)
            {
                Doors.SetActive(false);
                GameObject.Find("Player").GetComponent<MoveScript>().roomison = false;
                

            }
        }else if(roomcomplete != true && spawnEnemies == true)
        {
            if (Doors != null)
            {
                Doors.SetActive(true);
                GameObject.Find("Player").GetComponent<MoveScript>().roomison = true;

            }
        }
        }
        
if(distance<=10){
GameObject.Find("Player").GetComponent<MoveScript>().currentroom = this.gameObject;
}else{
    
}
        /*if (fogOfWar != null)
        {
            if (distance <= 12)
            {
                fogOfWar.SetActive(true);
            }
            else
            {
                fogOfWar.SetActive(false);
            }
        }*/

    }
    GameObject GetDoors()
    {
        GameObject temp = null;
        foreach (Transform child in this.transform)
        {
            if (child.gameObject.name == "Doors")
            {
                temp = child.gameObject;
            }  
        }
        return temp;
    }
    void CheckZombies()
    {


        foreach (Transform child in spawners.transform)
        {
            if (child.childCount == 0)
            {
                Destroy(child.gameObject);
            }
        }
    }
    
}
