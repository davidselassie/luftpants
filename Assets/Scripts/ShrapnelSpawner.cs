using UnityEngine;
using System.Collections;

/// <summary>
/// On creation, generates shrapnel based on a prefab that will self destruct.
/// </summary>
public class ShrapnelSpawner : MonoBehaviour
{
    public GameObject ShrapnelPrefab;
    public int NumPieces = 6;
    public float SpawnOffset = 1.0f;
    public float Speed = 10.0f;
    public float LifetimeSeconds = 2.0f;
    
    void Awake()
    {
        for (int i = 0; i < NumPieces; i++)
        {
            float velAngle = (float)i / (float)NumPieces * 360.0f;
            var rot = Quaternion.Euler(0.0f, velAngle, 0.0f);

            var newShrapnel = (GameObject)Instantiate(
                ShrapnelPrefab,
                rot * Vector3.forward * SpawnOffset + transform.position,
                transform.rotation);
            newShrapnel.rigidbody.velocity = rot * Vector3.forward * Speed;
            newShrapnel.rigidbody.AddTorque(Random.rotation * Vector3.up);

            var destruct = newShrapnel.AddComponent<SelfDestructingBehavior>();
            destruct.LifetimeSeconds = LifetimeSeconds;
        }
    }
}
