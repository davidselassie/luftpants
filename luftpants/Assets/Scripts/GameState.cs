using UnityEngine;
using System.Collections;

public class GameState : MonoBehaviour
{
    public GameObject Logo;
    public GameObject Instructions;
    public GameObject Credits;

    public Phases CurrentPhase;
    public bool PlayIsFinished{
        get{
            return Time.time > 15f;
        }
    }
    public float LogoTime = 5f;
    public float InstructionTime = 5f;

    private float InstructionChangeTime;

    // Use this for initialization
    void Start ()
    {
        CurrentPhase = Phases.LOGO;
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
            }
            break;
        case Phases.PLAY:
            if(PlayIsFinished){
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
