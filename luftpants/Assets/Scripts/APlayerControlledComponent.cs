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

	protected Vector3 GetAxes () {
		return new Vector3(GetHorizontal(), 0.0f, GetVertical());
	}

	protected bool GetButtonDown (String button) {
		return Input.GetButtonDown (String.Format ("P{0} {1}", this.Player, button));
	}
}
