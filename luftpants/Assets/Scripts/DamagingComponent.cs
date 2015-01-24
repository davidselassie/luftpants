using UnityEngine;
using System.Collections;

public class DamagingComponent : MonoBehaviour {
    public float damage = 10.0f;

	// Use this for initialization
	void Start () {

	}

    void OnCollisionEnter (Collision collision) {
        foreach (ContactPoint contact in collision.contacts) {
            HealthComponent otherHealth = contact.otherCollider.GetComponent<HealthComponent>();
            if (otherHealth != null) {
                otherHealth.health -= damage;
                BulletController bulletController = GetComponent<BulletController>();
                if(bulletController != null) bulletController.Impact();
            }
        }
    }
}
