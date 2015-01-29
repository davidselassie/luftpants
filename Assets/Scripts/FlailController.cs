using UnityEngine;
using System.Collections;

public class FlailController : APlayerControlledComponent  {

    //public Transform ShipCenter;
    public float TorqueMultiplier = 1f;
    public float BounceDampen = 0.5f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
    void FixedUpdate () {
        Vector3 axesInput = GetAxes();
        if (axesInput.sqrMagnitude > 0.1f) {
            //Vector3 intendedDirection = axesInput.normalized;
            rigidbody.AddTorque(axesInput.x * TorqueMultiplier * transform.right);

            //ROTATION
            //transform.RotateAround (ShipCenter.position,transform.right,intendedDirection.x);
            
            if(transform.localEulerAngles.x >= 90 || transform.localEulerAngles.x <= 270){//&& transform.localEulerAngles.y < 180){
                //rigidbody.angularVelocity = -rigidbody.angularVelocity*BounceDampen;
              //  transform.localEulerAngles = new Vector3(0,90,0);
            }
            if(transform.localEulerAngles.y <= 270 && transform.localEulerAngles.y >= 180){
                //rigidbody.angularVelocity = -rigidbody.angularVelocity*BounceDampen;
            //    transform.localEulerAngles = new Vector3(0,270,0);
            }
            //this.transform.rotation = Quaternion.FromToRotation(Vector3.forward, axesInput);
        }
	}
}
