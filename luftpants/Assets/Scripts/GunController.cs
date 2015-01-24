using UnityEngine;
using System.Collections;

public class GunController : MonoBehaviour
{
    public float SpawnPeriod = 0.5f;
    public GameObject Seed;
    public float BulletSpeed = 3.0f;

    // Use this for initialization
    void Start ()
    {
        Seed.SetActive (false);
    }
    
    // Update is called once per frame
    public void Fire ()
    {
        Debug.Log ("FIRE!");
        GameObject clone = (GameObject) Instantiate (Seed);
        clone.SetActive (true);
        clone.transform.position = transform.position;
        clone.transform.forward = transform.forward;
        clone.rigidbody.velocity = BulletSpeed * clone.transform.forward;
    }
}
