using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyExplosion : MonoBehaviour {
	private ParticleSystem ps;
	void Start () {
		ps = GetComponent<ParticleSystem> ();
        Destroy(this.gameObject, ps.main.duration);
	}


}
