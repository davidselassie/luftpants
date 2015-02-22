using UnityEngine;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class ScoreMaster : MonoBehaviour {
    public List<Transform> spawnPoints;
    public List<GameObject> shipPrefabs;
    public int playerCount;
    
    public GameState gameState;
    
    public struct Team {
        public int PlayerA;
        public int PlayerB;
        
        public Team(int a, int b) {
            this.PlayerA = a;
            this.PlayerB = b;
        }
    };
    
    private List<GameObject> playerShips = new List<GameObject>();
    
    void Start () {
        if(gameState == null)
            gameState = GetComponent<GameState> ();
    }
    
    public List<GameObject> SpawnShips(){
        List<int> PlayerIndexes = Enumerable.Range(0, this.playerCount).ToList();
        if (PlayerIndexes.Count % 2 != 0)
        {
            Debug.LogError("Uneven player count!");
        }
        int shipCount = PlayerIndexes.Count / 2;
        
        List<GameObject> shuffledShips = shipPrefabs.OrderBy(i => UnityEngine.Random.value).ToList();
        List<Transform> shuffledSpawnPoints = spawnPoints.OrderBy(i => UnityEngine.Random.value).ToList();
        List<int> shuffledPlayerIndexes = PlayerIndexes.OrderBy(i => UnityEngine.Random.value).ToList();
        var shipTeams = new List<Team>();
        for (var i = 0; i < PlayerIndexes.Count; i += 2)
        {
            shipTeams.Add(new Team {
                PlayerA = shuffledPlayerIndexes [i],
                PlayerB = shuffledPlayerIndexes [i + 1]
            });
        }
        
        return Enumerable.Range(0, shipCount - 1).Select(shipIndex => {
            Transform spawnPoint = shuffledSpawnPoints [shipIndex];
            GameObject shipPrefab = shuffledShips [shipIndex % shuffledShips.Count];
            Team team = shipTeams [shipIndex];
            
            var newShip = (GameObject)Instantiate(shipPrefab, spawnPoint.position, spawnPoint.rotation);
            
            var teamComponent = newShip.GetComponentInChildren<TeamControlledShipComponent>();
            teamComponent.PlayerAIndex = team.PlayerA;
            teamComponent.PlayerBIndex = team.PlayerB;
            teamComponent.Sync();
            Debug.Log(String.Format("Players {0} and {1} are together.", team.PlayerA, team.PlayerB));
            
            return newShip;
        }).ToList();
//        System.Diagnostics.Debug.Assert(this.playerCount % 2 == 0);
//        int shipCount = this.playerCount / 2;
//        System.Diagnostics.Debug.Assert(this.spawnPoints.Count >= shipCount);
//        System.Diagnostics.Debug.Assert(this.shipPrefabs.Count >= shipCount);
//        
//        List<GameObject> shuffledShips = this.shipPrefabs.OrderBy(i => UnityEngine.Random.value).ToList();
//        List<int> shuffledIndex = Enumerable.Range(0, this.playerCount).OrderBy(i => UnityEngine.Random.value).ToList();
//        List<Team> shipTeams = new List<Team>();
//        for (int i = 0; i < this.playerCount; i += 2) {
//            shipTeams.Add(new Team(shuffledIndex[i], shuffledIndex[i + 1]));
//        }
//        System.Diagnostics.Debug.Assert(shipTeams.Count == shipCount);
//        
//        this.playerShips = Enumerable.Range(0, shipCount).Select(shipIndex => {
//            Transform spawnPoint = this.spawnPoints[shipIndex];
//            GameObject shipPrefab = shuffledShips[shipIndex];
//            Team team = shipTeams[shipIndex];
//            
//            GameObject newShip = (GameObject) Instantiate(shipPrefab, spawnPoint.position, spawnPoint.rotation);
//            
//            List<APlayerControlledComponent> playerComponents = newShip.GetComponentsInChildren<APlayerControlledComponent>().ToList();
//            System.Diagnostics.Debug.Assert(playerComponents.Count == 2);
//            playerComponents[0].PlayerIndex = team.PlayerA;
//            playerComponents[1].PlayerIndex = team.PlayerB;
//            Debug.Log(String.Format("Players {0} and {1} are together.", team.PlayerA, team.PlayerB));
//            
//            return newShip;
//        }).ToList();
    }
    
    
    void Update () {
        playerShips.RemoveAll(item => item == null);
        if (this.playerShips.Count == 1) {
            GameObject remainingShip = playerShips.First();
            float remainingShipHealth = remainingShip.GetComponentInChildren<HealthBehavior>().CurrentHealth;
            
            List<APlayerControlledComponent> playerComponents = remainingShip.GetComponentsInChildren<APlayerControlledComponent>().ToList();
            System.Diagnostics.Debug.Assert(playerComponents.Count == 2);
            List<int> players = playerComponents.Select(pc => pc.PlayerIndex).ToList();
            
            ReportScores(players, remainingShipHealth);
        }
    }
    
    void ReportScores(List<int> players, float score){
        float[] scores = new float[4];
        for (int i=0; i<scores.Length; i++) {
            scores[i] = 0f;
        }
        scores[players.First()] = score;
        scores[players.Last()] = score;
        
        gameState.RoundFinished (scores);
    }
    
    public void Clear(){
        foreach (GameObject ship in playerShips) {
            Destroy(ship);
        }
        playerShips = new List<GameObject>();
    }
    
    public void Begin(){
        spawnPoints = GameObject.FindGameObjectsWithTag("Respawn").Select(x => x.transform).ToList();
        Debug.Log("spawnPoints.Count:" + spawnPoints.Count);
        Debug.Log("find.Count:" + (GameObject.FindGameObjectsWithTag("Respawn").Length));
        SpawnShips ();
    }
}