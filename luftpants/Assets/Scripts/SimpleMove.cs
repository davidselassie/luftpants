using UnityEngine;
using System.Collections;


public class SimpleMove : MonoBehaviour {

    public string player = "P1 ";

    public float maxSpeed = 10f;
    public float burstSpeed = 5f;

    public float horizontalInput;
    public float verticalInput;


	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

        horizontalInput = Input.GetAxis(player + "Horizontal");
        if(Input.GetButton(player + "A"))
            verticalInput = burstSpeed;
        else if (Input.GetButton(player + "B"))
            verticalInput = -1f * burstSpeed;
        else
            verticalInput = 0f;


        rigidbody.AddTorque(horizontalInput * Vector3.up);

        rigidbody.AddForce(verticalInput * transform.forward);
	}
}
