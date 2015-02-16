using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Manages control of the warp ship. It's very similar to the ThrustMove component, but with cooldown and a timer.
/// </summary>
[RequireComponent(typeof(Rigidbody))]
public class WarpMove : APlayerControlledComponent
{
    public float WarpForce = 500.0f;
    public float WarpSeconds = 0.15f;
    public float CooldownSeconds = 4.0f;
    public AudioSource EngineAudio;
    public List<ParticleSystem> ThrusterParticles;
    private float _stopTime = -1.0f;
    private float _lastBurstTime;

    void Start()
    {
        _lastBurstTime = Time.time - CooldownSeconds;
    }

    private bool CanBurst()
    {
        return Time.time - _lastBurstTime >= CooldownSeconds;
    }

    void FixedUpdate()
    {
        if (_stopTime > 0.0f && Time.time >= _stopTime)
        {
            rigidbody.velocity = Vector3.zero;
            rigidbody.angularVelocity = Vector3.zero;
            foreach (var ps in ThrusterParticles)
            {
                ps.enableEmission = false;
            }

            _stopTime = -1.0f;
        } else if (GetButtonDown("A") && CanBurst())
        {
            rigidbody.velocity = WarpForce * transform.forward;
            foreach (var ps in ThrusterParticles)
            {
                ps.enableEmission = true;
            }

            _stopTime = Time.time + WarpSeconds;
            _lastBurstTime = Time.time;

            if (EngineAudio != null)
            {
                EngineAudio.Play();
            }
        }
    }
}
