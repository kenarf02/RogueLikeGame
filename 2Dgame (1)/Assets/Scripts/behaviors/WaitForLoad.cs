using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitForLoad : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject poses;
    public bool loaded;
    void Start()
    {
        loaded = false;
        poses = GameObject.Find("Poses");
    }
    public void LateUpdate()
    {
        if (loaded == false)
        {
            if (GameObject.Find("GameManager").GetComponent<LevelManager>().ongoinglevel.transform.GetComponentInChildren<LevelGeneration>().StopGeneration == true && loaded == false)
            {
                StartCoroutine("FindGraph");
                return;
            }
      //      Debug.Log("update");
        }
        else if(loaded == true)
        {
            setenable();
        }
    }

    public IEnumerator FindGraph()
    {
        

            yield return new WaitForSeconds(1.5f);
            AstarPath.active.Scan();
            foreach(GameObject go in GameObject.FindGameObjectsWithTag("Template"))
                {
                    go.transform.SetParent(GameObject.FindGameObjectWithTag("Level").transform);
                }
                foreach(GameObject door in GameObject.FindGameObjectsWithTag("Door")){
                    door.GetComponent<DoorRemovalScript>().Initialize();
                }
 foreach(GameObject room in GameObject.FindGameObjectsWithTag("Room"))
        {
            if (room.GetComponent<RoomCheckForNeigbor>())
            {
                room.GetComponent<RoomCheckForNeigbor>().raycast();
            }
        }


        GameObject.FindGameObjectWithTag("StartRoom").GetComponent<RoomCheckForNeigbor>().raycast();
            foreach(GameObject room in GameObject.FindGameObjectsWithTag("Room"))
        {
            if (room.GetComponent<RoomActive>())
            {
             if(room.GetComponent<RoomActive>().Doors != null){
                room.GetComponent<RoomActive>().Doors.SetActive(false);
            }
            }
        }
            poses.transform.SetParent(null);
        loaded = true;
        StopAllCoroutines();
        CancelInvoke("LateUpdate");
        }
        
    public void setenable()
    {
        enabled = !enabled;
    }
    }

