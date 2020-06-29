using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LevelManager : MonoBehaviour
{
    public int CurrLevel = 0;
    public GameObject[] Levels;
    public Vector3 CurrLevelPos;
    public GameObject PathfindingObj;
    public GameObject ongoinglevel;
    public GameObject Credits;
    public MapBehaviorScript map;

    [ContextMenu("NextLevel")]
    public void NewLevel()
    {
        transform.parent = null;
        try { 
            GameObject[] coin = GameObject.FindGameObjectsWithTag("Coin");
            foreach (GameObject c in coin)
            {
                Destroy(c.gameObject);
            }
            Destroy(GameObject.FindGameObjectWithTag("BossRoom"));
            ongoinglevel.SetActive(false);
            CurrLevel++;
            CurrLevelPos = CurrLevelPos + new Vector3(0, 0, 0);
            PathfindingObj = GameObject.Find("PathfinderObject");
            ongoinglevel = Instantiate(Levels[CurrLevel], CurrLevelPos, Quaternion.identity);
            PathfindingObj.GetComponent<WaitForLoad>().loaded = false;
            PathfindingObj.GetComponent<WaitForLoad>().enabled = true;
            GameObject.Find("Player").GetComponent<MoveScript>().itemtopickup = null;
            map.Resetmap();
            GameObject.Find("Player").GetComponent<MoveScript>().visitedRooms = new List<GameObject>();
            GameObject.Find("Player").GetComponent<MoveScript>().currentroom = null;
            GetComponent<BossRoomBehavior>().PlayerHasCard = false;
            GetComponent<BossRoomBehavior>().WereDoorsOpen = false;
        }
        catch (System.IndexOutOfRangeException ex)
        {
                Credits.SetActive(true);
                Debug.LogWarning("Game finished");
            Debug.Log(ex);
            
        }
        
    }
}
