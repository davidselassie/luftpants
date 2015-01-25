using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class TeleportMove : APlayerControlledComponent {
	public float maxSpeed = 10f;
	public float spinSpeed = 10f;
	private float burstSpeed = 500f;
	private float travelTime = 0.1f;
	private float stopTime = 0f;

	//    private float lastX = 0f;
	//    private float lastY = 0f;
	//    private int spinDirection = 0; //positive is clockwise, negative is counterclockwise
	
	int Average(int[] arr){
		int sum = arr[0] + arr[1] + arr[2] + arr[3] + arr[4] + arr[5];
		if (sum >= 1)
			return 1;
		if (sum <= -1)
			return -1;
		return 0;
	}
	
	int StickSpin(float oldX, float newX, float oldY, float newY){
		//returns 1 for clockwise, -1 for counterclockwise, 0 for neither
		if (newX > oldX){
			if (newY > oldY)
				return -1;
			else if (newY < oldY)
				return 1;
		}else if (newX < oldX){
			if (newY > oldY)
				return -1;
			else if (newY < oldY)
				return 1;
		}
		return 0;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		float rotationAmount = GetHorizontal();
		
		if (Time.time > stopTime && Time.time - stopTime < 0.2){
			rigidbody.velocity = Vector3.zero;
		}else if (GetButton("A")) {
			rigidbody.velocity = this.burstSpeed * transform.forward;
			stopTime = Time.time + travelTime;
		}
		
		rigidbody.AddTorque(rotationAmount * Vector3.up * spinSpeed);
	}
}
