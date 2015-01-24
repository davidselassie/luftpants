using UnityEngine;
using System.Collections;

public class TurretControl : APlayerControlledComponent {
	public GameObject projectile;
    public Vector3 launchPoint;

	// Use this for initialization
	void Start () {
	
	}

	void FixedUpdate () {
		float verticalInput = GetVertical();

        rigidbody.AddTorque(verticalInput * Vector3.up);
	}
}
