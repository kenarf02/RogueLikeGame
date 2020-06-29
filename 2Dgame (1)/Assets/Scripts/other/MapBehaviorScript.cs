using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapBehaviorScript : MonoBehaviour
{
    public List <Image> poses;
    GameObject LevelGenerator;
    public List<GameObject> allrooms;
    public List<GameObject> RoomList;
    bool Posesinitialized;
    bool RowsInitialized;
    public Sprite PlayerHead;
    public Sprite shop;
    public Sprite Boss;
     private void OnEnable() {
        
       LevelGenerator = GameObject.Find("LevelGeneration XL");
        allrooms = new List<GameObject>();
        poses = new List<Image>();
        if(!Posesinitialized){
        InitializePoses();
        }
        if(!RowsInitialized){ 
        InitializeRows();
        }
      UpdateGraphics();
    }  
    void InitializeRows(){
        List<GameObject> tempo = new List<GameObject>();
         List<GameObject> tempt = new List<GameObject>();
          List<GameObject> tempthree = new List<GameObject>();
           List<GameObject> tempf = new List<GameObject>();
     foreach(GameObject go in GameObject.FindGameObjectsWithTag("Room")){
            allrooms.Add(go);
        }
        allrooms.Add(GameObject.FindGameObjectWithTag("Shop"));
        allrooms.Add(GameObject.FindGameObjectWithTag("BossRoom"));
        allrooms.Add(GameObject.FindGameObjectWithTag("StartRoom"));
        foreach (GameObject go in allrooms){
            if(go.tag == "Room"){
            switch(go.transform.parent.transform.position.y){
              case 30:
               tempo.Add(go);
              break;
              case 10:
                 tempt.Add(go);
              break;
              case -10:
                 tempthree.Add(go);
              break;
              case -30:
               tempf.Add(go);
              break;
            }
        }else{
            switch(go.transform.position.y){
              case 30:
               tempo.Add(go);
              break;
              case 10:
                 tempt.Add(go);
              break;
              case -10:
                 tempthree.Add(go);
              break;
              case -30:
               tempf.Add(go);
              break;
            }
        }
        }
        RoomList = new List<GameObject>();
        SortArray(tempo.ToArray());
         SortArray(tempt.ToArray());
         SortArray(tempthree.ToArray());
        SortArray(tempf.ToArray());
        
    }
    void InitializePoses(){
foreach(Transform child in this.transform){
            if(child.GetComponent<Image>()==true){
                poses.Add(child.GetComponent<Image>());
                
            }
        }
    }
    void SortArray(GameObject[] array){
        GameObject[] arr = array;
        GameObject temp;
for (int i = 0; i < array.Length; i++)
    {
        for (int j = i+1; j < array.Length; j++)
        {
            if (array[i].transform.TransformPoint(Vector3.zero).x > array[j].transform.TransformPoint(Vector3.zero).x)
            {
                temp = array[i];

                array[i] = array[j];

                array[j] = temp;
            }
        }
    }
        for(int i =0; i<allrooms.Count/4;i++){
            RoomList.Add(arr[i]);
        }
        return;
    }
     
     void UpdateGraphics(){
         MoveScript moveScript = GameObject.Find("Player").GetComponent<MoveScript>();
        for(int i =0; i<16;i++){
            for(int j =0 ; j<moveScript.visitedRooms.Count;j++){
 if(RoomList[i]==moveScript.visitedRooms[j]){  
  poses[i].gameObject.SetActive(true);          
  if(RoomList[i].GetComponent<RoomCheckForNeigbor>().upn){
poses[i-4].gameObject.SetActive(true);         
  }
   if(RoomList[i].GetComponent<RoomCheckForNeigbor>().down){
      poses[i+4].gameObject.SetActive(true);         
  }
   if(RoomList[i].GetComponent<RoomCheckForNeigbor>().leftn){
      poses[i-1].gameObject.SetActive(true);         
  }
   if(RoomList[i].GetComponent<RoomCheckForNeigbor>().rightn){
      poses[i+1].gameObject.SetActive(true);         
  }
            }
            
               
            if(RoomList[i].gameObject.tag == "BossRoom"){
                poses[i].gameObject.transform.GetChild(0).gameObject.SetActive(true);
                poses[i].gameObject.transform.GetChild(0).gameObject.GetComponent<Image>().sprite=Boss;
            }
            else if(RoomList[i].gameObject.tag == "Shop"){
                poses[i].gameObject.transform.GetChild(0).gameObject.SetActive(true);
                poses[i].gameObject.transform.GetChild(0).gameObject.GetComponent<Image>().sprite=shop;
            }
           else{
                poses[i].gameObject.transform.GetChild(0).gameObject.SetActive(false);
                poses[i].gameObject.transform.GetChild(0).gameObject.GetComponent<Image>().sprite=null;
            }
             if(RoomList[i]==moveScript.currentroom){
                poses[i].gameObject.transform.GetChild(0).gameObject.SetActive(true);
                poses[i].gameObject.transform.GetChild(0).gameObject.GetComponent<Image>().sprite=PlayerHead;
             }else{
                 if(poses[i].gameObject.transform.GetChild(0).gameObject.GetComponent<Image>().sprite != null){
                 poses[i].gameObject.transform.GetChild(0).gameObject.SetActive(true);
                poses[i].gameObject.transform.GetChild(0).gameObject.GetComponent<Image>().sprite=poses[i].gameObject.transform.GetChild(0).gameObject.GetComponent<Image>().sprite;
             }else{
                      poses[i].gameObject.transform.GetChild(0).gameObject.SetActive(false);
             }
             }
            if(RoomList[i].GetComponent<RoomActive>().roomcomplete){
                poses[i].color = Color.white;
            }else{
poses[i].color = Color.gray;
            }
            }
        }
    
     }
     public void Resetmap(){
         Posesinitialized = false;
         RowsInitialized = false;
         allrooms = new List<GameObject>();
         RoomList = new List<GameObject>();
         foreach(Image pose in poses){
             pose.gameObject.SetActive(false);
         }
     }
}