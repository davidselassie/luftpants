using UnityEngine;
using System.Collections;

public class HealthComponent : MonoBehaviour {
    public float health = 100.0f;

	// Use this for initialization
	void Start () {
	
	}
	
	void FixedUpdate () {
        if (health <= 0.0f) {
            Destroy(this);
        }
	}
}
