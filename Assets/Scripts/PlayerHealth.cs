using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class PlayerHealth : NetworkBehaviour {

	[SyncVar]
	public int health = 2;
	[SyncVar]
	public int score = 0;
	public bool isDead = false;
	public float timeToRespawn = 5f;
	public float respawnTimer = 0;
	
	// Update is called once per frame
	void Update () {
		// if(!isDead) {
		// 	if(health == 0) {
		// 		isDead = true;
		// 	}
		// } else {
		// 	respawnTimer += Time.deltaTime;

		// 	if(respawnTimer >= timeToRespawn) {
		// 		isDead = false;
		// 		health = 2;
		// 		respawnTimer = 0;
		// 	}
		// 	//dead so they need to respawn;
		// }
	}

	public void TakeDmg(bool dd) {

		if(!isServer) 
			return;

		if(dd)
			health -= 2;
		else
			health--;
	}

	public void Reset() {
		if(!isServer)
			return;

		health = 2;
	}
}
