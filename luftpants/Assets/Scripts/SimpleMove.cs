using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class SimpleMove : APlayerControlledComponent {
    public float thrustSpeed = 5.0f;
    public float spinSpeed = 10.0f;

	public ParticleSystem thrusterParticles;
	public float maxEmissionRate = 5f;
	
	void FixedUpdate () {
		float rotationAmount = GetHorizontal();
		float thrust = 0.0f;
        if (GetButton("A")) {
			thrust = this.thrustSpeed;
		} else if (GetButton("B")) {
			thrust = -this.thrustSpeed;
		}

        rigidbody.AddTorque(rotationAmount * Vector3.up * this.spinSpeed);
		rigidbody.AddForce(transform.rotation * Vector3.forward * thrust);
    }
}
