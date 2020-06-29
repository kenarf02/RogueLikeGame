using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Spawner : MonoBehaviour {
	public Text text;
	public float Score;
	[SerializeField]public float WaveNumber =0 ;
	public GameObject Zombie;
	private GameObject ZombiePlaceHolder;
	public float TimerDown;
	[SerializeField]private bool WaveOn;
	public float EnemyCount;
	[SerializeField]public GameObject[] SpawnPoints;
	public float NextWave;
	public GameObject BigZombie;
	public Text ScoreText;
	void Start(){
		SpawnPoints = GameObject.FindGameObjectsWithTag("SpawnPoint");
	}

	void Update(){
		ScoreText.text = Score + "$";

		if (NextWave <= 0) {
			WaveOn = false;
		}
		if(WaveOn ==false){
			float nextWaveNum;
			nextWaveNum = WaveNumber + 1;
			text.text = "WAVE " + nextWaveNum+ " APPROACHING IN 10S!";
			Invoke("SpawnWave",10f);
		}

	}
	void SpawnWave(){
		StartCoroutine ("SpawnMobs");
	}

	IEnumerator SpawnMobs(){
		if (WaveOn == false) {
			WaveNumber++;
			WaveOn = true;
			EnemyCount = 10 + (WaveNumber * 2);
			NextWave = EnemyCount;

			text.text = "WAVE " + WaveNumber;
			yield return new WaitForSeconds (2);
			text.text = " ";
			for (int i = 0; i < EnemyCount; i++) {
				float DiceRoll;
				DiceRoll = Random.Range (0, 100);
				if (DiceRoll < 90) {
					Spawning ();
				}
				if (DiceRoll > 90) {
					if (WaveNumber >= 5) {
						SpawningBig ();
						i += 3;
					} else {
						Spawning ();
					}
				}
				if (this.gameObject.transform.childCount == 0&&WaveOn == true) {
					WaveOn = false;
				}
				if (WaveOn == false) {
					yield return null;
				}
			
			}
	

		} 
		}

	void Spawning(){
		ZombiePlaceHolder = Instantiate (Zombie, SpawnPoints [Random.Range (0, SpawnPoints.Length)].transform.position, Quaternion.identity);
		ZombiePlaceHolder.transform.SetParent (this.gameObject.transform);
		ZombiePlaceHolder = null;
	}
	void SpawningBig(){
		ZombiePlaceHolder = Instantiate (BigZombie, SpawnPoints [Random.Range (0, SpawnPoints.Length)].transform.position, Quaternion.identity);
		ZombiePlaceHolder.transform.SetParent (this.gameObject.transform);
		ZombiePlaceHolder = null;
	}

}
