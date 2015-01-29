using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Will damage any objects with health that come into contact with this.
/// </summary>
public class DamagingBehavior : MonoBehaviour
{
    public float Damage = 10.0f;
    public List<HealthBehavior> DontDamage;

    void OnCollisionEnter(Collision collision)
    {
        foreach (var contact in collision.contacts)
        {
            HealthBehavior otherHealth = contact.otherCollider.GetComponent<HealthBehavior>();
            if (otherHealth != null && CanDamage(otherHealth))
            {
                otherHealth.Damage(Damage);
            }
        }
    }

    private bool CanDamage(HealthBehavior other)
    {
        foreach (var dontDamage in DontDamage)
        {
            if (dontDamage == other)
            {
                return false;
            }
        }
        return true;
    }
}
