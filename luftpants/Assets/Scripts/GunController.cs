using UnityEngine;
using System.Collections;

public class Gun : MonoBehaviour
{
    public float SpawnPeriod = 0.5f;
    public GameObject seed;

    // Use this for initialization
    void Start ()
    {
        seed.SetActive (false);
    }
    
    // Update is called once per frame
    void Update ()
    {
        GameObject clone = (GameObject) Instantiate (seed);
        clone.SetActive (true);
        clone.transform.position = transform.position;
        clone.transform.forward = transform.forward;
    }
}
