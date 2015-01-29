using UnityEngine;
using System.Collections;

[RequireComponent(typeof(TurretControl))]
public class MadManGunnerAI : MonoBehaviour {

	// Use this for initialization
	void Start () {

	}

    void FixedUpdate () {
		TurretControl turret = GetComponent<TurretControl>();
		if (turret && turret.CanFire()) {
			turret.Fire();
        }
	}
}
