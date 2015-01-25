using UnityEngine;
using System.Collections;

public class Shrapnel : MonoBehaviour {

    public float speed = 10f;
    public float lifetime = 0.5f;
    private float deathTime;

	// Use this for initialization
	void Awake () {
        rigidbody.AddForce(transform.forward * speed);
        deathTime = Time.time + lifetime;
	}
	
	// Update is called once per frame
	void Update () {
        if (Time.time > deathTime)
            GameObject.Destroy(this.gameObject);
	}
}
