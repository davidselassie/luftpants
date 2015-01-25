using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BulletController : MonoBehaviour {
    public GameObject[] impactExplosions;

	void OnCollisionEnter () {
		Destroy(gameObject);
	}

    void OnDestroy () {
		foreach (GameObject explosion in this.impactExplosions) {
			Instantiate(explosion, transform.position, transform.rotation);
        }
    }
}
