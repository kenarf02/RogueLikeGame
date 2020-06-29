using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum types
{
    Passive,
    Usable,
    Weapon
}
public class ItemOfChoice : MonoBehaviour
{

    public types itemtype;
    public int ID;
    private ItemScript iscript;
    private void Start()
    {
        iscript = GameObject.Find("GameManager").GetComponent<ItemScript>();
        iscript.Initialize();

        switch (itemtype)
        {
            case types.Passive:
                GetComponent<DropBehavior>().thisitem = iscript.Passives[ID];
                break;
            case types.Usable:
                GetComponent<DropBehavior>().thisitem = iscript.Usables[ID];
                break;
            case types.Weapon:
                GetComponent<DropBehavior>().thisitem = iscript.Weapons[ID];
                break;
        }
       this.gameObject.GetComponent<DropBehavior>().InitializeItem();
    }
}
