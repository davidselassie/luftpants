using UnityEngine;
using System;
using System.Collections;

/// <summary>
/// Subclass in another component to read player input.
/// </summary>
public abstract class APlayerControlledComponent : MonoBehaviour
{
    public int PlayerIndex;

    protected float GetHorizontal()
    {
        return Input.GetAxis(String.Format("P{0} Horizontal", PlayerIndex));
    }

    protected float GetVertical()
    {
        return Input.GetAxis(String.Format("P{0} Vertical", PlayerIndex));
    }

    // Assume the level is oriented Z+ is down, X+ is right.
    protected Vector3 GetAxes()
    {
        return new Vector3(GetHorizontal(), 0.0f, GetVertical());
    }

    protected bool GetButtonDown(String button)
    {
        return Input.GetButtonDown(String.Format("P{0} {1}", PlayerIndex, button));
    }

    protected bool GetButton(String button)
    {
        return Input.GetButton(String.Format("P{0} {1}", PlayerIndex, button));
    }
}
