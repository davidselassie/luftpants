using UnityEngine;
using System.Collections;

public class SelfDestructing : MonoBehaviour {
	public float lifetimeSeconds = 5.0f;

	private float deathTime;

	void Start () {
		this.deathTime = Time.time + this.lifetimeSeconds;
	}

	void FixedUpdate () {
		if (Time.time >= this.deathTime) {
			Destroy(gameObject);
		}
	}
}
