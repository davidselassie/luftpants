using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// Player controlled firing and cooldown logic.
/// 
/// This has no directional control. Add a move component also to allow it to be aimed.
/// </summary>
public class FiringController : APlayerControlledComponent
{
    public GameObject BulletPrefab;
    public Transform Muzzle;
    public List<ParticleSystem> MuzzleFlashParticles;
    public AudioSource FireAudio;
    public float BulletVelocity = 50.0f;
    public float ReloadSeconds = 1.0f;
    private float _lastFireTime;
    
    void Start()
    {
        _lastFireTime = Time.time - ReloadSeconds;
    }
    
    void FixedUpdate()
    {
        if (GetButtonDown("A"))
        {
            Fire();
        }
    }
    
    public bool CanFire()
    {
        return Time.time - _lastFireTime > ReloadSeconds;
    }
    
    public void Fire()
    {
        if (CanFire())
        {
            var newBullet = (GameObject)Instantiate(BulletPrefab, Muzzle.position, Muzzle.rotation);
            newBullet.rigidbody.velocity = newBullet.transform.rotation * Vector3.forward * BulletVelocity;
            var bulletControllers = newBullet.GetComponentsInChildren<APlayerControlledComponent>().ToList();
            foreach (var controller in bulletControllers)
            {
                controller.PlayerIndex = PlayerIndex;
            }
            
            _lastFireTime = Time.time;
            if (MuzzleFlashParticles != null)
            {
                foreach (var ps in MuzzleFlashParticles)
                {
                    ps.Play();
                }
            }
            if (FireAudio != null)
            {
                FireAudio.Play();
            }
        }
    }
}
