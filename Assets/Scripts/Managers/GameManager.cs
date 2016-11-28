using UnityEngine;
using UnityEngine.SceneManagement;
//using UnityEngine.Networking;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

	public Color LaserCol;
	public int recentSpawn = 0;
	public int[] scores = new int[4];
	public Player player;
	public GameObject[] PlayerPF;
	bool respawnPointsGathered = false;
	private Transform[] spawnPoints;
	private static GameManager instance = null;
	public static GameManager Instance {get { return instance;}}
	public string test;
	public bool gameStarted;
	public int skinChoice;
	public bool singleplayer = false;
	List<GameObject> activePlayers = new List<GameObject>();
	public List<GameObject> ActivePlayers {get { return activePlayers;}}
	public float gameTimer;
	public bool gameOver = false;
	// Use this for initialization
	void Start () {
		if (instance == null)
			instance = this;
		else if (instance != this)
			Destroy(gameObject);
		
		DontDestroyOnLoad(gameObject);
	//	GetSpawnPoints();
	}
	
	// Update is called once per frame
	void Update () {

		if(SceneManager.GetActiveScene().name.Contains("Arena") && !respawnPointsGathered) {
			
			respawnPointsGathered = true;
			if(!gameStarted && singleplayer) {
				Instantiate(PlayerPF[skinChoice], spawnPoints[0].position, Quaternion.identity);
				// spawn some bots
				// AddPlayer(); // instantiate as you add ??
				// freeze everyone <kinematic>
				// 
				gameStarted = true;
			}

		}


		if(gameStarted) {
			gameTimer += Time.deltaTime;

			if(gameTimer >= 120f) {
				// gameover 
				gameOver = true;	
			}

		}
		
		if(gameOver) {
			// freeze all players

		}

	}



	public void AddPlayer(GameObject dude) {
		activePlayers.Add(dude);
	}

}
