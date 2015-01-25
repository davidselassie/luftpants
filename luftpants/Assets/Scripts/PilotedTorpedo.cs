using UnityEngine;
using System.Collections;

public class PilotedTorpedo : APlayerControlledComponent {

    public float speed = 10f;
    public float turnSpeed = 1f;
    public GameObject explosion;
    private float horizontalInput;
    private bool selfDestruct;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        horizontalInput = GetHorizontal();
        selfDestruct = GetButton("A");


        rigidbody.AddTorque(Vector3.up * horizontalInput * turnSpeed);
        rigidbody.AddForce(transform.forward * speed);

        if (selfDestruct)
            Destroy(gameObject);


	}

    void OnDestroy(){
        GameObject.Instantiate(explosion,gameObject.transform.position,Quaternion.identity);
    }
}
