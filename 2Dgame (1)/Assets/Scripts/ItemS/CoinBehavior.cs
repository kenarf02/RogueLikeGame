using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
public class CoinBehavior : MonoBehaviour
{
    Light2D Thislight;
    private SpriteRenderer sr;
   public int value;
    private AudioSource SoundSource;
    private GameObject player;
    public float magnetSpeed;
     private void Update() {
         if(player.GetComponent<MoveScript>().hasMagnet){
        if((this.transform.position-player.transform.position).magnitude <= 6){
            transform.position = Vector2.Lerp(transform.position,player.transform.position,magnetSpeed*Time.deltaTime);
        }
         }
    }
        void Start()
    {
        player = GameObject.Find("Player");
        SoundSource = GameObject.Find("CoinSoundSource").GetComponent<AudioSource>();
        if (value != 100)
        {
            sr = GetComponent<SpriteRenderer>();
            value = Random.Range(0, 10);

            if (value == 0)
            {
                Destroy(this.gameObject);
            }
            else if (value > 0 && value <= 3)
            {
                sr.color = new Color(0.447f, 0.247f, 0.074f, 1f);
            }
            else if (value > 3 && value <= 6)
            {
                sr.color = new Color(0.874f, 0.874f, 0.847f, 1f);
            }
            else if (value > 6 && value <= 10)
            {
                sr.color = new Color(0.968f, 0.956f, 0.552f, 1f);
            }
            Thislight = GetComponent<Light2D>();
            Thislight.color = sr.color;
        }
        else {
            value = 100;
            Thislight = GetComponent<Light2D>();
            Thislight.color = Color.green;
        }
        
    }

void OnTriggerEnter2D(Collider2D col){
    if(col.gameObject.name == "Player"){
           
            MoveScript ms = col.gameObject.GetComponent<MoveScript>();
            if (!ms.hasBalaclava)
            {
                ms.money += value;
            }
            else
            {
                if (Random.Range(0, 6)==3)
                {
                    ms.money += value * 2;
                }
                else
                {
                    ms.money += value;
                }
            }
            if (SoundSource.isPlaying == false)
            {
                SoundSource.Play();
                Destroy(this.gameObject);
            }
            else
            {
                Destroy(this.gameObject);
            }
           
    }
}
}
