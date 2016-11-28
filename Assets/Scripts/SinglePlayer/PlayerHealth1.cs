using UnityEngine;
using System.Collections;

public class PlayerHealth1 : MonoBehaviour {

	public int healthPoints = 2;
	public bool isDead = false;
	public float timeToRespawn = 5f;
	public float respawnTimer = 0;
	
	// Update is called once per frame
	void Update () {
		if(!isDead) {
			if(healthPoints == 0) {
				isDead = true;
			}
		} else {
			respawnTimer += Time.deltaTime;

			if(respawnTimer >= timeToRespawn) {
				isDead = false;
				healthPoints = 2;
				respawnTimer = 0;
			}
			//dead so they need to respawn;
		}
	}

	public void TakeDmg() {
		healthPoints--;
	}
}
