using UnityEngine;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class ScoreMaster : MonoBehaviour {
	public List<Transform> spawnPoints;
	public List<GameObject> shipPrefabs;
	public int playerCount;

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
        //SpawnShips ();
    }

    void SpawnShips(){
        System.Diagnostics.Debug.Assert(this.playerCount % 2 == 0);
        int shipCount = this.playerCount / 2;
        System.Diagnostics.Debug.Assert(this.spawnPoints.Count >= shipCount);
        System.Diagnostics.Debug.Assert(this.shipPrefabs.Count >= shipCount);
        
        List<GameObject> shuffledShips = this.shipPrefabs.OrderBy(i => UnityEngine.Random.value).ToList();
        List<int> shuffledIndex = Enumerable.Range(0, this.playerCount).OrderBy(i => UnityEngine.Random.value).ToList();
        List<Team> shipTeams = new List<Team>();
        for (int i = 0; i < this.playerCount; i += 2) {
            shipTeams.Add(new Team(shuffledIndex[i], shuffledIndex[i + 1]));
        }
        System.Diagnostics.Debug.Assert(shipTeams.Count == shipCount);
        
        this.playerShips = Enumerable.Range(0, shipCount).Select(shipIndex => {
            Transform spawnPoint = this.spawnPoints[shipIndex];
            GameObject shipPrefab = shuffledShips[shipIndex];
            Team team = shipTeams[shipIndex];
            
            GameObject newShip = (GameObject) Instantiate(shipPrefab, spawnPoint.position, spawnPoint.rotation);
            
            List<APlayerControlledComponent> playerComponents = newShip.GetComponentsInChildren<APlayerControlledComponent>().ToList();
            System.Diagnostics.Debug.Assert(playerComponents.Count == 2);
            playerComponents[0].Player = team.PlayerA;
            playerComponents[1].Player = team.PlayerB;
            Debug.Log(String.Format("Players {0} and {1} are together.", team.PlayerA, team.PlayerB));
            
            return newShip;
        }).ToList();
    }
	

	void UpdateFixed () {
		if (this.playerShips.Count <= 1) {
            GameObject remainingShip = playerShips.First();
            float remainingShipHealth = remainingShip.GetComponentInChildren<HealthComponent>().health;

			List<APlayerControlledComponent> playerComponents = remainingShip.GetComponentsInChildren<APlayerControlledComponent>().ToList();
			System.Diagnostics.Debug.Assert(playerComponents.Count == 2);
			List<int> players = playerComponents.Select(pc => pc.Player).ToList();

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

        GameState gameState = GetComponent<GameState> ();
        gameState.RoundFinished (scores);
    }

    public void Clear(){
        foreach (GameObject ship in playerShips) {
            Destroy(ship);
        }
        playerShips = new List<GameObject>();
    }

    public void Begin(){
        SpawnShips ();
    }
}
