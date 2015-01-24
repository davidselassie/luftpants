using UnityEngine;
using System.Collections;

public class DamagingComponent : MonoBehaviour {
    public float damage = 10.0f;

	// Use this for initialization
	void Start () {
	
	}

    override void OnCollisionEnter (Collision collision) {
        foreach (ContactPoint contact in collision.contacts) {
            HealthComponent otherHealth = contact.otherCollider.gameObject.GetComponent<HealthComponent>();
            if (otherHealth) {
                otherHealth.health -= damage;
            }
        }
    }
}
