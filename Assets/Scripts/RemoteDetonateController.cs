using UnityEngine;
using System.Collections;

/// <summary>
/// Player controlled component that will destroy itself on button press.
/// </summary>
public class RemoteDetonateController : APlayerControlledComponent
{
    void FixedUpdate()
    {
        if (GetButtonDown("A"))
        {
            Destroy(gameObject);
        }
    }
}
