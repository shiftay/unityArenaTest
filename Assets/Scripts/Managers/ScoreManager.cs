using UnityEngine;
using UnityEngine.Networking;
using System.Collections.Generic;

public class ScoreManager : NetworkBehaviour {

	private static ScoreManager instance = null;
	public static ScoreManager Instance { get { return instance;}}
	bool started = false;
	float gameTimer = 0;
	public int amountOfPlayers = 2;
	List<Player> playerList = new List<Player>();
	// Use this for initialization
	void Start () {
		if (instance == null)
			instance = this;
		else if (instance != this)
			Destroy(gameObject);
		
		DontDestroyOnLoad(gameObject);
	}
	
	// Update is called once per frame
	void Update () {
		if(!isServer)
			return;

		if(playerList.Count >= amountOfPlayers && !started) {
			RpcStartGame();
			started = true;
		}

		if(started)
			gameTimer += Time.deltaTime;

		if(gameTimer >= 120f) {
			RpcEndGame();
		}

	}


	[ClientRpc]
	void RpcStartGame() {
		// if(isLocalPlayer)
			GameObject.Find("helloWorl").GetComponent<BufferServerInput>().gameStarted = true;
	}

	[ClientRpc]
	void RpcEndGame() {
		// if(isLocalPlayer)
			GameObject.Find("helloWorl").GetComponent<BufferServerInput>().gameStarted = false;
	}

	public void AddPlayer(Player player) {
		playerList.Add(player);
	}
}
