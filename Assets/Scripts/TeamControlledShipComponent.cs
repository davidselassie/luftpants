using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Denotes a ship and makes sure that input on controlled components are setup correctly.
/// 
/// Explicitly link every subclass of APlayerControlledComponent here to ensure it reads the right input.
/// 
/// This is also what the level manager looks for to setup a ship.
/// </summary>
public class TeamControlledShipComponent : MonoBehaviour
{
    public int PlayerAIndex;
    public int PlayerBIndex;
    public List<APlayerControlledComponent> PlayerAControls;
    public List<APlayerControlledComponent> PlayerBControls;

    void Start()
    {
        Sync();
    }

    public void Sync()
    {
        foreach (var pcc in PlayerAControls)
        {
            pcc.PlayerIndex = PlayerAIndex;
        }
        foreach (var pcc in PlayerBControls)
        {
            pcc.PlayerIndex = PlayerBIndex;
        }
    }
}
