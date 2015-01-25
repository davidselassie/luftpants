using UnityEngine;
using System.Collections;

public class HealthComponent : MonoBehaviour {
    public float health = 100.0f;
    public float MaxHealth = 100.0f;
    public float MaxEmissionRate = 5f;
	public bool immortal = false;

	public bool useThreeParticleTypes = false;
	public ParticleSystem lowDamage;
	public float lowDamageEmissionRate = 5f;
	public ParticleSystem medDamage;
	public float medDamageEmissionRate = 5f;
	public ParticleSystem highDamage;
	public float highDamageEmissionRate = 5f;

	private ParticleSystem smokeSystem;

	public GameObject deathPrefab;

	// Use this for initialization
	void Start () {
        health = MaxHealth;
		if (useThreeParticleTypes == false){
			smokeSystem = GetComponent<ParticleSystem> ();
		} else{
			lowDamage.emissionRate = 0f;
			medDamage.emissionRate = 0f;
			highDamage.emissionRate = 0f;
		}
    }
    
    void FixedUpdate () {
        if (health <= 0.0f && immortal == false) {
            Destroy(gameObject);
        }
		if (useThreeParticleTypes){
			if ((MaxHealth - health) / MaxHealth > 0.25f)
				lowDamage.emissionRate = lowDamageEmissionRate;
			if ((MaxHealth - health) / MaxHealth > 0.50f)
				medDamage.emissionRate = medDamageEmissionRate;
			if ((MaxHealth - health) / MaxHealth > 0.75f)
				highDamage.emissionRate = highDamageEmissionRate;

		}else{
			if (health >= MaxHealth)
				smokeSystem.emissionRate = ((MaxHealth - health) / MaxHealth) * MaxEmissionRate;
			else
				smokeSystem.emissionRate = MaxEmissionRate;
		}
	}

	void OnDestroy () {
		Instantiate(this.deathPrefab, transform.position, transform.rotation);
	}
}
