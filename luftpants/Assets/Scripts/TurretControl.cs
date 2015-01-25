using UnityEngine;
using System.Collections;

public class TurretControl : APlayerControlledComponent {
	public GameObject bulletPrefab;
    public Transform muzzle;
    public float bulletVelocity = 50.0f;
	public float reloadSeconds = 1.0f;

	protected float lastFireTime;
	
	void Start () {
		this.lastFireTime = Time.time - this.reloadSeconds;
	}

	void FixedUpdate () {
		Vector3 axesInput = GetAxes();
		if (axesInput.sqrMagnitude > 0.1f) {
            Vector3 intendedDirection = axesInput.normalized;

            transform.localRotation = Quaternion.LookRotation(intendedDirection,Vector3.up);

            //ROTATION
            //transform.RotateAround (transform.position,transform.up,axesInput.x);

            if(transform.localEulerAngles.y > 90 && transform.localEulerAngles.y < 180){
                transform.localEulerAngles = new Vector3(0,90,0);
            }
            if(transform.localEulerAngles.y <= 270 && transform.localEulerAngles.y >= 180){
                transform.localEulerAngles = new Vector3(0,270,0);
            }
            //this.transform.rotation = Quaternion.FromToRotation(Vector3.forward, axesInput);
		}

		if (GetButton("A")) {
			Fire();
		}
	}

	public bool CanFire () {
		return Time.time - this.lastFireTime > this.reloadSeconds;
	}

	public void Fire () {
		if (CanFire ()) {
			GameObject newBullet = (GameObject) Instantiate(this.bulletPrefab, muzzle.position, muzzle.rotation);
			newBullet.rigidbody.velocity = newBullet.transform.rotation * Vector3.forward * this.bulletVelocity;

			this.lastFireTime = Time.time;

			AudioSource audio = GetComponent<AudioSource> ();
			if (audio != null) {
				audio.Play ();
			}
		}
	}
}
