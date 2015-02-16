using UnityEngine;
using System.Collections;

/// <summary>
/// A kind of rotation control that uses the explicit joystick direction.
/// </summary>
public class TurretMove : APlayerControlledComponent
{
    void FixedUpdate()
    {
        Vector3 axesInput = GetAxes();
        if (axesInput.sqrMagnitude >= 0.01f)
        {
            Vector3 intendedDirection = axesInput.normalized;
            transform.localRotation = Quaternion.LookRotation(intendedDirection, Vector3.up);

            if (transform.localEulerAngles.y > 90 && transform.localEulerAngles.y < 180)
            {
                transform.localEulerAngles = new Vector3(0, 90, 0);
            }
            if (transform.localEulerAngles.y <= 270 && transform.localEulerAngles.y >= 180)
            {
                transform.localEulerAngles = new Vector3(0, 270, 0);
            }
        }
    }
}
