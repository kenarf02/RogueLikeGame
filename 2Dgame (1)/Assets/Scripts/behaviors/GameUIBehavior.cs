 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameUIBehavior : MonoBehaviour
{
    public GameObject inventoryCard, OptionsCard;
    //u should understand those
    public Image primaryweaponInvetoryimg;
    public Image SecondaryweaponInventoryimg;
    //Spacebaritem
    public Image SpacebarItemImg;
    //whole canvas of Game
    public GameObject onscreenui;
    //hearts array
    public Image[] hearts;
    //sprite of full heart
    public Sprite fullHeart;
    public Sprite ShieldHeart;
    //sprite of empty heart
    public Sprite EmptyHeart;
    //menu after death
    public GameObject Diedmenu;
    //panel of red color to indicate damage
    public GameObject BloodTintImg;
    // how much time should the blood thing be shown
    public float bloodTintDurationTime;
    //pause menu
    public GameObject PauseMenu;
    //you should know these
    public Text AmmoText;
    public Text GunNAme;
    //sound between weapons swapping and in menu
    public AudioClip swapsound;
    [SerializeField] AudioSource src;
    //game manager stuff
    Spawner spawn;
    private GameObject Spawner;
    //player script
    public MoveScript movescript;
    //dialogue menu
    public GameObject dialogueui;
    // how much bullets in mag
    public string TextTotalbullets;
    //spacebar item sprite
    public Image spacerbaritem;
    //money text
    //TODO: FIX IT TO LOOK NICE
    public Text moneycount;
    GameObject pathfinderobj;
    public GameObject loadingScreen;
    public GameObject Inventory;
    public GameObject ImagePrefab;
    public GameObject Map;
    void Start()
    {
        pathfinderobj = GameObject.Find("PathfinderObject");
        src = GetComponent<AudioSource>();
        Spawner = GameObject.Find("GameManager");
        movescript = GetComponent<MoveScript>();
    }
    void Update()
    {
        
        if (pathfinderobj != null)
        {
            if (pathfinderobj.GetComponent<WaitForLoad>().loaded == true)
            {
                GetComponent<MoveScript>().enabled = true;
                loadingScreen.SetActive(false);
            }
            else
            {
                GetComponent<MoveScript>().enabled = false;
                loadingScreen.SetActive(true);
            }
        }
        moneycount.text = movescript.money + "$";
        if (movescript.activeusable != null)
        {
            spacerbaritem.gameObject.SetActive(true);
            spacerbaritem.sprite = movescript.activeusable.Shopsprite;
            spacerbaritem.color = new Color(1, 1, 1, movescript.currentTimeActiveitem / movescript.activeusable.regentime);
        }
        else
        {
            spacerbaritem.gameObject.SetActive(false);
        }
        if (movescript.primary != null)
        {
            primaryweaponInvetoryimg.sprite = movescript.primary.Shopsprite;
            primaryweaponInvetoryimg.preserveAspect = true;
        }
        else
        {

        }
        if (movescript.secondary != null)
        {
            SecondaryweaponInventoryimg.gameObject.SetActive(true);
            SecondaryweaponInventoryimg.sprite = movescript.secondary.Shopsprite;
            SecondaryweaponInventoryimg.preserveAspect = true;
        }
        else
        {
            SecondaryweaponInventoryimg.gameObject.SetActive(false);
        }
        if (movescript.activeusable != null)
        {
            SpacebarItemImg.gameObject.SetActive(true);
            SpacebarItemImg.sprite = movescript.activeusable.Shopsprite;
            SpacebarItemImg.preserveAspect = true;
        }
        else
        {
            SpacebarItemImg.gameObject.SetActive(false);
        }
        if (movescript.EquippedWeapon.TotalBullets == Mathf.Infinity)
        {
            TextTotalbullets = "∞";
        }
        else
        {
            TextTotalbullets = (movescript.EquippedWeapon.TotalBullets).ToString();
        }
        AmmoText.text = movescript.EquippedWeapon.currentclip + "/" + TextTotalbullets;
        GunNAme.text = movescript.EquippedWeapon.Name;
        if (Input.GetButtonDown("Cancel"))
        {
            if (loadingScreen == null)
            {
                pause();
            }
            else
            {
                if (movescript.IsDead == false && (loadingScreen.activeInHierarchy == false)&&movescript.dialogueon != true)
            {
                    pause();
                }
            }
            
        }
        if (Input.GetButtonDown("Map"))
        {
            if (loadingScreen == null)
            {
                TurnMap();
            }
            else
            {
                if (movescript.IsDead == false && (loadingScreen.activeInHierarchy == false)&&movescript.dialogueon != true)
            {
                    TurnMap();
                }
            }
            
        }
        if (movescript.paused == true)
        {
            if (dialogueui.activeInHierarchy != true && movescript.IsDead != true)
            {
                ShowPause();
            }
        }
        else
        {
            HidePause();
        }
        if(movescript.MapOn){
            if(dialogueui.activeInHierarchy != true && movescript.IsDead != true && loadingScreen.activeInHierarchy != true){
            ShowMap();
            }
        }else{
            HideMap();
        }
        //Uwaga nie ruszać tego, bo się zjebie
        if (movescript.HP > movescript.HPcontainers)
        {
            movescript.HP = movescript.HPcontainers;
        }
        for (int i = 0; i < hearts.Length; i++)
        {
            if (movescript.hasShield == false || movescript.shields == 0)
            {
                hearts[i].color = Color.white;
                if (i < movescript.HP)
                {
                    hearts[i].sprite = fullHeart;
                }
                else
                {
                    hearts[i].sprite = EmptyHeart;
                }
            }
            else
            {
                if (movescript.shields > i)
                {
                    hearts[i].sprite = ShieldHeart;
                }
                else if (movescript.shields <= i)
                {
                    hearts[i].color = Color.white;
                    if (movescript.HP > i)
                    {
                        hearts[i].sprite = fullHeart;
                    }
                    else
                    {
                        hearts[i].sprite = EmptyHeart;
                    }
                }
                else if (movescript.shields == 0)
                {
                    hearts[i].color = Color.white;
                }else if(movescript.HP == 0)
                {
                    hearts[i].sprite = EmptyHeart;
                }
                    /*else if(i > movescript.HP && movescript.shields < i)
                {
                    hearts[i].color = Color.white;
                    hearts[i].sprite = EmptyHeart;
                }
                */
            }
            if (i < movescript.HPcontainers)
            {
                hearts[i].gameObject.SetActive(true);
            }
            else
            {
                hearts[i].gameObject.SetActive(false);
            }
        }
        if (movescript.HPcontainers >= hearts.Length)
        {
            movescript.HPcontainers = hearts.Length;
        }
        if(Map != null){
        if(dialogueui.activeInHierarchy == true|| Map.activeInHierarchy == true || PauseMenu.activeInHierarchy){
            onscreenui.SetActive(false);
            Time.timeScale = 0.0f;
        }else{
            onscreenui.SetActive(true);
            Time.timeScale = 1.0f;
        }
    }
    }
    public void ShowPause()
    {
        AudioListener.pause = true;
        PauseMenu.SetActive(true);
        showInventory();
        movescript.src.Stop();
        Time.timeScale = 0;
        onscreenui.SetActive(false);
         movescript.src.Stop();
             movescript.srctwo.Stop();
             movescript.src.loop = false;
             movescript.srctwo.loop = false;
        return;
    }
    public void ShowMap()
    {
        if(Map != null){
             movescript.src.Stop();
             movescript.srctwo.Stop();
             movescript.src.loop = false;
             movescript.srctwo.loop = false;
        Map.SetActive(true);
        Time.timeScale = 0;
        onscreenui.SetActive(false);
        movescript.src.Stop();
        return;
        }
    }
    public void HideMap(){
        if(Map != null){
        Time.timeScale = 1;
        Map.SetActive(false);
        onscreenui.SetActive(true);
    }
    }
    public void HidePause()
    {
        AudioListener.pause = false;
        OptionsCard.SetActive(false);
        inventoryCard.SetActive(true);
        
        foreach (Transform child in Inventory.transform)
        {
            Destroy(child.gameObject);
        }
        PauseMenu.SetActive(false);
        if (dialogueui.activeInHierarchy != true)
        {
            onscreenui.SetActive(true);
            Time.timeScale = 1;
            movescript.dialogueon = false;
        }
        else
        {
            onscreenui.SetActive(false);
            Time.timeScale = 0;
            movescript.dialogueon = true;

        }
        GameObject.Find("Global Light 2D").GetComponent<ChangeBrightness>().UpdateBrightness();
    }
    public void ShowDie()
    {
        spawn = Spawner.GetComponent<Spawner>();
        Diedmenu.SetActive(true);
        Time.timeScale = 0;
            movescript.dialogueon = false;
           AudioListener al = Camera.main.GetComponent<AudioListener>();
         Destroy(al);
    }
    public void changeUp()
    {
        Weapon placehold;
        placehold = movescript.primary;
        movescript.primary = movescript.secondary;
        movescript.secondary = placehold;
        placehold = null;
        Time.timeScale = 1;
        AudioSource.PlayClipAtPoint(swapsound, this.transform.position);
        Time.timeScale = 0;
    }
    public void changeDown()
    {
        if(movescript.secondary != null){
        Weapon placehold;
        placehold = movescript.secondary;
        movescript.secondary = movescript.primary;
        movescript.primary = placehold;
        placehold = null;
        Time.timeScale = 1;
        AudioSource.PlayClipAtPoint(swapsound, this.transform.position);
        Time.timeScale = 0;
        }
    }
    public void DropPrimary()
    {
        if(movescript.secondary !=null){
         Time.timeScale =1;
         movescript.drop(movescript.primary);
         Time.timeScale =0;
        movescript.primary = movescript.secondary;
        movescript.secondary = null;
        }else{
//do nothing
        }
    }
    public void DropSecondary()
    {
        if(movescript.primary != null&&movescript.secondary != null){
            Time.timeScale =1;
         movescript.drop(movescript.secondary);
         Time.timeScale =0;
         movescript.secondary = null;
        }
    }
    public void DropActive(){
        if(movescript.activeusable != null){
            Time.timeScale =1;
         movescript.drop(movescript.activeusable);
         Time.timeScale =0;
         movescript.activeusable = null;
        }
    }
    public void pause()
    {
        movescript.paused = !movescript.paused;
    }
    public void TurnMap()
    {
        if(Map!= null){
        movescript.MapOn = !movescript.MapOn;
    }
    }
   public IEnumerator SetBloodTint(){
       BloodTintImg.SetActive(true);
    yield return new WaitForSeconds (bloodTintDurationTime);
    BloodTintImg.SetActive(false);
   }
    void showInventory()
    {
        if(Inventory.transform.childCount < movescript.InventoryList.Count) { 
        if (movescript.InventoryList.Count >= 1)
        {
            Debug.Log(movescript.InventoryList.Count);
                foreach (Item it in movescript.InventoryList)
                {
                    GameObject placehold;
                    placehold = Instantiate(ImagePrefab);
                
                    placehold.transform.SetParent(Inventory.transform);
                    placehold.GetComponent<Image>().sprite = it.Shopsprite;
                    placehold.transform.localScale = new Vector3(1, 1, 1);
                    placehold.GetComponent<Image>().preserveAspect = true;
                }
            }
        }
    }
}
