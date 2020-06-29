using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialBehavior : MonoBehaviour
{
    MoveScript ms;
    public GameObject[] barricade;
    public int index;
    private void Start()
    {
        index = 0;
        ms = GameObject.Find("Player").GetComponent<MoveScript>();
    }
 private void Update()
    {
        if (ms.activeusable != null)
        {
            if (ms.currentTimeActiveitem <= ms.activeusable.regentime)
            {
                ms.currentTimeActiveitem = ms.activeusable.regentime;
            }
        }
        if(ms.HP <= 3)
        {
            ms.HP = 3;
        }
    }
    public void DestroyBarricade()
    {
        Destroy(barricade[index]);
        if(index == 2)
        {
            ms.activeusable = null;
        }
        index++;
    }
}