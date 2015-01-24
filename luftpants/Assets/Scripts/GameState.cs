using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameState : MonoBehaviour
{
    public GameObject Logo;
    public GameObject Instructions;
    public GameObject Credits;
    public List<GameObject> Players;

    public Phases CurrentPhase;
    public static bool PlayIsFinished;
    public float LogoTime = 1f;
    public float InstructionTime = 1f;

    private float InstructionChangeTime;

    // Use this for initialization
    void Start ()
    {
        CurrentPhase = Phases.LOGO;
        
        foreach(GameObject player in Players){player.SetActive(false);}
        Logo.SetActive(true);
        Instructions.SetActive (false);
        Credits.SetActive (false);
    }
    
    // Update is called once per frame
    void Update ()
    {
        switch (CurrentPhase) {
        case Phases.LOGO:
            if (Time.time > LogoTime && Input.anyKeyDown) {
                CurrentPhase = Phases.INSTRUCTIONS;
                Logo.SetActive (false);
                InstructionChangeTime = Time.time + InstructionTime;
                Instructions.SetActive (true);
            }   
            break;
        case Phases.INSTRUCTIONS:
            if (Time.time > InstructionChangeTime && Input.anyKeyDown) {
                CurrentPhase = Phases.PLAY;
                Instructions.SetActive(false);
                foreach(GameObject player in Players){player.SetActive(true);}
            }
            break;
        case Phases.PLAY:
            if(PlayIsFinished){
                foreach(GameObject player in Players){
                    if(player != null) player.SetActive(false);
                }
                CurrentPhase = Phases.CREDITS;
                Credits.SetActive(true);
            }
            break;
        }
    }
    
    public enum Phases
    {
        LOGO,
        INSTRUCTIONS,
        PLAY,
        CREDITS
    }
}
