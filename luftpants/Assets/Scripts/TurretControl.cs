using UnityEngine;
using System.Collections;

public class TurretControl : MonoBehaviour {
    public string player = "P1 ";
    private float verticalInput;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
       
        verticalInput = Input.GetAxis(player + "Vertical");

        rigidbody.AddTorque(verticalInput * Vector3.up);
	
	}
}
