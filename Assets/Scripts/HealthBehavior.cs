using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Keeps track of health and will destroy itself on death and create a death prefab.
/// </summary>
public class HealthBehavior : SafeMonoBehavior
{
    public float MaxHealth = 100.0f;
    public List<ParticleSystem> DamageParticles;
    public GameObject DeathPrefab;
    public float CurrentHealth { get; private set; }

    void Start()
    {
        CurrentHealth = MaxHealth;
        foreach (var ps in DamageParticles)
        {
            ps.enableEmission = false;
        }
    }

    public void Damage(float amount)
    {
        CurrentHealth -= amount;

        if (CurrentHealth <= 0.0f)
        {
            Destroy(gameObject);
        } else
        {
            for (int i = 0; i < DamageParticles.Count; i++)
            {
                ParticleSystem ps = DamageParticles [i];
                
                float percentHealth = MaxHealth / CurrentHealth;
                float psPercentHealthEmissionThreshold = (float)DamageParticles.Count / (float)i;
                ps.enableEmission = percentHealth >= psPercentHealthEmissionThreshold;
            }
        }
    }

    public override void SafeOnDestroy()
    {
        if (DeathPrefab != null)
        {
            Instantiate(DeathPrefab, transform.position, transform.rotation);
        }
    }
}
