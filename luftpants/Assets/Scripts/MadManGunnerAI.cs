using UnityEngine;
using System.Collections;

public class MadManGunnerAI : MonoBehaviour {

    public float FirePeriod = 1.0f;
    private float NextFireTime;

	// Use this for initialization
	void Start () {
        NextFireTime = Time.time + FirePeriod;
	}
	
	// Update is called once per frame
    void Update () {
	    if (Time.time > NextFireTime) {
            GetComponent<GunController>().Fire();
            NextFireTime = Time.time + FirePeriod;
        }
	}
}
