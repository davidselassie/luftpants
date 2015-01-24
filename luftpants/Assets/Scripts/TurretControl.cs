using UnityEngine;
using System.Collections;

public class TurretControl : MonoBehaviour {
    public string player = "P1 ";
    private float verticalInput;
    private bool fireControl = false;
    public GameObject projectile;
    public Vector3 launchPoint;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        fireControl = Input.GetButtonDown(player + "A");
        verticalInput = Input.GetAxis(player + "Vertical");

        rigidbody.AddTorque(verticalInput * Vector3.up);
        if (fireControl)
            Instantiate(projectile,transform.position + launchPoint,this.transform.rotation);
	
	}
}
