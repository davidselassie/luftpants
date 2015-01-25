using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class ShieldController : APlayerControlledComponent {

    void Start () {

    }

    void FixedUpdate () {
		float rotationAmount = GetHorizontal();
		rigidbody.AddTorque(rotationAmount * Vector3.up);
    }
}
