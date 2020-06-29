using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
public AudioClip[] audioclips;
  [SerializeField] private AudioSource src;
   private int currindex = 0;
    private int lastindex = -1;
    private MoveScript ms;
    bool paused;
    private void Start()
    {
        ms = GameObject.Find("Player").GetComponent<MoveScript>();
        src = gameObject.GetComponent<AudioSource>();
        InitializeClip();
    }
    private void Update()
    {
        if (src != null)
        {
            if (Time.timeScale == 0)
            {
                src.Pause();
                paused = true;
            }
            else
            {
               
                if (!src.isPlaying)
                {
                    if (paused == true)
                    {
                        src.Play();
                        paused = false;
                    }
                    else
                    {
                        InitializeClip();
                    }
                }
            }
        }
       
    }
    
    private void InitializeClip()
    {
        currindex = Random.Range(0, audioclips.Length);
        if (currindex != lastindex)
        {
            lastindex = currindex;
            src.clip = audioclips[currindex];
            src.Play();
        }
        else
        {
            InitializeClip();
        }
    }

}
