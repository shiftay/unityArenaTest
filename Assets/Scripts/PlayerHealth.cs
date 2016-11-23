using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour {

	public int health = 2;
	public bool isDead = false;
	public float timeToRespawn = 5f;
	public float respawnTimer = 0;
	
	// Update is called once per frame
	void Update () {
		if(!isDead) {
			if(health == 0) {
				isDead = true;
			}
		} else {
			respawnTimer += Time.deltaTime;

			if(respawnTimer >= timeToRespawn) {
				isDead = false;
				health = 2;
				respawnTimer = 0;
			}
			//dead so they need to respawn;
		}
	}
}
