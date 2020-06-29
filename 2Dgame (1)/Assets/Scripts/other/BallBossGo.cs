using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallBossGo : MonoBehaviour
{
    public GameObject BulletPrefab;
    public bool IsGoing;
    public float ShotRate;
    public GameObject RayObj;
    private float timer;
    public GameObject RopePrefab;
   [SerializeField] private GameObject ropeObj;
    private GameObject ZombieOnRope;
    private GameObject Boss;
    private void Start()
    {
        Boss = transform.parent.gameObject;
        RayObj = transform.GetChild(0).gameObject;
        RayObj.SetActive(false);
        IsGoing = false;
    }
    private void Update()
    {
        if (IsGoing)
        {
            if (timer <= 0)
            {
                Shoot();
                timer = ShotRate;
            }
            else
            {
                timer-= Time.deltaTime;
            }
        }
        else
        {
            //I Love Vin Diesel
        }
        if(ropeObj != null)
        {
            if(ZombieOnRope == null)
            {
                Destroy(ropeObj);
                Boss.GetComponent<SoruceBossBehavior>().SubstractAliveZombies();
                Boss.GetComponent<SoruceBossBehavior>().TakeDamage();
            }
        }
    }
    void Shoot()
    {
        Instantiate(BulletPrefab, this.transform.position, this.transform.rotation);
    }
    public void InitializeShotSpeed()
    {
        ShotRate = Random.Range(0.1f, 0.4f);
    }
    public void InitializeRope()
    {
        ropeObj = Instantiate(RopePrefab,this.transform.position,transform.rotation,this.transform);
        ropeObj.transform.up = this.transform.up;
        ropeObj.transform.GetChild(0).gameObject.GetComponent<HingeJoint2D>().connectedBody = Boss.GetComponent<Rigidbody2D>();
        ZombieOnRope = ropeObj.transform.GetChild(5).gameObject;

    }
}
