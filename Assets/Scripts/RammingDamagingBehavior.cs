using UnityEngine;
using System.Collections;

/// <summary>
/// Damages health components it comes in contact with with damage proportional to the impact velocity.
/// </summary>
public class RammingDamagingBehavior : MonoBehaviour
{
    public float DamageMultiplier = 0.5f;
    public float ThreshholdVelocity = 10f;
    
    void OnCollisionEnter(Collision collision)
    {
        if (collision.relativeVelocity.magnitude > ThreshholdVelocity)
        {
            foreach (var contact in collision.contacts)
            {
                HealthBehavior otherHealth = contact.otherCollider.GetComponent<HealthBehavior>();
                if (otherHealth != null)
                {
                    otherHealth.Damage(DamageMultiplier * collision.relativeVelocity.magnitude);
                }
            }
        }
    }
}
