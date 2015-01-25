using UnityEngine;
using System.Collections;

public class BulletController : MonoBehaviour {

    public ParticleSystem[] ImpactParticles;
    public float SurviveTime = 5f;

    public IEnumerator Impact(){
        MeshRenderer meshRenderer = GetComponent<MeshRenderer> ();
        meshRenderer.enabled = false;
        foreach (ParticleSystem particleSystem in ImpactParticles) {
            ParticleSystem impactParticles = (ParticleSystem) Instantiate(particleSystem,transform.position,transform.rotation);
            impactParticles.enableEmission = true;
        }
        AudioSource audio = GetComponent<AudioSource> ();
        if(audio != null) audio.Play ();
        yield return new WaitForSeconds(SurviveTime);
        Destroy(gameObject);
    }
}
