using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Controls only rotational movement via a torque.
/// 
/// To directly aim without a rigidbody, use the turret move.
/// </summary>
[RequireComponent(typeof(Rigidbody))]
public class RotationMove : APlayerControlledComponent
{
    public float TurnForce = 10.0f;
    
    void FixedUpdate()
    {
        float rotationAmount = GetHorizontal();
        rigidbody.AddTorque(rotationAmount * Vector3.up * TurnForce);
    }
}
