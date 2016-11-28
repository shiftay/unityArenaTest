using UnityEngine;
using UnityEngine.Networking;
using System.Collections.Generic;

public class BufferServerInput : NetworkBehaviour {

	[SyncVar]
	public bool gameStarted = false;
	public bool toggle = false;
	[SyncVar]
	public float startDelay;
	[SyncVar]
	public bool gameOver = false;
	int[] score;
	int[] currentSkin;
	Color[] players;
	// need some shit to set the scores?
	// dictionary of color - int ?


	// Update is called once per frame
	void Update () {

		if(gameStarted && !toggle) {
			startDelay += Time.deltaTime;


			if(startDelay >= 10f) {
				GameManager.Instance.gameStarted = true;
				//gameStarted = false;
				startDelay = 0;
				toggle = true;
			}
		}


		//GameManager.Instance.gameStarted = gameStarted;
	}

	public void EndGame(List<Player> playerList) {
		score = new int[playerList.Count];
		currentSkin = new int[playerList.Count];
		players = new Color[playerList.Count];

		for(int i = 0; i < playerList.Count; i++) {
			score[i] = playerList[i].score;
			players[i] = playerList[i].holder;
		}

		GameManager.Instance.gameStarted = false;
		GameManager.Instance.gameOver = true;

		// set gamestarted to false
		// set gameover to true
		// anything else?
	}
}
