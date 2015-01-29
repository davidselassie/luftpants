using UnityEngine;
using System.Collections;

public abstract class SafeMonoBehavior : MonoBehaviour {
	private bool inQuit = false;

	void OnDestroy () {
		if (!this.inQuit) {
			SafeOnDestroy ();
		}
	}

	public abstract void SafeOnDestroy ();

	void OnApplicationQuit () {
		this.inQuit = true;
	}
}
