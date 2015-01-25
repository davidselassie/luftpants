using UnityEngine;
using System.Collections;

public class HealthComponent : MonoBehaviour {
    public float health = 100.0f;
    public float MaxHealth = 100.0f;
    public float MaxEmissionRate = 5f;
	public ParticleSystem[] smokeSystem;
	public bool immortal = false;

	// Use this for initialization
	void Start () {
        health = MaxHealth;
		if (smokeSystem == null)
			smokeSystem = GetComponents<ParticleSystem> ();
    }
    
    void FixedUpdate () {
        if (health <= 0.0f && immortal == false) {
            Destroy(gameObject);
        }

		for (int i = 0; i < smokeSystem.Length; i++){
			if (health >= MaxHealth)
        		smokeSystem[i].emissionRate = ((MaxHealth - health) / MaxHealth) * MaxEmissionRate;
			else
				smokeSystem[i].emissionRate = MaxEmissionRate;
		}
	}
}
