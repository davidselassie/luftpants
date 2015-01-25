using UnityEngine;
using System.Collections;
using UnityEngine.UI;
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
                if(roundsFinished == NumberOfRounds){
                    ResultsChangeTime = Time.time + ResultsTime;
                    CurrentPhase = Phases.FINAL_RESULTS;

                    string finalResultText = RenderFinalResult();
                    Text resultTextBox = Results.GetComponentInChildren<Text> ();
                    resultTextBox.text = finalResultText;
                }else{
                    Results.SetActive(false);
                    CurrentPhase = Phases.PLAY;
                    scoreMaster.Begin();
                    scoreMaster.enabled = true;
                }
            }
            break;
        case Phases.FINAL_RESULTS:
            if (Time.time > ResultsChangeTime && Input.anyKeyDown) {
                Results.SetActive(false);
                Credits.SetActive(true);
            }   
            break;
        }
    }

    private string RenderRoundResults(float[] resultScores){
        float score = 0f;
        List<int> victors = new List<int>();
        for (int i=0; i<resultScores.Length; i++) {
            if(resultScores[i] > 0){
                score = resultScores[i];
                victors.Add (i);
            }
        }
        string results = "Player ";
        results += victors [0] + 1;
        results += " and Player ";
        results += victors [1] + 1;
        results += " survived the round with ";
        results += score;
        results += " health remaining on their ship.";

        return results;
    }

    private string RenderFinalResult(){
        float leaderScore = 0f;
        int victor = 0;
        for (int i=0; i<scores.Length; i++) {
            if(scores[i] > leaderScore){
                leaderScore = scores[i];
                victor = i;
            }
        }
        string results = "Player ";
        results += victor + 1;
        results += " has emerged victorious with a whopping final score of ";
        results += leaderScore;
        results += ". \n\nThey alone answered correctly \nWHAT DO WE DO NOW?";
        
        return results;
    }

    public void RoundFinished(float[] resultScores){
        Results.SetActive(true);
        string roundResultText = RenderRoundResults (resultScores);
        Text resultTextBox = Results.GetComponentInChildren<Text> ();
        resultTextBox.text = roundResultText;

        scoreMaster.Clear();
        scoreMaster.enabled = false;

        for (int i = 0; i<scores.Length; i++) {
            scores [i] += resultScores [i];
        }
        roundsFinished++;

        CurrentPhase = Phases.RESULTS;
        ResultsChangeTime = Time.time + ResultsTime;
    }
    
    public enum Phases
    {
        LOGO,
        INSTRUCTIONS,
        PLAY,
        RESULTS,
        FINAL_RESULTS,
        CREDITS
    }
}
