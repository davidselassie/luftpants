using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameState : MonoBehaviour
{
    public ScoreMaster scoreMaster;

    public GameObject Logo;
    public GameObject Instructions;
    public GameObject Credits;
    public GameObject Results;

    public int NumberOfRounds = 3;

    public Phases CurrentPhase;
    //public static bool PlayIsFinished{
      //  get{ scoreMaster.RoundComplete }
    //}
    public float LogoTime = 1f;
    public float InstructionTime = 1f;
    public float ResultsTime = 1f;

    private float InstructionChangeTime;
    private float ResultsChangeTime;
    private int roundsFinished = 0;
    private float[] scores = new float[4];

    // Use this for initialization
    void Start ()
    {
        for (int i = 0; i<scores.Length; i++) {
            scores [i] = 0f;
        }
        if (scoreMaster == null)
            scoreMaster = GetComponent<ScoreMaster> ();
        scoreMaster.enabled = false;

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
                scoreMaster.Begin();
                scoreMaster.enabled=true;
            }
            break;
        case Phases.RESULTS:
            if (Time.time > ResultsChangeTime && Input.anyKeyDown) {
                Results.SetActive(false);
                if(roundsFinished == NumberOfRounds){
                    CurrentPhase = Phases.CREDITS;
                    Credits.SetActive(true);
                }
                CurrentPhase = Phases.PLAY;
                scoreMaster.Begin();
                scoreMaster.enabled = true;
            }
            break;
        }
    }

    public void RoundFinished(float[] resultScores){
        scoreMaster.Clear();
        scoreMaster.enabled = false;
        for (int i = 0; i<scores.Length; i++) {
            scores [i] += resultScores [i];
        }
        roundsFinished++;

        CurrentPhase = Phases.RESULTS;
        ResultsChangeTime = Time.time + ResultsTime;
        Results.SetActive(true);
    }
    
    public enum Phases
    {
        LOGO,
        INSTRUCTIONS,
        PLAY,
        RESULTS,
        CREDITS
    }
}
