using UnityEngine;
using System.Collections;

public class ShrapnelSpawner : MonoBehaviour {
	public GameObject shrapnelPrefab;
	public int numPieces = 6;
	public float spawnOffset = 1.0f;
    public float speed = 10.0f;
	public float lifetimeSeconds = 2.0f;
	
	void Awake () {
		for (int i = 0; i < this.numPieces; i++) {
			float velAngle = (float) i / (float) this.numPieces * 360.0f;
			Quaternion rot = Quaternion.Euler(0.0f, velAngle, 0.0f);


			GameObject newShrapnel = (GameObject) Instantiate(this.shrapnelPrefab, rot * Vector3.forward * spawnOffset + transform.position, transform.rotation);
			newShrapnel.rigidbody.velocity = rot * Vector3.forward * this.speed;
			newShrapnel.rigidbody.AddTorque(Random.rotation * Vector3.up);

			SelfDestructing destruct = newShrapnel.AddComponent<SelfDestructing>();
			destruct.lifetimeSeconds = this.lifetimeSeconds;
		}
	}
}
