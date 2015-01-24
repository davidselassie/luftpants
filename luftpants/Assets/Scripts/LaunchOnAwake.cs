using UnityEngine;
using System.Collections;

public class LaunchOnAwake : MonoBehaviour {

    public float speed = 10f;

	// Use this for initialization
	void Awake () {
        rigidbody.AddForce(transform.forward * speed);
	}
}
