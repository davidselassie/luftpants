using UnityEngine;
using System.Collections;

public class KillBox : MonoBehaviour {

	void OnCollisionEnter(Collision other){
		Destroy(other.gameObject);
	}
}
