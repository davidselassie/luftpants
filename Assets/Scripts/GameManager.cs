using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class GameManager : MonoBehaviour
{
    public GameObject UI;
    public GameObject Logo;
    public float LogoDisplaySeconds = 1f;
    public GameObject Instructions;
    public float InstructionDisplaySeconds = 1f;
    public GameObject ScoreDisplay;
    public float ScoreDisplaySeconds = 1f;
    public GameObject Credits;

    public List<string> Scenes;
    public List<int> PlayerIndexes;
    public List<GameObject> ShipPrefabs;
    public int Rounds = 3;

    private int _currentRound = -1;
    private Dictionary<int, float> _scores = new Dictionary<int, float>();

    public enum Phase
    {
        LOGO,
        INSTRUCTIONS,
        PLAY,
        SHOW_SCORES,
        LOADING,
        SHOW_FINAL_SCORES,
        CREDITS
    }

    private Phase _currentPhase = Phase.LOGO;

    private float _creditsChangeTime;
    private float _instructionChangeTime;
    private float _resultsChangeTime;

    void Start()
    {
        RenderImmortal();
    }
    
    private void RenderImmortal()
    {
        DontDestroyOnLoad(UI);
        DontDestroyOnLoad(Credits);
        DontDestroyOnLoad(Logo);
        DontDestroyOnLoad(Instructions);
        DontDestroyOnLoad(ScoreDisplay);
        
        DontDestroyOnLoad(this);
    }

//    void FixedUpdate()
//    {
//        if (Input.anyKeyDown)
//        {
//            AdvanceStateMachine();
//        }
//    }
//
//    private bool ActivatePhase(Phase newPhase)
//    {
//        switch (this.currentPhase)
//        {
//            case Phase.LOGO:
//                if (Time.time > LogoTime)
//                {
//                    this.currentPhase = Phase.INSTRUCTIONS;
//                    Logo.SetActive(false);
//                    InstructionChangeTime = Time.time + InstructionTime;
//                    Instructions.SetActive(true);
//                }   
//                break;
//            case Phase.INSTRUCTIONS:
//                if (Time.time > InstructionChangeTime)
//                {
//                    Instructions.SetActive(false);
//
//                    CurrentPhase = Phase.LOADING;
//                    StartNewRound();
//                }
//                break;
//            case Phase.RESULTS:
//                if (Time.time > ResultsChangeTime)
//                {
//                    if (roundsFinished == NumberOfRounds)
//                    {
//                        ResultsChangeTime = Time.time + ResultsTime;
//                        CurrentPhase = Phase.FINAL_RESULTS;
//
//                        string finalResultText = RenderFinalResult();
//                        Text resultTextBox = Results.GetComponentInChildren<Text>();
//                        resultTextBox.text = finalResultText;
//                    } else
//                    {
//                        Results.SetActive(false);
//                        CurrentPhase = Phases.LOADING;
//                        StartNewRound();
//                    }
//                }
//                break;
//            case Phase.LOADING:
//                CurrentPhase = Phases.PLAY;
//                break;
//            case Phase.FINAL_RESULTS:
//                if (Time.time > ResultsChangeTime)
//                {
//                    Results.SetActive(false);
//                    Credits.SetActive(true);
//                    CurrentPhase = Phase.CREDITS;
//                    CreditsChangeTime = Time.time + LogoTime;
//                }   
//                break;
//            case Phase.CREDITS:
//                if (Time.time > CreditsChangeTime)
//                {
//                    CurrentPhase = Phase.INSTRUCTIONS;
//                    Credits.SetActive(false);
//                    InstructionChangeTime = Time.time + InstructionTime;
//                    Instructions.SetActive(true);
//                }   
//                break;
//        }
//
//    }

//    public void StartNewRound()
//    {
//        _currentRound++;
//        Application.LoadLevel(Scenes [_currentRound % Scenes.Count]);
//        var currentLevelManager = FindObjectOfType<LevelManager>();
//        currentLevelManager.InitRoundFromGameManager(
//            ShipPrefabs,
//            PlayerIndexes,
//            new LevelManager.OnRoundFinished(RoundFinished));
//    }
//    
//    public void RoundFinished(List<int> winners, Dictionary<int, float> roundScores)
//    {
//        var resultTextBox = ScoreDisplay.GetComponentInChildren<Text>();
//        resultTextBox.text = RenderRoundScore(winners, roundScores);
//        ActivatePhase(Phase.SHOW_SCORES);
//
//        _scores = AddScores(_scores, roundScores);
//    }

    private static Dictionary<int, float> AddScores(Dictionary<int, float> one, Dictionary<int, float> two)
    {
        var sum = new Dictionary<int, float>(one);
        foreach (var pair in two)
        {
            sum.Add(pair.Key, sum[pair.Key] + pair.Value);
        }
        return sum;
    }

    private string RenderRoundScore(List<int> winners, Dictionary<int, float> roundScores)
    {
        return string.Format(
            "Player {0} and Player {1} survived the round with {2} health remaining on their ship.",
            winners [0] + 1,
            winners [1] + 1,
            roundScores [winners [0]]);
    }

    private string RenderFinalScore()
    {
        KeyValuePair<int, float> winner = _scores.OrderByDescending(pair => pair.Key).First();
        return string.Format(
            "Player {0} has emerged victorious with a whopping final score of {1}.\n\nThey alone answered correctly\nWHAT DO WE DO NOW?",
            winner.Key + 1,
            winner.Value);
    }

}
