using UnityEngine;
using System.Collections;

public class HealthComponent : MonoBehaviour {
    public float health = 100.0f;
    public float MaxHealth = 100.0f;
    public float MaxEmissionRate = 5f;

	// Use this for initialization
	void Start () {
        health = MaxHealth;
	}

	void FixedUpdate () {
        if (health <= 0.0f) {
            Destroy(gameObject);
            GameState.PlayIsFinished = true;
        }
        ParticleSystem particleSystem = GetComponent<ParticleSystem> ();
        particleSystem.emissionRate = ((MaxHealth - health) / MaxHealth) * MaxEmissionRate;
	}

    void Update(){
    }
}
