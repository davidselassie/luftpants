using UnityEngine;
using System.Collections;

public class RammingController : MonoBehaviour {
    public float damageMultiplier = 0.5f;
    public float threshholdVelocity = 30f;
    public GameObject[] ImpactAnimations;
    
    void OnCollisionEnter (Collision collision) {
        float damage = 0f;
        if (collision.relativeVelocity.magnitude > threshholdVelocity) {
            damage = damageMultiplier * collision.relativeVelocity.magnitude;
            Debug.Log ("ramming damage: " + damage);
            Debug.Log ("ramming speed: " + collision.relativeVelocity.magnitude);
            foreach (ContactPoint contact in collision.contacts) {
                HealthComponent otherHealth = contact.otherCollider.GetComponent<HealthComponent> ();
                if (otherHealth != null) {
                    otherHealth.health -= damage;
                }
            }
            
            foreach (GameObject anim in ImpactAnimations) {
                Instantiate(anim, transform.position, transform.rotation);
            }
        }
    }
}
