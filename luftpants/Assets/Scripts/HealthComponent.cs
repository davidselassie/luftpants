using UnityEngine;
using System.Collections;

public class HealthComponent : MonoBehaviour {
    public float health = 100.0f;
    public float MaxHealth = 100.0f;
    public float MaxEmissionRate = 5f;
	public ParticleSystem smokeSystem;

	// Use this for initialization
	void Start () {
        health = MaxHealth;
        if(smokeSystem == null)
            smokeSystem = smokeSystem.GetComponentInChildren<ParticleSystem> ();
	}

	void FixedUpdate () {
        if (health <= 0.0f) {
            Destroy(gameObject);
        }
        smokeSystem.emissionRate = ((MaxHealth - health) / MaxHealth) * MaxEmissionRate;
	}

    void Update(){
    }
}
