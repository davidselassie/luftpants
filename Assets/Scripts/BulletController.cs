using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BulletController : SafeMonoBehavior {
    public GameObject[] impactExplosions;

	void OnCollisionEnter () {
		Destroy(gameObject);
	}

    override public void SafeOnDestroy () {
		foreach (GameObject explosion in this.impactExplosions) {
			Instantiate(explosion, transform.position, transform.rotation);
        }
    }
}
