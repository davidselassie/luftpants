using UnityEngine;
using System;
using System.Collections;

public abstract class APlayerControlledComponent : MonoBehaviour {
	public int Player;

	protected float GetHorizontal () {
		return Input.GetAxis(String.Format ("P{0} Horizontal", this.Player));
	}

	protected float GetVertical () {
		return Input.GetAxis(String.Format ("P{0} Vertical", this.Player));
	}

	protected bool GetButton (String button) {
		return Input.GetButton (String.Format ("P{0} {1}", this.Player, button));
	}
}
