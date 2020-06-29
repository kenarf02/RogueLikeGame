using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ItemAssigner : MonoBehaviour
{
    public AudioClip kaching;
    public ItemScript itemList;
    private GameObject gamemanager;
    [SerializeField] private Weapon weaponAvail;
    [SerializeField] private Passive passiveAvail;
    [SerializeField] private UsableItem usableAvail;
    [SerializeField] private bonus bonusAvail;
    [SerializeField] private string bonusname, usablename, passivename, weaponame;
    public GameObject[] itemontable;
    private GameObject player;
    Vector2 distancefromplayer;
    private Vector2 distancewep, distancepass, distanceuse, distancebonus;
    public float[] vectorlenghts;
    private MoveScript mscr;
    public bool abletoshop;
   public GameObject ShopCanvas;
    public Text itemtext;
    public Text costText;
    [SerializeField] private bool wep, pass, use, bonus;
    public Text affordtext;
    public Text DPSText;

    void Start()
    {
        player = GameObject.Find("Player");
        mscr = player.GetComponent<MoveScript>();
        gamemanager = GameObject.Find("GameManager");
        itemList = gamemanager.GetComponent<ItemScript>();
        //making items in shop random by taking them from the list
      
   
            weaponAvail = itemList.Weapons[Random.Range(1, itemList.Weapons.Count)];
        passiveAvail = itemList.Passives[Random.Range(1, itemList.Passives.Count)];
        usableAvail = itemList.Usables[Random.Range(1, itemList.Usables.Count)];
        bonusAvail = itemList.Bonuses[Random.Range(1, itemList.Bonuses.Count)];
        //
        //just for indication ;)
        bonusname = bonusAvail.Name;
        usablename = usableAvail.Name;
        passivename = passiveAvail.Name;
        weaponame = weaponAvail.Name;
        if(weaponAvail == mscr.primary || weaponAvail == mscr.secondary)
        {
            while (weaponAvail == mscr.primary || weaponAvail == mscr.secondary)
            {
                weaponAvail = itemList.Weapons[Random.Range(1, itemList.Weapons.Count)];
            }
        }
        if (usableAvail == mscr.activeusable)
        {
            while (usableAvail == mscr.activeusable)
            {
                usableAvail = itemList.Usables[Random.Range(1, itemList.Usables.Count)];
            }
        }
        for (int x = 0; x < itemontable.Length; x++)
        {
            //display sprites
            GameObject placeholder;
            placeholder = itemontable[x];
            if (wep == false)
            {
                placeholder.GetComponent<SpriteRenderer>().sprite = weaponAvail.Shopsprite;
                wep = true;
            } else if (pass == false)
            {
                placeholder.GetComponent<SpriteRenderer>().sprite = passiveAvail.Shopsprite;
                pass = true;
            }else if(use == false)
            {
                placeholder.GetComponent<SpriteRenderer>().sprite = usableAvail.Shopsprite;
                use = true;
            }else if(bonus == false)
            {
                placeholder.GetComponent<SpriteRenderer>().sprite = bonusAvail.Shopsprite;
                bonus = true;
            }
        }
    }
    void Update()
    {
        if (weaponAvail !=null && itemontable[0] != null)
        {
            distancewep = itemontable[0].transform.position - player.transform.position;
        }
        else
        {
            distancewep = new Vector2(10000,10000);
        }
       
        if (passiveAvail != null && itemontable[1] != null)
        {
            distancepass = itemontable[1].transform.position - player.transform.position;
        }
        else
        {
            distancepass = new Vector2(10000, 10000);
        }


        if (usableAvail != null && itemontable[2] != null)
        {
            distanceuse = itemontable[2].transform.position - player.transform.position;
        }
        else
        {
            distanceuse = new Vector2(10000, 10000);
        }

        if (bonusAvail != null && itemontable[3] != null)
        {
            distancebonus = itemontable[3].transform.position - player.transform.position;
        }
        else
        {
            distancebonus = new Vector2(10000, 10000);
        }


        vectorlenghts = new float[4] { distancewep.magnitude, distancepass.magnitude, distanceuse.magnitude, distancebonus.magnitude };
        
        //check if player is in range to shop
        distancefromplayer = this.transform.position - player.transform.position;
        if (distancefromplayer.magnitude <= 5)
        {
            abletoshop = true;
        }
        else
        {
            abletoshop = false;
        }
        if (abletoshop == true)
        {
            ShopCanvas.SetActive(true);
            Animator anim;
            if (vectorlenghts[0] < vectorlenghts[1] && vectorlenghts[0] < vectorlenghts[2] && vectorlenghts[0] < vectorlenghts[3])
            {
                if (itemontable[0] != null)
                {
                    anim = itemontable[0].GetComponent<Animator>();
                    anim.SetBool("ItemActive", true);
                    mscr.iteminrangeshop = weaponAvail;
                    if (itemontable[1] != null)
                    {
                        itemontable[1].GetComponent<Animator>().SetBool("ItemActive", false);
                    }
                    if (itemontable[2] != null)
                    {
                        itemontable[2].GetComponent<Animator>().SetBool("ItemActive", false);
                    }
                    if (itemontable[3] != null)
                    {
                        itemontable[3].GetComponent<Animator>().SetBool("ItemActive", false);
                    }

                }
            }
            else if (vectorlenghts[1] < vectorlenghts[0] && vectorlenghts[1] < vectorlenghts[2] && vectorlenghts[1] < vectorlenghts[3])
            {
                if (itemontable[1] != null)
                {
                    anim = itemontable[1].GetComponent<Animator>();
                    anim.SetBool("ItemActive", true);
                    mscr.iteminrangeshop = passiveAvail;
                    if (itemontable[0] != null)
                    {
                        itemontable[0].GetComponent<Animator>().SetBool("ItemActive", false);
                    }
                    if (itemontable[2] != null)
                    {
                        itemontable[2].GetComponent<Animator>().SetBool("ItemActive", false);
                    }
                    if (itemontable[3] != null)
                    {
                        itemontable[3].GetComponent<Animator>().SetBool("ItemActive", false);
                    }
                }
            }
            else if (vectorlenghts[2] < vectorlenghts[1] && vectorlenghts[2] < vectorlenghts[0] && vectorlenghts[2] < vectorlenghts[3])
            {
                if (itemontable[2] != null)
                {
                    anim = itemontable[2].GetComponent<Animator>();
                    anim.SetBool("ItemActive", true);
                    mscr.iteminrangeshop = usableAvail;
                    if (itemontable[1] != null)
                    {
                        itemontable[1].GetComponent<Animator>().SetBool("ItemActive", false);
                    }
                    if (itemontable[0] != null)
                    {
                        itemontable[0].GetComponent<Animator>().SetBool("ItemActive", false);
                    }
                    if (itemontable[3] != null)
                    {
                        itemontable[3].GetComponent<Animator>().SetBool("ItemActive", false);
                    }
                }
            }
            else if (vectorlenghts[3] < vectorlenghts[1] && vectorlenghts[3] < vectorlenghts[2] && vectorlenghts[3] < vectorlenghts[0])
            {
                if (itemontable[3] != null)
                {
                    anim = itemontable[3].GetComponent<Animator>();
                    anim.SetBool("ItemActive", true);
                    mscr.iteminrangeshop = bonusAvail;
                    if (itemontable[1] != null)
                    {
                        itemontable[1].GetComponent<Animator>().SetBool("ItemActive", false);
                    }
                    if (itemontable[2] != null)
                    {
                        itemontable[2].GetComponent<Animator>().SetBool("ItemActive", false);
                    }
                    if (itemontable[0] != null)
                    {
                        itemontable[0].GetComponent<Animator>().SetBool("ItemActive", false);
                    }
                }
            }
            if (mscr.iteminrangeshop != null)
            {
                itemtext.text = mscr.iteminrangeshop.Name;
                if(!mscr.hasDiscount){
                costText.text = mscr.iteminrangeshop.BuyValue.ToString() + "$";
                }else{
                    costText.text = Mathf.CeilToInt(mscr.iteminrangeshop.BuyValue /2).ToString()  + "$";
                }
                if(mscr.iteminrangeshop.Type == "Weapon")
                {
                    Weapon placehold = mscr.iteminrangeshop as Weapon;
                    DPSText.text = (Mathf.FloorToInt (placehold.Damage * (1/placehold.RateOfFire))).ToString() + "DPS";
                }
                else
                {
                    DPSText.text = "";
                }
            }
            else
            {
                itemtext.text = "";
            }
            if (mscr.iteminrangeshop != null)
            {
                
                if (mscr.money >= mscr.iteminrangeshop.BuyValue || (mscr.hasDiscount&&mscr.money >= mscr.iteminrangeshop.BuyValue/2))
                {
                    affordtext.text = "Press 'E' to buy: " + mscr.iteminrangeshop.Name;
                    if (Input.GetKeyDown(KeyCode.E) && player.GetComponent<MoveScript>().dialogueon == false)
                    {
                        mscr.iteminrangeshop.OnBuy();
                        if (mscr.iteminrangeshop.Type != "Weapon" && mscr.secondary == null)
                        {
                            mscr.secondary = null;
                        }
                        player.GetComponent<MoveScript>().srctwo.PlayOneShot(kaching);
                        foreach (GameObject gobject in itemontable)
                        {
                            if (gobject != null)
                            {
                                if (gobject.GetComponent<SpriteRenderer>().sprite == mscr.iteminrangeshop.Shopsprite)
                                {
                                    if (weaponAvail != null)
                                    {
                                        if (weaponAvail.Shopsprite == gobject.GetComponent<SpriteRenderer>().sprite)
                                        {
                                            weaponAvail = null;
                                        }
                                    }

                                    else if (passiveAvail != null)
                                    {
                                        if (passiveAvail.Shopsprite == gobject.GetComponent<SpriteRenderer>().sprite)
                                        {
                                            passiveAvail = null;
                                        }
                                    }
                                    else if (usableAvail != null)
                                    {
                                        if (usableAvail.Shopsprite == gobject.GetComponent<SpriteRenderer>().sprite)
                                        {
                                            usableAvail = null;
                                        }
                                    }
                                    else if (bonusAvail != null)
                                    {
                                        if (bonusAvail.Shopsprite == gobject.GetComponent<SpriteRenderer>().sprite)
                                        {
                                            bonusAvail = null;
                                        }
                                    }
                                    mscr.iteminrangeshop = null;
                                    Destroy(gobject);
                                    break;

                                }
                            }
                        }

                    }
                }
                else if (mscr.money < mscr.iteminrangeshop.BuyValue)
                {
                    if (mscr.iteminrangeshop != null)
                    {
                        affordtext.text = "You can't afford: " + mscr.iteminrangeshop.Name;
                    }

                }
            }
            else
            {
                affordtext.text = "";
            }
        }
        else
        {
            ShopCanvas.SetActive(false);
            if (itemontable[0] != null)
            {
                itemontable[0].GetComponent<Animator>().SetBool("ItemActive", false);
            }
            if (itemontable[1] != null)
            {
                itemontable[1].GetComponent<Animator>().SetBool("ItemActive", false);
            }
            if (itemontable[2] != null)
            {
                itemontable[2].GetComponent<Animator>().SetBool("ItemActive", false);
            }
            if (itemontable[3] != null)
            {
                itemontable[3].GetComponent<Animator>().SetBool("ItemActive", false);
            }
        }
      /*  if(distancebonus.magnitude+distancepass.magnitude+distanceuse.magnitude+distancewep.magnitude >= 8000)
        {
            ShopCanvas.SetActive(false);
        }*/
    }
}
