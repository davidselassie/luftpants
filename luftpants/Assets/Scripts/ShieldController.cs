using UnityEngine;
using System.Collections;

public class ShieldController : MonoBehaviour {
    public string player = "P2 ";

    void Start () {

    }

    void FixedUpdate () {
        float h = Input.GetAxis(player + "Horizontal");

        transform.Rotate(new Vector3(0.0f, h, 0.0f));
    }
}
