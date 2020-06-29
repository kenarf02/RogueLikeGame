using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGeneration : MonoBehaviour
{
 
    public Transform[] startingPositions;
    public GameObject[] rooms; //0 --> LR, 1 --> LRD, 2 --> LRU, 3 --> LRDU

    int direction;
    public float moveAmount;

    float timeBtwRoom;
    public float startTimeBtwRoom =0.5f;

    public float minX;
    public float maxX;
    public float minY;

    public bool StopGeneration;
    public LayerMask room;

    int downCounter;

    public Vector3 startingPos;
    public Vector3 BossPos;
    public Vector3 ShopPos;
    public bool shopExist;
    public bool bossExist;
    public bool ChallengeExist;
    public bool isChallenge;
    GameObject player;
    public GameObject startingRoom;

    //fix

    void Start()
    {
       
        minX += GameObject.Find("GameManager").GetComponent<LevelManager>().CurrLevelPos.x;
        maxX += GameObject.Find("GameManager").GetComponent<LevelManager>().CurrLevelPos.x;
        StopGeneration = false;
        shopExist = false;
        ChallengeExist = false;
        isChallenge = false;
        int randomnum = Random.Range(0, 10);
        Debug.LogWarning(randomnum + "Is the random number of challenge room (5 is needed for it to spawn)");
        if(randomnum == 5)
        {
            isChallenge = true;
        }
        else
        {
            isChallenge = false;
        }
        int randStartingPos = Random.Range(0, startingPositions.Length);
        transform.position = startingPositions[randStartingPos].position;
        startingPos = transform.position;
        Instantiate(startingRoom, transform.position, Quaternion.identity);
        GameObject cameramain = GameObject.FindGameObjectWithTag("MainCamera");
        direction = Random.Range(1, 6);
        
    }

    private void Update()
    {
        player = GameObject.Find("Player");
        if (timeBtwRoom <= 0 && StopGeneration == false)
        {
            Move();
            timeBtwRoom = startTimeBtwRoom;
        }
        else
        {
            timeBtwRoom -= Time.deltaTime;
        }
    }

    void Move()
    {
        GameObject roomplholder;
        if(direction == 1 || direction == 2)
        {
            if  (transform.position.x < maxX)
            {
                Vector2 newPos = new Vector2(transform.position.x + moveAmount, transform.position.y);
                transform.position = newPos;

                int rand = Random.Range(0, rooms.Length);
               roomplholder= Instantiate(rooms[rand], transform.position, Quaternion.identity);
               
                direction = Random.Range(1, 6);
                if (direction == 3)
                {
                    direction = 2;
                }
                else if (direction == 4)
                {
                    direction = 5;
                }
            }
            else
            {
                direction = 5;
            }
            
        }
        else if (direction == 3 || direction == 4)
        {
            if (transform.position.x > minX)
            {
                downCounter = 0;
                Vector2 newPos = new Vector2(transform.position.x - moveAmount, transform.position.y);
                transform.position = newPos;

                int rand = Random.Range(0, rooms.Length);
                roomplholder = Instantiate(rooms[rand], transform.position, Quaternion.identity);

                direction = Random.Range(3, 6);
            }
            else
            {
                direction = 5;
            }

        }
        else if (direction == 5)
        {
            downCounter++;
            
            if (transform.position.y > minY)
            {
                Collider2D roomDetection = Physics2D.OverlapCircle(transform.position, 1, room);

                /*if (roomDetection.GetComponent<RoomType>() == null)
                {
                    Debug.Log("NUL");
                    return;
                }*/
               
                if (roomDetection.GetComponent<RoomType>().type != 1 && roomDetection.gameObject.GetComponent<RoomType>().type != 3)
                {
                    if (downCounter >= 2)
                    {
                        roomDetection.GetComponent<RoomType>().RoomDestruction();
                        roomplholder = Instantiate(rooms[3], transform.position, Quaternion.identity);
                    }
                    else
                    {
                        roomDetection.GetComponent<RoomType>().RoomDestruction();

                        int randBottomRoom = Random.Range(1, 4);
                        if (randBottomRoom == 2)
                        {
                            randBottomRoom = 1;
                        }
                        roomplholder = Instantiate(rooms[randBottomRoom], transform.position, Quaternion.identity);
                    }
                }
                

                Vector2 newPos = new Vector2(transform.position.x, transform.position.y - moveAmount);
                transform.position = newPos;

                int rand = Random.Range(2, 4);


                roomplholder = Instantiate(rooms[rand], transform.position, Quaternion.identity);
                

                direction = Random.Range(1, 6);
              
            }
            else
            {
                
                foreach(GameObject go in GameObject.FindGameObjectsWithTag("Template"))
                {
                    go.transform.SetParent(GameObject.FindGameObjectWithTag("Level").transform);
                }
                GameObject.FindGameObjectWithTag("StartRoom").transform.SetParent(GameObject.FindGameObjectWithTag("Level").transform);
               
                StopGeneration = true;
              
            }
            
        }
    }
}
