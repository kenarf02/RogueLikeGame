using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[System.Serializable]
public class Credits : MonoBehaviour
{
    [Header("Dialogue texts")]
    public Dialogue dialogue;
    public string[] endingOne;
    public string[] endingTwo;
    public string[] endingThree;
    public string[] endingFour;
    public string[] endingFive;
    public string[] finalEnding;
    public string[] universalEnding;
    public string currentanimationName;
    private bool WasDialogue;
    [Header("Acting")]
    GameObject player;
    public GameObject actor;
    public GameObject playerAvatar;
    public Sprite[] actorLooks;
    [Header("CameraWork")]
    public Camera camtwo;
    public Transform Cameraplace;
    public GameObject CreditsRoll;

    //add another string in order to get other endings
    void OnEnable()
    {
        camtwo.GetComponent<AudioListener>().enabled = false;
        GameObject.Find("Global Light 2D").SetActive(false);
        player = GameObject.Find("Player");
        GameObject.Find("Gameui").SetActive(false);
        player.GetComponent<GameUIBehavior>().enabled = false;
        player.GetComponent<MoveScript>().enabled = false;
        Camera.main.enabled = false;
        camtwo.enabled = true;
        GameObject Manager = GameObject.Find("GameManager");
        //GameObject.FindGameObjectWithTag("Level").SetActive(false);
        Manager.SetActive(false);
        //move player to the credits scene;
        //get which ending has been played yet and play non-played one
        if (PlayerPrefs.HasKey("endingOne"))
        {
            //TODO: change animator to valid animation for the actor
            if (PlayerPrefs.GetInt("endingOne") == 0)
            {
                PlayerPrefs.SetInt("endingOne", 1);
                ActivateEnding(endingOne);
                currentanimationName = "GrandpaAnim";
                dialogue.enabled = true;
                dialogue.StartDialogue();
                               actor.GetComponent<Image>().sprite = actorLooks[0];
              
                          }
            
        
        else
        {
            if (PlayerPrefs.HasKey("endingTwo"))
            {
                if (PlayerPrefs.GetInt("endingTwo") == 0)
                {
                    PlayerPrefs.SetInt("endingTwo", 1);
                    ActivateEnding(endingTwo);
                    dialogue.enabled = true;
                    actor.GetComponent<Image>().sprite = actorLooks[1];
                    dialogue.StartDialogue();
                }
                else
                {
                    if (PlayerPrefs.HasKey("endingThree"))
                    {
                        if (PlayerPrefs.GetInt("endingThree") == 0)
                        {
                            PlayerPrefs.SetInt("endingThree", 1);
                            ActivateEnding(endingThree);
                            dialogue.enabled = true;
                            actor.GetComponent<Image>().sprite = actorLooks[2];
                                currentanimationName = "AmandaAnim";
                                dialogue.StartDialogue();
                        }
                        else
                        {
                            if (PlayerPrefs.HasKey("endingFour"))
                            {
                                if (PlayerPrefs.GetInt("endingFour") == 0)
                                {
                                    PlayerPrefs.SetInt("endingFour", 1);
                                    ActivateEnding(endingFour);
                                    dialogue.enabled = true;
                                        currentanimationName = "JacobAnim";
                                        actor.GetComponent<Image>().sprite = actorLooks[3];
                                    dialogue.StartDialogue();
                                }
                                else
                                {
                                    if (PlayerPrefs.HasKey("endingFive"))
                                    {
                                        if (PlayerPrefs.GetInt("endingFive") == 0)
                                        {
                                            PlayerPrefs.SetInt("endingFive", 1);
                                            ActivateEnding(endingFive);
                                                currentanimationName = "MarthaAnim";
                                                dialogue.enabled = true;
                                            actor.GetComponent<Image>().sprite = actorLooks[4];
                                            dialogue.StartDialogue();
                                        }
                                        else
                                        {
                                            if (PlayerPrefs.HasKey("endingFinal"))
                                            {
                                                if (PlayerPrefs.GetInt("endingFinal") == 0)
                                                {
                                                    PlayerPrefs.SetInt("endingFinal", 1);
                                                    ActivateEnding(finalEnding);
                                                        currentanimationName = "FrancescoAnim";
                                                        dialogue.enabled = true;
                                                    actor.GetComponent<Image>().sprite = actorLooks[5];
                                                    dialogue.StartDialogue();
                                                    }
                                                    else
                                                    {
                                                      
                                                         
                                                                ActivateEnding(universalEnding);
                                                                dialogue.enabled = true;
                                                                currentanimationName = "FrancescoAnim";
                                                                dialogue.StartDialogue();
                                                            
                                                        
                                                    }
                                                
                                            }
                                            else
                                            {
                                                PlayerPrefs.SetInt("endingFinal", 1);
                                                ActivateEnding(finalEnding);
                                                dialogue.enabled = true;
                                                    currentanimationName = "FrancescoAnim";
                                                    //actor.GetComponent<Image>().sprite = actorLooks[5];
                                                dialogue.StartDialogue();
                                            }
                                        }
                                    }
                                    else
                                    {
                                        PlayerPrefs.SetInt("endingFive", 1);
                                        ActivateEnding(endingFive);
                                            currentanimationName = "MarthaAnim";
                                            dialogue.enabled = true;
                                        actor.GetComponent<Image>().sprite = actorLooks[4];
                                        dialogue.StartDialogue();
                                    }
                                }
                            }
                            else
                            {
                                PlayerPrefs.SetInt("endingFour", 1);
                                ActivateEnding(endingFour);
                                dialogue.enabled = true;
                                actor.GetComponent<Image>().sprite = actorLooks[3];
                                    currentanimationName = "JacobAnim";
                                    dialogue.StartDialogue();
                            }
                        }
                    }
                    else
                    {
                        PlayerPrefs.SetInt("endingThree", 1);
                        ActivateEnding(endingThree);
                            currentanimationName = "AmandaAnim";
                            dialogue.enabled = true;
                        actor.GetComponent<Image>().sprite = actorLooks[2];
                        dialogue.StartDialogue();
                    }
                }
            }
            else
            {
                PlayerPrefs.SetInt("endingTwo", 1);
                    currentanimationName = "SoldierAnim";
                    ActivateEnding(endingTwo);
                dialogue.enabled = true;
                actor.GetComponent<Image>().sprite = actorLooks[1];
                    actor.GetComponent<RectTransform>().sizeDelta = new Vector2(playerAvatar.GetComponent<RectTransform>().sizeDelta.x, playerAvatar.GetComponent<RectTransform>().sizeDelta.y);
                    actor.GetComponent<RectTransform>().anchoredPosition = new Vector2(actor.GetComponent<RectTransform>().anchoredPosition.x, playerAvatar.GetComponent<RectTransform>().anchoredPosition.y);
                    dialogue.StartDialogue();
            }
        }
        }
        else
        {
            PlayerPrefs.SetInt("endingOne", 1);
            currentanimationName = "GrandpaAnim";
            ActivateEnding(endingOne);
            dialogue.enabled = true;
            actor.GetComponent<Image>().sprite = actorLooks[0];
           // actor.GetComponent<RectTransform>().sizeDelta = new Vector2(playerAvatar.GetComponent<RectTransform>().sizeDelta.x, playerAvatar.GetComponent<RectTransform>().sizeDelta.y);
           // actor.GetComponent<RectTransform>().anchoredPosition = new Vector2(actor.GetComponent<RectTransform>().anchoredPosition.x, playerAvatar.GetComponent<RectTransform>().anchoredPosition.y);
            dialogue.StartDialogue();
        }
        WasDialogue = true;
    }
    private void Update()
    {
        if(WasDialogue && dialogue.GetComponent<Dialogue>().dialogui.activeInHierarchy == false)
        {
            CreditsRoll.SetActive(true);
        }
    }
    void ActivateEnding(string[] dialogueText)
    {
        List <string> sentences = new List<string>();
        foreach (string s in dialogueText)
        {
            sentences.Add(s);
        }
        dialogue.sentencesWithShopKeeper = sentences.ToArray();
        }
}
