using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Item
    {
    public GameObject Player;
   protected MoveScript movescript;
    public int ID;
        public float SellValue, BuyValue;
        public string Name;
        public Sprite Shopsprite;
    public virtual void OnBuy()
    {
        Player = GameObject.Find("Player");
        movescript = Player.GetComponent<MoveScript>();
        movescript.money -= Mathf.CeilToInt(this.BuyValue);
    }
}
public class bonus: Item
{
    // ammo refil and such
    public bonus(int id, string name, float Sellvalue, float Buyvalue, Sprite sprit)
    {
        ID = id;
        Name = name;
        SellValue = Sellvalue;
        BuyValue = Buyvalue;
        Shopsprite = sprit;
    }
  
}
public class RefillAmmo:bonus
{
    public RefillAmmo(int id, string name, float Sellvalue, float Buyvalue, Sprite sprit) : base(id, name, Sellvalue, Buyvalue, sprit)
    {

    }
    public override void OnBuy()
    {
        base.OnBuy();
        movescript.primary.TotalBullets += 100;
        movescript.primary.BulletsInClip = Mathf.FloorToInt(movescript.primary.bulletsincliplimit);
        if (movescript.secondary != null)
        {
            movescript.secondary.TotalBullets += 100;
            movescript.secondary.BulletsInClip = Mathf.FloorToInt(movescript.primary.bulletsincliplimit);
        }
    }
}
public class RefillHP : bonus
{
    public RefillHP(int id, string name, float Sellvalue, float Buyvalue, Sprite sprit) : base(id, name, Sellvalue, Buyvalue, sprit)
    {

    }
    public override void OnBuy()
    {
  
        base.OnBuy();
        movescript.HP = movescript.HPcontainers;
    }
}
//USABLES

public class UsableItem : Item
    {
    public float regentime;
    public UsableItem(int id, string name, float Sellvalue, float Buyvalue, Sprite sprit,float reg)
    {
        ID = id;
        Name = name;
        SellValue = Sellvalue;
        BuyValue = Buyvalue;
        Shopsprite = sprit;
        regentime = reg;
    }
    public override void OnBuy()
    {
        base.OnBuy();
        movescript.activeusable = this;
        movescript.currentTimeActiveitem = this.regentime;
    }
    public virtual void OnUse()
        {
            Debug.Log("Using!" + Name);
        }
    }

public class airstrike : UsableItem
{
    public GameObject Rocket;
    public GameObject player ;
    MoveScript mscr;
    public airstrike(int id, string name, float Sellvalue, float Buyvalue, Sprite sprit, float reg) : base(id, name, Sellvalue, Buyvalue, sprit,reg)
    {
       
    }

    public override void OnUse()
    {
        player = GameObject.Find("Player");
        Rocket = Resources.Load("Prefabs/Airstrike") as GameObject;
        mscr = player.GetComponent<MoveScript>();
        Vector3 mousepos = new Vector3(Input.mousePosition.x, Input.mousePosition.y,10);
        mousepos = Camera.main.ScreenToWorldPoint(mousepos);
        mscr.instantiaterckt(Rocket, mousepos);

    }
}
public class ammodrop : UsableItem
{
    public GameObject ammopack;
    public GameObject player;
    MoveScript mscr;
    public ammodrop(int id, string name, float Sellvalue, float Buyvalue, Sprite sprit, float reg) : base(id, name, Sellvalue, Buyvalue, sprit, reg)
    {

    }

    public override void OnUse()
    {
        player = GameObject.Find("Player");
        ammopack = Resources.Load("Prefabs/ammo drop") as GameObject;
        mscr = player.GetComponent<MoveScript>();
        Vector3 mousepos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10);
        mousepos = Camera.main.ScreenToWorldPoint(mousepos);
        mscr.instantiaterckt(ammopack, mousepos);

    }
}


public class PortableBloodBank: UsableItem
{

