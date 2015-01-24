using UnityEngine;
using System.Collections;

public class TurretControl : MonoBehaviour {
    public string player = "P2 ";
    private float verticalInput;
    private float horizontalInput;
    private bool fireControl = false;
    public GameObject projectile;
    public Vector3 launchPoint;
    public float spinSpeed = 0.5f;

	// Use this for initialization
	void Start () {
	
	}
    //Update for input
    void Update () {
        fireControl = Input.GetButtonDown(player + "A");
        horizontalInput = Input.GetAxis(player + "Horizontal");

        if (Input.GetButtonDown("P1 A"))
            Debug.Log("P1 A");
        if (Input.GetButtonDown("P2 A"))
            Debug.Log("P2 A");
        if (Input.GetButtonDown("P3 A"))
            Debug.Log("P3 A");
        if (Input.GetButtonDown("P4 A"))
            Debug.Log("P4 A");

    }
	
	// FixedUpdate for physics
	void FixedUpdate () {

        rigidbody.AddTorque(horizontalInput * Vector3.up * spinSpeed);

	
	}
}
