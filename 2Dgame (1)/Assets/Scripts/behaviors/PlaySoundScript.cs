using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySoundScript : MonoBehaviour
{
    public AudioClip clip;
  AudioSource src;
    void Start()
    {
        src = GetComponent<AudioSource>();
        src.clip = clip;
        src.Play();
    }
    void OnEnable()
    {
        src = GetComponent<AudioSource>();
        src.clip = clip;
        src.Play();
    }


}
