using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class DropBehavior : MonoBehaviour
{
[SerializeField] public Item thisitem;
private SpriteRenderer sr;

    private void Start()
    {
        transform.parent = GameObject.FindGameObjectWithTag("Level").transform;
        sr = GetComponent<SpriteRenderer>();
        if(this.gameObject.tag == "Reward")
        {
            generaterandom();
        }
        else
        {
            //do nothing
        }
      
    }
    public void generaterandom()
    {
        MoveScript ms = GameObject.Find("Player").GetComponent<MoveScript>();
        ItemScript Is = GameObject.Find("GameManager").GetComponent<ItemScript>();
        int randomnumber = Random.Range(0, 3);
        switch (randomnumber)
        {
            case 0:
                thisitem = Is.Passives[Random.Range(1, Is.Passives.Count)];
                while (doesContain(ms,thisitem))
                {
                    thisitem = Is.Passives[Random.Range(1, Is.Passives.Count)];
                }
                break;
            case 1:
                thisitem = Is.Usables[Random.Range(1, Is.Usables.Count)];
                while (thisitem == ms.activeusable)
                {
                    thisitem = Is.Usables[Random.Range(1, Is.Usables.Count)];
                }
                break;
            case 2:
                thisitem = Is.Weapons[Random.Range(1, Is.Weapons.Count)];
                while (thisitem == ms.primary)
                {
                    thisitem = Is.Weapons[Random.Range(1, Is.Weapons.Count)];
                }
                while( thisitem == ms.secondary){
                    thisitem = Is.Weapons[Random.Range(1, Is.Weapons.Count)];
                }
                break;
                
        }
        Debug.LogWarning(thisitem.Name);
        sr = GetComponent<SpriteRenderer>();
        sr.sprite = thisitem.Shopsprite;
    }
    bool doesContain(MoveScript move, Item it)
    {
        bool x = false;
        if (move.InventoryList.Count > 0)
        {
            foreach (Item i in move.InventoryList)
            {
                if (i == it)
                {
                    x = true;
                }
                else
                {
                    // do nothing
                }
            }
            return x;
        }
        else
        {
            return false;
        }
    }
    public void InitializeItem()
    {
        if(thisitem != null)
        {
            sr = this.gameObject.GetComponent<SpriteRenderer>();
            sr.sprite = thisitem.Shopsprite;
        }
    }
}
