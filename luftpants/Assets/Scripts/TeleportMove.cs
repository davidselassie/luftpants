using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class TeleportMove : APlayerControlledComponent {
	public float burstSpeed = 500.0f;
	public float spinSpeed = 10.0f;
	public float burstSeconds = 0.5f;
	public float cooldownSeconds = 4.0f;

	public ParticleSystem thrusterParticles;
	public float maxEmissionRate = 5f;

	private float stopTime = -1.0f;
	private float lastBurstTime;

	void Start () {
		this.lastBurstTime = Time.time - this.cooldownSeconds;
	}

	public bool CanBurst () {
		return Time.time - this.lastBurstTime > this.cooldownSeconds;
	}

	void FixedUpdate () {
		float rotationAmount = GetHorizontal();
		
		if (this.stopTime > 0.0f && Time.time >= stopTime) {
			rigidbody.velocity = Vector3.zero;
			rigidbody.angularVelocity = Vector3.zero;
			if (thrusterParticles != null) {
				thrusterParticles.emissionRate = 0f;
			}

			this.stopTime = -1.0f;
		} else if (GetButton("A") && CanBurst()) {
			rigidbody.velocity = this.burstSpeed * transform.forward;
			if (thrusterParticles != null) {
				thrusterParticles.emissionRate = maxEmissionRate;
			}

			this.stopTime = Time.time + this.burstSeconds;
			this.lastBurstTime = Time.time;
		}
		
		rigidbody.AddTorque(rotationAmount * Vector3.up * this.spinSpeed);
	}
}
