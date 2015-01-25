using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class TeleportMove : APlayerControlledComponent {
	public float maxSpeed = 10f;
	public float spinSpeed = 10f;
	public float burstSpeed = 500f;
	public float travelTime = 0.1f;

	private float stopTime = 0f;

	void FixedUpdate () {
		float rotationAmount = GetHorizontal();
		
		if (Time.time > stopTime && Time.time - stopTime < 0.2) {
			rigidbody.velocity = Vector3.zero;
			rigidbody.angularVelocity = Vector3.zero; 
		} else if (GetButton("A")) {
			rigidbody.velocity = this.burstSpeed * transform.forward;
			stopTime = Time.time + travelTime;
		}
		
		rigidbody.AddTorque(rotationAmount * Vector3.up * spinSpeed);
	}
}
