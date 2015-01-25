using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class GameState : MonoBehaviour
{
    public ScoreMaster scoreMaster;

    public GameObject UI;
    public GameObject Logo;
    public GameObject Instructions;
    public GameObject Credits;
    public GameObject Results;
    public List<string> Scenes;
    public List<string> SceneList;

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

    void FillSceneList(){
        SceneList = new List<string> ();
        for (int i=0; i<NumberOfRounds; i++) {
            SceneList.Add(Scenes[i%Scenes.Count]);
        }
    }

    // Use this for initialization
    void Start ()
    {
        for (int i = 0; i<scores.Length; i++) {
            scores [i] = 0f;
        }
        if (scoreMaster == null)
            scoreMaster = GetComponent<ScoreMaster> ();
        scoreMaster.gameState = this;
        scoreMaster.enabled = false;

        CurrentPhase = Phases.LOGO;
        Logo.SetActive(true);
        Instructions.SetActive (false);
        Credits.SetActive (false);

        RenderImmortal();
        if (SceneList.Count != NumberOfRounds)
            FillSceneList ();
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
                Instructions.SetActive(false);

                CurrentPhase = Phases.LOADING;
                StartNewRound();
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
                    CurrentPhase = Phases.LOADING;
                    StartNewRound();
                }
            }
            break;
        case Phases.LOADING:
            scoreMaster.Begin();
            scoreMaster.enabled=true;
            CurrentPhase = Phases.PLAY;
            break;
        case Phases.FINAL_RESULTS:
            if (Time.time > ResultsChangeTime && Input.anyKeyDown) {
                Results.SetActive(false);
                Credits.SetActive(true);
                CurrentPhase = Phases.CREDITS;
            }   
            break;
        case Phases.CREDITS:
            if (Time.time > LogoTime && Input.anyKeyDown) {
                CurrentPhase = Phases.INSTRUCTIONS;
                Credits.SetActive (false);
                InstructionChangeTime = Time.time + InstructionTime;
                Instructions.SetActive (true);
            }   
            break;
        }
    }

    public void StartNewRound(){
        Application.LoadLevel (SceneList[roundsFinished]);
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


    private void RenderImmortal(){
        DontDestroyOnLoad (UI);
        DontDestroyOnLoad(this);
    }
    
    public enum Phases
    {
        LOGO,
        INSTRUCTIONS,
        PLAY,
        RESULTS,
        LOADING,
        FINAL_RESULTS,
        CREDITS
    }
}
