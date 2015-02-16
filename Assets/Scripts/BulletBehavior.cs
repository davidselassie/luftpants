using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Object that self destructs on collision and creates an explosion prefab.
/// 
/// To damage things, you also need a damaging behavior.
/// </summary>
public class BulletBehavior : SafeMonoBehavior
{
    public List<GameObject> ExplosionPrefabs;

    void OnCollisionEnter()
    {
        Destroy(gameObject);
    }

    override public void SafeOnDestroy()
    {
        foreach (var explosion in ExplosionPrefabs)
        {
            Instantiate(explosion, transform.position, transform.rotation);
        }
    }
}
