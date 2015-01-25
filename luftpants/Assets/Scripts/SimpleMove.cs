using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class SimpleMove : APlayerControlledComponent {
    public float maxSpeed = 10f;
    public float burstSpeed = 5f;
    public float spinSpeed = 10f;
	public ParticleSystem thrusterParticles;
	public float maxEmissionRate = 5f;
	

	// Update is called once per frame
	void FixedUpdate () {
		float rotationAmount = GetHorizontal();
		float thrust = 0.0f;

        if (GetButton("A")) {
			thrust = this.burstSpeed;
            Debug.Log("A");
		} else if (GetButton("B")) {
			thrust = -this.burstSpeed;
		}

        rigidbody.AddTorque(rotationAmount * Vector3.up * spinSpeed);
		rigidbody.AddForce(this.transform.rotation * Vector3.forward * thrust);
    }
}