    public float storedheart;
    public PortableBloodBank(int id, string name, float Sellvalue, float Buyvalue, Sprite sprit, float reg):base(id, name, Sellvalue, Buyvalue, sprit, reg)
    {

    }
    public override void OnBuy()
    {
        base.OnBuy();
    }
    public override void OnUse()
    {
        Player = GameObject.Find("Player");
        movescript = Player.GetComponent<MoveScript>();
        if (movescript.HP-1 > 0 && storedheart == 0&&movescript.isBlinking != true)
        {
            //add changing sprite later
            movescript.TakeDamage();
            storedheart++;
            Shopsprite = Resources.Load<Sprite>("Sprites/piggy full");
        }
     
      else if(storedheart == 1)
        {
            if(movescript.HP < movescript.HPcontainers)
            {
                //add changing sprite later
                movescript.HP++;
                storedheart--;
                Shopsprite = Resources.Load<Sprite>("Sprites/piggy empty");
            }
            else if (movescript.HP - 1 <= 0 && storedheart != 1)
            {
                //do nothing
            }
            else if(movescript.HP == movescript.HPcontainers)
            {
                //do nothing
            }
        }
    }
}
[System.Serializable]
public class Weapon : Item
    {
        public float currentclip;
        public float maxSway;
        public float minSway;
        public float RateOfFire;
        public float Damage;
        public float TotalBullets;
        public int BulletsInClip;
        public GameObject bulletPrefab;
        public float Range;
        public AudioClip gunSound;
        public AudioClip reloadSound;
        public Sprite looks;
        public Vector2 ShootingSpot;
        public float bulletsincliplimit;
        public Weapon(int id, string name, float Sellvalue, float Buyvalue, Sprite sprit, float range, float rateoffire, float damage, float totalbullets, int bulletsinclip, GameObject bullet, AudioClip sound, Vector2 shooting, float MinSway, float MaxSway, AudioClip ReloadSound,Sprite shopsprit)
        {
            ID = id;
            Name = name;
            SellValue = Sellvalue;
            BuyValue = Buyvalue;
            looks = sprit;
            RateOfFire = rateoffire;
            Damage = damage;
            TotalBullets = totalbullets;
            BulletsInClip = bulletsinclip;
            bulletPrefab = bullet;
            Range = range;
            gunSound = sound;
            ShootingSpot = shooting;
            currentclip = bulletsinclip;
            bulletsincliplimit = bulletsinclip;
            minSway = MinSway;
            maxSway = MaxSway;
            reloadSound = ReloadSound;
        Shopsprite = shopsprit;
        }
    public override void OnBuy()
    {
        base.OnBuy();
      if(movescript.secondary == null)
        {
            movescript.secondary = this;
        }
    }
}
    public class Passive : Item
    {
    
    public Passive(int id, string name, float Sellvalue, float Buyvalue, Sprite sprit)
        {
            ID = id;
            Name = name;
            SellValue = Sellvalue;
            BuyValue = Buyvalue;
            Shopsprite = sprit;
        }
}
public class AnotherHeart : Passive
{
    public AnotherHeart(int id, string name, float Sellvalue, float Buyvalue, Sprite sprit) : base(id, name, Sellvalue, Buyvalue, sprit)
    {

    }
    public override void OnBuy()
    {
        base.OnBuy();
        movescript.HPcontainers += 1;
    }
}
public class ItemScript : MonoBehaviour
    {

        public List<Weapon> Weapons = new List<Weapon>();
        public List<UsableItem> Usables = new List<UsableItem>();
        public List<Passive> Passives = new List<Passive>();
    public List<bonus> Bonuses = new List<bonus>();
    //items list appending
    Object[] pistols;

        void Start()
        {
        pistols = Resources.LoadAll("Sprites/pistols");
        //weapons
        pistols = Resources.LoadAll("Sprites/pistols");
        Weapon EmptyWeapon = new Weapon(0, "Empty", 0, 0, null, 0, 0, 0, 0, 0, null, null, new Vector2(0, 0), 0f, 0f, null,null);
            Weapons.Add(EmptyWeapon);
            Weapon glock17 = new Weapon(1, "Glock 17", 0.0f, 50f, Resources.Load<Sprite>("Sprites/glock"), 0.35f, 0.25f, 25f, Mathf.Infinity, 17, Resources.Load("Prefabs/glock bullet") as GameObject, Resources.Load("Sounds/glock17") as AudioClip, new Vector2(-0.028f, 0.604f), 2f, -2f, Resources.Load("Sounds/reload") as AudioClip,(Sprite)pistols[1]);
            Weapons.Add(glock17);
            Weapon DEagle = new Weapon(2, "Desert Eagle", 40f, 150f, Resources.Load<Sprite>("Sprites/de"), 0.4f, 1f, 1f, 100f, 9, Resources.Load("Prefabs/glock bullet") as GameObject, Resources.Load("Sounds/de") as AudioClip, new Vector2(-0.028f, 0.604f), 6f, -6f, Resources.Load("Sounds/deagle_reload") as AudioClip, (Sprite)pistols[2]);
            Weapons.Add(DEagle);
        Weapon Kalashnikov = new Weapon(3, "AK 47", 100f, 350f, Resources.Load<Sprite>("Sprites/AK47"), 0.42f, 0.1f, 50f, 250, 30, Resources.Load("Prefabs/glock bullet") as GameObject, Resources.Load("Sounds/ak47") as AudioClip, new Vector2(0.056f, 0.562f), 5f, -5f, Resources.Load("Sounds/AK47_reload") as AudioClip, Resources.Load<Sprite>("Sprites/ak48"));
            Weapons.Add(Kalashnikov);
        Weapon Barrett = new Weapon(4, "Barrett M82", 350f, 800f, Resources.Load<Sprite>("Sprites/barett"), 1f, 1.5f, 200f, 50, 5, Resources.Load("Prefabs/Barrett bullet") as GameObject, Resources.Load("Sounds/barrett") as AudioClip, new Vector2(0.06f, 0.65f), 0f, 0f, Resources.Load("Sounds/reload") as AudioClip, Resources.Load<Sprite>("Sprites/barrett")) ;
            Weapons.Add(Barrett);
            Weapon M416 = new Weapon(5, "M416", 200f, 400f, Resources.Load<Sprite>("Sprites/M416"), 0.55f, 0.086f, 40f, 150, 30, Resources.Load("Prefabs/glock bullet") as GameObject, Resources.Load("Sounds/m416") as AudioClip, new Vector2(0.056f, 0.562f), 3f, -3f, Resources.Load("Sounds/m4_reload") as AudioClip, Resources.Load<Sprite>("Sprites/m417"));
            Weapons.Add(M416);
            Weapon AUG = new Weapon(6, "AUG", 300f, 600f, Resources.Load<Sprite>("Sprites/AUG"), 0.55f, 0.086f, 42f, 150, 40, Resources.Load("Prefabs/glock bullet") as GameObject, Resources.Load("Sounds/aug") as AudioClip, new Vector2(0.056f, 0.562f), 1f, -1f, Resources.Load("Sounds/aug_reload") as AudioClip, Resources.Load<Sprite>("Sprites/aug 1"));
            Weapons.Add(AUG);
            Weapon SW17 = new Weapon(7, "S&W Model 17", 20f, 50f, Resources.Load<Sprite>("Sprites/SW17"), 0.4f, 0.7f, 20f, 100, 6, Resources.Load("Prefabs/glock bullet") as GameObject, Resources.Load("Sounds/sw17_sound") as AudioClip, new Vector2(-0.028f, 0.604f), 1f, -1f, Resources.Load("Sounds/sw17_reload") as AudioClip, (Sprite)pistols[3]);
            Weapons.Add(SW17);
            Weapon P90 = new Weapon(8, "P90", 200f, 500f, Resources.Load<Sprite>("Sprites/P90"), 0.4f, 0.067f, 30f, 180, 50, Resources.Load("Prefabs/glock bullet") as GameObject, Resources.Load("Sounds/p90_sound") as AudioClip, new Vector2(0.056f, 0.432f), 1f, -1f, Resources.Load("Sounds/p90_reload") as AudioClip, Resources.Load<Sprite>("Sprites/p91"));
            Weapons.Add(P90);
        Weapon SawedOff = new Weapon(9, "Sawed-off", 100, 250, Resources.Load<Sprite>("Sprites/sawed-off"), 0.25f, 0.8f,20f, 50, 2, Resources.Load("Prefabs/shotgun bullet") as GameObject, Resources.Load("Sounds/sawed-off shot") as AudioClip, new Vector2(0.056f, 0.432f), 8, -8, Resources.Load("Sounds/sawed-off reload") as AudioClip, Resources.Load<Sprite>("Sprites/sawed-off1"));
        Weapons.Add(SawedOff);
        //items
        Passive nullpasive = new Passive(0, "Ass", Mathf.Infinity, 0, null);
        Passives.Add(nullpasive);
        AnotherHeart ah = new AnotherHeart(1, "AnotherHeart", 75, 125, Resources.Load<Sprite>("Sprites/HeartUp"));
        Passives.Add(ah);
        //bonusese
        bonus zerobonus = new bonus(0, "ASs", 0, 0, null);
        RefillAmmo ammorefil = new RefillAmmo(1, "Ammo Refill",0, 50, Resources.Load<Sprite>("Sprites/Ammorefil"));
        RefillHP refillhp = new RefillHP(2, "HP refill", 0, 75, Resources.Load<Sprite>("Sprites/fullhealth"));
        Bonuses.Add(zerobonus);
        Bonuses.Add(ammorefil);
        Bonuses.Add(refillhp);
        //actives
        UsableItem nullusable = new UsableItem(0, "ASs", 0, 0, null,0);
        airstrike airstrike = new airstrike(1, "Airstrike", 40, 120, Resources.Load<Sprite>("Sprites/airstrike"), 25);
        PortableBloodBank pbb = new PortableBloodBank(2, "Portable blood bank", 25, 100, Resources.Load<Sprite>("Sprites/Piggy empty"), 0.1f);
        ammodrop ad = new ammodrop(3, "Ammo Delivery", 50, 200, Resources.Load<Sprite>("Sprites/ammodrop"), 25f);
        Usables.Add(nullusable);
        Usables.Add(airstrike);
        Usables.Add(pbb);
        Usables.Add(ad);
    }
}
