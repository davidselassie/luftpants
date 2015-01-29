using UnityEngine;
using System.Collections;

public class DamagingComponent : MonoBehaviour {
    public float damage = 10.0f;
    public Collider AttachedShip;

    void Start(){
    }

    void OnCollisionEnter (Collision collision) {
        if (collision.transform.root.gameObject.GetInstanceID() == transform.root.gameObject.GetInstanceID())
            return;
        foreach (ContactPoint contact in collision.contacts) {
            HealthComponent otherHealth = contact.otherCollider.GetComponent<HealthComponent>();
            if (otherHealth != null) {
                otherHealth.health -= damage;
            }
        }
    }
}
