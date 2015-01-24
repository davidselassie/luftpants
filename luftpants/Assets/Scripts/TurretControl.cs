using UnityEngine;
using System.Collections;

public class TurretControl : APlayerControlledComponent {
	public GameObject projectile;
    public Vector3 launchPoint;
    public float spinSpeed = 0.5f;

	// Use this for initialization
	void Start () {
	
	}

	void FixedUpdate () {
		Vector3 axesInput = GetAxes();
		this.transform.rotation = Quaternion.FromToRotation(Vector3.forward, axesInput);
	}
}
