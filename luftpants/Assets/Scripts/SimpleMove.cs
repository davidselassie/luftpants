﻿using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class SimpleMove : APlayerControlledComponent {
    public float maxSpeed = 10f;
    public float burstSpeed = 5f;
    public float spinSpeed = 10f;

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
		float thrust = 0.0f;

        if (GetButton("A")) {
			thrust = this.burstSpeed;
		} else if (GetButton("B")) {
			thrust = -this.burstSpeed;
		}

        rigidbody.AddTorque(rotationAmount * Vector3.up * spinSpeed);
		rigidbody.AddForce(this.transform.rotation * Vector3.forward * thrust);
    }
}
