using UnityEngine;
using System.Collections;

/// <summary>
/// Creates an object that will destroy itself after a period of time.
/// </summary>
public class SelfDestructingBehavior : MonoBehaviour
{
    public float LifetimeSeconds = 5.0f;
    private float _deathTime;

    void Start()
    {
        _deathTime = Time.time + LifetimeSeconds;
    }

    void FixedUpdate()
    {
        if (Time.time >= _deathTime)
        {
            Destroy(gameObject);
        }
    }
}
