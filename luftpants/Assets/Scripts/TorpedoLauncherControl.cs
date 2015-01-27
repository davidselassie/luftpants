using UnityEngine;
using System.Collections;

public class TorpedoLauncherControl : APlayerControlledComponent {
	public GameObject torpedoPrefab;
	public Transform muzzle;
	public ParticleSystem muzzleFlash;
	public ParticleSystem muzzleSmoke;
	public float reloadSeconds = 1.0f;
	
	protected float lastFireTime;
	protected GameObject lastTorpedo;
	
	void Start () {
		this.lastFireTime = Time.time - this.reloadSeconds;
	}
	
	void FixedUpdate () {
		if (GetButtonDown ("A")) {
		// if (Input.GetKeyDown("space")) { // for testing
			if (this.lastTorpedo == null) {
				Fire();
			} else {
				Destroy (this.lastTorpedo);
				this.lastTorpedo = null;
			}
		}
	}
	
	public bool CanFire () {
		return Time.time - this.lastFireTime > this.reloadSeconds;
	}
	
	public void Fire () {
		if (CanFire ()) {
			this.lastTorpedo = (GameObject) Instantiate(this.torpedoPrefab, muzzle.position, muzzle.rotation);
			PilotedTorpedo pilot = this.lastTorpedo.GetComponent<PilotedTorpedo>();
			this.lastTorpedo.rigidbody.velocity = this.lastTorpedo.transform.rotation * Vector3.forward * pilot.thrustSpeed;
			pilot.Player = this.Player;
			
            this.lastFireTime = Time.time;
			if (this.muzzleFlash != null) {
				this.muzzleFlash.Play();
				// add this pup pup for smoke burst at muzzle flash?
				//this.muzzleSmoke.Play ();
			}

            
            AudioSource audio = GetComponent<AudioSource> ();
            if (audio != null) {
                audio.Play ();
            }
        }
	}
}
