using UnityEngine;
using System.Collections;

public class Explosion : MonoBehaviour {

    public GameObject shrapnel;
    public int shrapnelCount = 6;

	// Use this for initialization
	void Awake () {


	}

    void Update(){
        for (int i = 0; i < shrapnelCount; i++){
            GameObject.Instantiate(shrapnel, transform.position, this.transform.rotation);
            this.transform.eulerAngles += new Vector3(this.transform.eulerAngles.x, 360f / shrapnelCount, this.transform.eulerAngles.z);
        }
        
        GameObject.Destroy(this.gameObject);
    }
}
