using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Controls the forward thrust only of a ship. Also manages sound and thrust particles.
/// 
/// You probably want a rotation move component also.
/// </summary>
[RequireComponent(typeof(Rigidbody))]
public class ThrustMove : APlayerControlledComponent
{
    public float ThrustForce = 5.0f;
    public float CooldownSeconds = 0.0f;
    public bool AlwaysOn = false;
    public List<ParticleSystem> ThrusterParticles;
    public AudioSource ThrusterAudio;
    private float _lastThrustTime;

    void Start()
    {
        _lastThrustTime = Time.time - CooldownSeconds;
    }

    private bool CanThrust()
    {
        return Time.time - _lastThrustTime >= CooldownSeconds;
    }

    private bool ShouldTryThrust()
    {
        return AlwaysOn || CooldownSeconds <= 0.0f && GetButton("A") || GetButtonDown("A");
    }
    
    void FixedUpdate()
    {
        bool didThrust = false;
        if (ShouldTryThrust() && CanThrust())
        {
            rigidbody.AddForce(transform.rotation * Vector3.forward * ThrustForce);
            _lastThrustTime = Time.time;
            didThrust = true;
            
            if (ThrusterAudio != null && !ThrusterAudio.isPlaying)
            {
                ThrusterAudio.Play();
            }
        } else
        {
            if (ThrusterAudio != null && ThrusterAudio.isPlaying && ThrusterAudio.loop)
            {
                ThrusterAudio.Stop();
            }
        }

        if (ThrusterParticles != null)
        {
            foreach (var ps in ThrusterParticles)
            {
                ps.enableEmission = didThrust;
            }
        }
    }
}
