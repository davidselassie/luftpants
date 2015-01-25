using UnityEngine;
using System.Collections;

public class TorpedoLauncherControl : APlayerControlledComponent {
	public GameObject torpedoPrefab;
	public Transform muzzle;
	public ParticleSystem muzzleFlash;
	public float reloadSeconds = 1.0f;
	
	protected float lastFireTime;
	
	void Start () {
		this.lastFireTime = Time.time - this.reloadSeconds;
	}
	
	void FixedUpdate () {
		if (GetButton("A")) {
			Fire();
		}
	}
	
	public bool CanFire () {
		return Time.time - this.lastFireTime > this.reloadSeconds;
	}
	
	public void Fire () {
		if (CanFire ()) {
			GameObject newTorpedo = (GameObject) Instantiate(this.torpedoPrefab, muzzle.position, muzzle.rotation);
			PilotedTorpedo pilot = newTorpedo.GetComponent<PilotedTorpedo>();
			newTorpedo.rigidbody.velocity = newTorpedo.transform.rotation * Vector3.forward * pilot.speed;
			pilot.Player = this.Player;
			
            this.lastFireTime = Time.time;
			if (this.muzzleFlash != null) {
				this.muzzleFlash.Play();
			}
            
            AudioSource audio = GetComponent<AudioSource> ();
            if (audio != null) {
                audio.Play ();
            }
        }
	}
}
