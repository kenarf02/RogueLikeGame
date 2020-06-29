using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Dialogue : MonoBehaviour
{
    public Text text;
    public string[] sentencesWithShopKeeper;
    private int index;
    public GameObject otherSpeaker;
    public GameObject dialogui;
    public GameObject gameui;
    private MoveScript mscr;
    private GameObject player;
    public float textSpeed;

    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            StartCoroutine(turnoff());
        }
        if (text.text == sentencesWithShopKeeper[index])
        {
            
            if (Input.anyKeyDown)
            {
                nextSentence();
            }
        }
        player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            mscr = player.GetComponent<MoveScript>();
        }
        if (gameui.activeInHierarchy == false )
        {
            mscr.src.Stop();
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
       
    }
    public void StartDialogue()
    {
        text.text = "";
        foreach(Transform child in dialogui.transform)
        {
            child.gameObject.SetActive(true);
        }
        gameui.SetActive(false);
        StartCoroutine(TalkShopKeeper()); 
    }

    IEnumerator TalkShopKeeper()
    {
        if (mscr != null)
        {
            if (mscr.IsDead != true && mscr.paused != true)
            {
                dialogui.SetActive(true);
                otherSpeaker = Resources.Load("Prefabs/ShopKeeper") as GameObject;
                text.alignment = TextAnchor.UpperLeft;
                mscr.dialogueon = true;
                foreach (char letter in sentencesWithShopKeeper[index].ToCharArray())
                {
                    text.text += letter;
                    yield return new WaitForSecondsRealtime(textSpeed);
                }
            }
        }
        else
        {
            dialogui.SetActive(true);
            otherSpeaker = Resources.Load("Prefabs/ShopKeeper") as GameObject;
            text.alignment = TextAnchor.UpperLeft;
            foreach (char letter in sentencesWithShopKeeper[index].ToCharArray())
            {
                text.text += letter;
                yield return new WaitForSecondsRealtime(textSpeed);
            }
        }
    }
    public void nextSentence()
    {
        if (index < sentencesWithShopKeeper.Length - 1)
        {
            index++;
            text.text = null;
            StartCoroutine(TalkShopKeeper());
        }
        else
        {
            StartCoroutine(turnoff());
    }
    }
    public IEnumerator turnoff()
    {
        text.text = null;
        dialogui.SetActive(false);
        
        if (mscr == null)
        {
            gameui.SetActive(true);
        }
        yield return new WaitForSeconds(1f);
        mscr.dialogueon = false;
        gameui.SetActive(true);
        StopAllCoroutines();
        
    }
}
