using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawnerChallenge : MonoBehaviour
{
    public GameObject[] ZombiePrefab;
    public GameObject button;
    private ChallengeRoomButtonBehavior cr;
    private void OnEnable()
    {
        cr = button.GetComponent<ChallengeRoomButtonBehavior>();
    }
    public void SpawnZombie()
    {
        Instantiate(ZombiePrefab[Random.Range(0,ZombiePrefab.Length)], transform.position, Quaternion.identity, cr.zombieCount.transform);
    }
}
