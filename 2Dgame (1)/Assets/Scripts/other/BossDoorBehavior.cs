using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDoorBehavior : MonoBehaviour
{
    public GameObject floatingtext;
    private GameObject text;
    private float timer;
    private bool standing;
    private void Start()
    {
        timer = 3f;
        text = Instantiate(floatingtext, transform.position + new Vector3(0, 1.5f, 0), Quaternion.identity);

    }
    private void Update()
    {
        if(standing == true)
        {
            text.GetComponent<TextMesh>().text = "Commencing to level " + Mathf.CeilToInt( GameObject.Find("GameManager").GetComponent<LevelManager>().CurrLevel+2).ToString()+ " in: " + Mathf.CeilToInt(timer).ToString();
            timer -= Time.deltaTime;
            if(timer <= 0)
            {
                GameObject.Find("GameManager").GetComponent<LevelManager>().NewLevel();
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            if(this.gameObject.tag == "FinishExit")
            {
                GameObject.Find("GameManager").GetComponent<LevelManager>().NewLevel();
            }
            else
            {
                standing = true;
                text.SetActive(true);
            }
           
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && gameObject.tag != "FinishExit")
        {
            standing = false;
            timer = 3f;
            text.SetActive(false);
        }
    }
}
