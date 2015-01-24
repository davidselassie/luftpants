using UnityEngine;
using System.Collections;

public class BulletController : MonoBehaviour {

    public ParticleSystem[] ImpactParticles;

    public void Impact(){
        Debug.Log ("Impact!");
        MeshRenderer meshRenderer = GetComponent<MeshRenderer> ();
        meshRenderer.enabled = false;
        ParticleSystem[] particleSystems = GetComponents<ParticleSystem> ();
        foreach (ParticleSystem particleSystem in ImpactParticles) {
            ParticleSystem impactParticles = (ParticleSystem) Instantiate(particleSystem,transform.position,transform.rotation);
            impactParticles.enableEmission = true;
        }
        Destroy(gameObject);
    }
}
