using UnityEngine;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class LevelManager : MonoBehaviour
{
    public List<Transform> SpawnPoints;
    public List<GameObject> ShipPrefabs;
    public List<int> PlayerIndexes;

    private struct Team
    {
        public int PlayerAIndex;
        public int PlayerBIndex;
    };

    public delegate void OnRoundFinished(List<int> winners,Dictionary<int, float> levelScores);

    private List<GameObject> _playerShips = new List<GameObject>();
    private OnRoundFinished _winCallback;
    
    public void InitRoundFromGameManager(List<GameObject> shipPrefabs, List<int> playerIndexes, OnRoundFinished winCallback)
    {
        ShipPrefabs = shipPrefabs;
        PlayerIndexes = playerIndexes;
        _winCallback = winCallback;

        _playerShips = SpawnShips();
    }

    void Start()
    {
        _playerShips = SpawnShips();
    }

    private List<GameObject> SpawnShips()
    {
        if (PlayerIndexes.Count % 2 != 0)
        {
            Debug.LogError("Uneven player count!");
        }
        int shipCount = PlayerIndexes.Count / 2;
        
        List<GameObject> shuffledShips = ShipPrefabs.OrderBy(i => UnityEngine.Random.value).ToList();
        List<Transform> shuffledSpawnPoints = SpawnPoints.OrderBy(i => UnityEngine.Random.value).ToList();
        List<int> shuffledPlayerIndexes = PlayerIndexes.OrderBy(i => UnityEngine.Random.value).ToList();
        var shipTeams = new List<Team>();
        for (var i = 0; i < PlayerIndexes.Count; i += 2)
        {
            shipTeams.Add(new Team {
                PlayerAIndex = shuffledPlayerIndexes [i],
                PlayerBIndex = shuffledPlayerIndexes [i + 1]
            });
        }
        
        return Enumerable.Range(0, shipCount).Select(shipIndex => {
            Transform spawnPoint = shuffledSpawnPoints [shipIndex];
            GameObject shipPrefab = shuffledShips [shipIndex % shuffledShips.Count];
            Team team = shipTeams [shipIndex];
            
            var newShip = (GameObject)Instantiate(shipPrefab, spawnPoint.position, spawnPoint.rotation);
            
            var teamComponent = newShip.GetComponentInChildren<TeamControlledShipComponent>();
            teamComponent.PlayerAIndex = team.PlayerAIndex;
            teamComponent.PlayerBIndex = team.PlayerBIndex;
            teamComponent.Sync();
            Debug.Log(String.Format("Players {0} and {1} are together.", team.PlayerAIndex, team.PlayerBIndex));
            
            return newShip;
        }).ToList();
    }

    void FixedUpdate()
    {
        _playerShips.RemoveAll(item => item == null);
        if (_playerShips.Count == 1)
        {
            GameObject remainingShip = _playerShips.First();
            float remainingShipHealth = remainingShip.GetComponentInChildren<HealthBehavior>().CurrentHealth;

            var teamComponent = remainingShip.GetComponentInChildren<TeamControlledShipComponent>();
            List<int> remainingPlayers = new List<int>() {
                teamComponent.PlayerAIndex,
                teamComponent.PlayerBIndex
            };

            CallbackScore(remainingPlayers, remainingShipHealth);
        }
    }

    private void CallbackScore(List<int> remainingPlayers, float remainingShipHealth)
    {
        var scores = new Dictionary<int, float>();
        foreach (var playerIndex in remainingPlayers)
        {
            if (scores.ContainsKey(playerIndex)) {
                float lastScore = scores[playerIndex];
                scores.Remove(playerIndex);
                scores.Add(playerIndex, lastScore + remainingShipHealth);
            } else {
                scores.Add(playerIndex, remainingShipHealth);
            }
        }

        if (_winCallback != null) {
            _winCallback(remainingPlayers, scores);
        }
    }
}
