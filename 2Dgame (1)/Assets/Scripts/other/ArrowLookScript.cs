using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowLookScript : MonoBehaviour
{
    private GameObject AccessCard;
    private GameObject Player;
    public GameObject target;
    public bool PlayerHasNav; //Determines if player owns GPS Navigation system item in his inventory
        void OnEnable()
    {

        Player = GameObject.Find("Player");
        if (GameObject.FindGameObjectWithTag("AccessCard") != null)
        {
            AccessCard = GameObject.FindGameObjectWithTag("AccessCard");
        }
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (AccessCard != null || target != null)
        {
            transform.position = Player.transform.position;
            Vector3 difference = target.transform.position - transform.position;
            float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, rotZ + 90);
        }
    }
}
