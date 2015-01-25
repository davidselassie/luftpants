using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BulletController : MonoBehaviour {

    public ParticleSystem[] ImpactParticles;
    public float SurviveTime = 5f;

    public IEnumerator Impact(){
        MeshRenderer meshRenderer = GetComponent<MeshRenderer> ();
        meshRenderer.enabled = false;
        Collider collider = GetComponent<Collider> ();
        collider.enabled = false;

        List<ParticleSystem> explosions = new List<ParticleSystem> ();
        foreach (ParticleSystem particleSystem in ImpactParticles) {
            ParticleSystem impactParticles = (ParticleSystem) Instantiate(particleSystem,transform.position,transform.rotation);
            impactParticles.enableEmission = true;
            explosions.Add(impactParticles);
        }
        AudioSource audio = GetComponent<AudioSource> ();
        if(audio != null) audio.Play ();
        yield return new WaitForSeconds(SurviveTime);

        foreach(ParticleSystem explosion in explosions){Destroy(explosion);}
        Destroy(gameObject);
    }
}
