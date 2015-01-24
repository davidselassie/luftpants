using UnityEngine;
using System.Collections;

public class PilotedTorpedo : APlayerControlledComponent {

    public float speed = 10f;
    public float turnSpeed = 1f;
    private float horizontalInput;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        horizontalInput = GetHorizontal();


        rigidbody.AddTorque(Vector3.up * horizontalInput * turnSpeed);
        rigidbody.AddForce(transform.forward * speed);
	}
}
