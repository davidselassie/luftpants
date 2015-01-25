using UnityEngine;
using System.Collections;

public class Explosion : MonoBehaviour {

    public GameObject shrapnel;
    public int shrapnelCount = 6;
    private int i;

	// Use this for initialization
	void Awake () {
        i = 0;

	}

    void FixedUpdate(){
        if (i < shrapnelCount){
            GameObject.Instantiate(shrapnel, transform.position + transform.forward / 3f, gameObject.transform.rotation);
            transform.Rotate(Vector3.up,360f / shrapnelCount);
            i++;
        } else
            GameObject.Destroy(this.gameObject);
    }
}
