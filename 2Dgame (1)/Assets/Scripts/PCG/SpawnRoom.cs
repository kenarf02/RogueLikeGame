using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnRoom : MonoBehaviour
{
    public LayerMask whatIsRoom;
    LevelGeneration levelGen;
    public GameObject levelGenObject;
    public GameObject shopRoom;
    public GameObject bossRoom;
    public GameObject challengeRoom;
    private void Start()
    {
     //   levelGenObject = GameObject.Find("LevelGeneration XL");
        levelGen = levelGenObject.GetComponent<LevelGeneration>();
        transform.SetParent(GameObject.FindGameObjectWithTag("Level").transform);
    }

    void Update()
    {
        Collider2D roomDetection = Physics2D.OverlapCircle(transform.position, 1, whatIsRoom);
        if (roomDetection == null && levelGen.StopGeneration == true)
        {
            Debug.Log("Ok");
            GameObject room;
            //if needed the values of vector magnitudes may change...
             if (levelGen.bossExist == false && (this.transform.position - levelGen.startingPos).magnitude > 20 )
            {
                room = Instantiate(bossRoom, transform.position, Quaternion.identity);
                levelGen.bossExist = true;
                               
                levelGen.BossPos = transform.position;
            }
            else if (levelGen.shopExist == false && (this.transform.position - levelGen.startingPos).magnitude >20 && (this.transform.position - levelGen.BossPos).magnitude > 20)
            {
                room = Instantiate(shopRoom, transform.position, Quaternion.identity);
                levelGen.shopExist = true;
                GameObject.FindGameObjectWithTag("Shop").transform.SetParent(GameObject.FindGameObjectWithTag("Level").transform);
                             
                levelGen.ShopPos = transform.position;
                //fix
            }
            // if needed the values of vectors magnitiude may change...
            
            //challenge room generation system (in progress)
            else if (levelGen.ChallengeExist == false && levelGen.isChallenge == true&& (this.transform.position - levelGen.startingPos).magnitude > 20 && (this.transform.position - levelGen.BossPos).magnitude > 20)
            {
                room = Instantiate(challengeRoom, transform.position, Quaternion.identity);
                 
                levelGen.ChallengeExist = true;
            }
            else
            {
                int rand = Random.Range(0, levelGen.rooms.Length);
               room= Instantiate(levelGen.rooms[rand], transform.position, Quaternion.identity);
                Debug.Log(this.gameObject.name);
                                
                Destroy(this.gameObject);
                
            }
            
         
        }
    
    }
}
