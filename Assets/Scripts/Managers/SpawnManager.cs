using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class SpawnManager : NetworkBehaviour {

	public Transform[] spawnPoints;
	
	float[] playersAlive;
	bool player1Dead = false;
	bool movedP1 = false;
	float p1timer = 0;
	bool player2Dead = false;
	bool movedP2 = false;
	float p2timer = 0;
	bool player3Dead = false;
	bool movedP3 = false;
	float p3timer = 0;
	bool player4Dead = false;
	bool movedP4 = false;
	float p4timer = 0;
	int recentSpawn = 0;

	void Start() {
		playersAlive = new float[GameManager.Instance.ActivePlayers.Count];
	}
	
	// Update is called once per frame
	void Update () {
		// if(!isServer)
		// 	return;
		// if(!isLocalPlayer)
		// 	return;

		if(GameManager.Instance.gameStarted) {

			for(int i = 0; i < GameManager.Instance.ActivePlayers.Count; i++) {
				if(GameManager.Instance.ActivePlayers[i].GetComponent<PlayerHealth>().health <= 0) {
					//start timer for that player, turn off that players components, move the player Transform
					switch(i) {
						case 0: 
							player1Dead = true;
							break;
						case 1: 
							player2Dead = true;
							break;
						case 2: 
							player3Dead = true;
							break;
						case 3: 
							player4Dead = true;
							break;
					}
				}




			}

	//		CheckDead();




		}

	}


	void CheckDead() {
//------ PLAYER 1 ------
		if(player1Dead) {
			if(!movedP1) {
				// GameManager.Instance.ActivePlayers[0].SetActive(false);
				// GameManager.Instance.ActivePlayers[0].transform.position = spawnPoints[recentSpawn].position;
				RpcMovePos(0, false);
				recentSpawn++;
				movedP1 = true;
			}

			p1timer += Time.deltaTime;

			if(p1timer > 4) {
				// GameManager.Instance.ActivePlayers[0].GetComponent<PlayerHealth>().health = 2;
				// GameManager.Instance.ActivePlayers[0].SetActive(true);
				RpcMovePos(0, true);
				player1Dead = false;
				p1timer = 0;
				movedP1 = false;
			}

		}
//------ PLAYER 1 ------

//------ PLAYER 2 ------
	if(player2Dead) {
			if(!movedP2) {
				RpcMovePos(1, false);
				// GameManager.Instance.ActivePlayers[1].SetActive(false);
				// GameManager.Instance.ActivePlayers[1].transform.position = spawnPoints[recentSpawn].position;
				recentSpawn++;
				movedP2 = true;
			}

			p2timer += Time.deltaTime;

			if(p2timer > 4) {
				RpcMovePos(1, true);
				// GameManager.Instance.ActivePlayers[1].GetComponent<PlayerHealth>().health = 2;
				// GameManager.Instance.ActivePlayers[1].SetActive(true);
				player2Dead = false;
				movedP2 = false;
				p2timer = 0;
			}

		}
//------ PLAYER 2 ------
	}

	[ClientRpc]
	void RpcMovePos(int plyr, bool setActive) {
		if(setActive) {
			GameManager.Instance.ActivePlayers[plyr].GetComponent<PlayerHealth>().health = 2;
			GameManager.Instance.ActivePlayers[plyr].SetActive(true);
		} else {
			GameManager.Instance.ActivePlayers[plyr].GetComponent<PlayerHealth>().isDead = true;
			GameManager.Instance.ActivePlayers[plyr].SetActive(false);
			GameManager.Instance.ActivePlayers[plyr].transform.position = spawnPoints[recentSpawn].position;
		}
	}
}
