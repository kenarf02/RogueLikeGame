using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeforeBossScript : MonoBehaviour
{
    private AudioSource src;
    public GameObject herohead, Versus, Bosshead;
    public AudioClip BarrySound, VersusSound, BossSound;
    public int PosNeeded;
   [SerializeField] bool one, two,three;
    private void Start()
    {
        src = GetComponent<AudioSource>();
    }
    void Update()
    {
        if (one != true)
        {
            PlayTheSound(herohead, BarrySound);
        }
        else if (two != true)
        {
            PlayTheSound(Versus, VersusSound);
        }
        else if(three != true)
        {
            PlayTheSound(Bosshead, BossSound);
        }
    }
    void PlayTheSound(GameObject transformobj,AudioClip clip)
    {
        if (transformobj != null)
        {
            if (transformobj.GetComponent<RectTransform>().localPosition.y < PosNeeded && !src.isPlaying)
            {

                if (one == false)
                {
                    one = true;
                }
                else if(two == false)
                {
                    two = true;
                }
                else
                {
                    three = true;
                }
                src.PlayOneShot(clip);
                transformobj = null;
                
            }
        }
    }
}
