using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class TutorialStartGame : MonoBehaviour
{
    public GameObject text;
    private bool countdown;
    private bool cancountdown;
    private GameObject textobj;
    private float timer = 3;
    private void Start()
    {
        timer = 3;
        textobj = Instantiate(text);
    }
    private void Update()
    {
        if (cancountdown == true) {
            if (Input.GetButtonDown("Interact"))
            {
                countdown = true;
            }
        }
        if(countdown == true)
        {
            textobj.transform.position = GameObject.Find("Player").transform.position;
            textobj.GetComponent<TextMesh>().text = "Commencing in: " + Mathf.CeilToInt(timer).ToString();
            timer -= Time.deltaTime;
            if(timer <= 0)
            {
                SceneManager.LoadScene(1);
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            cancountdown = true;
            textobj.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            countdown = false;
            cancountdown = false;
            textobj.GetComponent<TextMesh>().text = "";
            textobj.SetActive(false);
        }
    }
}
