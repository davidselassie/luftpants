using UnityEngine;
using System.Collections;

public class PilotedTorpedo : APlayerControlledComponent {
    public float speed = 10f;
    public float turnSpeed = 1f;

	void FixedUpdate () {
        float horizontalInput = GetHorizontal();
        rigidbody.AddTorque(Vector3.up * horizontalInput * turnSpeed);
        rigidbody.AddForce(transform.forward * speed);

		if (GetButton("B")) {
			Destroy(gameObject);
		}
    }
}
