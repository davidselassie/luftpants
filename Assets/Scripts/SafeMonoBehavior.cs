using UnityEngine;
using System.Collections;

/// <summary>
/// Provides a SafeOnDestroy that only fires when an object is destroyed *in game* not during simulator shutdown.
/// </summary>
public abstract class SafeMonoBehavior : MonoBehaviour
{
    private bool _inQuit = false;

    void OnDestroy()
    {
        if (!_inQuit)
        {
            SafeOnDestroy();
        }
    }

    public abstract void SafeOnDestroy();

    void OnApplicationQuit()
    {
        _inQuit = true;
    }
}
