using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EngineerBulletBehavior : MonoBehaviour
{
    public float speed;
    [SerializeField] bool collision;
    public Vector2 Dir;
    Transform col;
    bool isbullet;
    Vector2 bulletdir;
    public void Update()
    {
       
        if (collision)
        {
            if (isbullet)
            {
                Debug.Log("Bullet");
                Dir = bulletdir.normalized;
            }
            else
            {
                Dir = -Dir.normalized;
                Debug.Log("No bullet");
                transform.rotation = Quaternion.Euler(0, 0, Random.Range(-45, 45));
            }
           
        }
        transform.Translate(Dir.normalized * speed * Time.deltaTime);
    }
 
    void OnCollisionEnter2D(Collision2D other)
    {
        col = other.transform;
        if (other.gameObject.tag != "Player" && other.gameObject.tag != "Boss")
        {
            collision = true;
            isbullet = false;
            if (other.gameObject.tag == "Blow")
            {
                bulletdir = (other.transform.up).normalized;
                transform.rotation = Quaternion.Euler(0, 0, other.transform.rotation.z);
                isbullet = true;
            }
        }
        else if (other.gameObject.tag == "Player")
        {
            GameObject.FindGameObjectWithTag("Boss").GetComponent<EngineerBossBehavior>().BulletList.Remove(this.gameObject);
            Destroy(this.gameObject);
        }
    } 
    void OnCollisionExit2D(Collision2D other)
    {
        isbullet = false;
        collision = false;
    }
    void OnCollisionStay2D(Collision2D other)
    {
        col = other.transform;
        if (other.gameObject.tag != "Player" && other.gameObject.tag != "Boss")
        {
            collision = true;
            if(other.gameObject.tag == "Blow")
            {
                bulletdir = (other.transform.up).normalized;
                transform.rotation = Quaternion.Euler(0, 0, other.transform.rotation.z);
                    isbullet = true;
            }
        }
        else if (other.gameObject.tag == "Player")
        {
            Destroy(this.gameObject);
        }
    }
}
