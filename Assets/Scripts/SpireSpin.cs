using UnityEngine;
using System.Collections;

public class SpireSpin : MonoBehaviour {

	public float spinSpeed = 10f;

	// Use this for initialization
	void Start () {
		rigidbody.AddTorque(Vector3.up * spinSpeed);

	}


}
