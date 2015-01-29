using UnityEngine;
using System.Collections;

public class PilotedTorpedo : APlayerControlledComponent {
    public float thrustSpeed = 10f;
	public float spinSpeed = 1f;

	void FixedUpdate () {
        float horizontalInput = GetHorizontal();
		rigidbody.AddTorque(Vector3.up * horizontalInput * spinSpeed);
		rigidbody.AddForce(transform.forward * thrustSpeed);
    }
}
