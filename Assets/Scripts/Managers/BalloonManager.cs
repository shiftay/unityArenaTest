using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

public class BalloonManager : NetworkBehaviour {

	public GameObject balloonPF;
	public Transform respawnPoints;
	Transform[] spawnPoints;
	bool gatheredSpawns = false;
	int recentSpawn = 0;
	public int balloonsAlive;
	public int balloonsAllowed = 2;
	
	// Update is called once per frame
	void Update () {
		if(!isServer)
			return;

		if(SceneManager.GetActiveScene().name.Contains("Arena") && !gatheredSpawns) {
			GetSpawnPoints();
			gatheredSpawns = true;
		}

		if(balloonsAlive < balloonsAllowed) {

			GameObject balloon = (GameObject)Instantiate(balloonPF, spawnPoints[recentSpawn].position, Quaternion.identity);
			NetworkServer.Spawn(balloon);
			balloonsAlive++;
			recentSpawn++;
			if(recentSpawn >= spawnPoints.Length) {
				recentSpawn = 0;
			}
		}
		// set a pool # of balloons
		// make this a singleton
		// spawn based off of pool.
	}

	void GetSpawnPoints() {
		//NOTE: Unity named this function poorly it also returns the parent’s component.
		Transform[] potentialWaypoints = respawnPoints.GetComponentsInChildren<Transform>();
		
		//initialize the waypoints array so that is has enough space to store the nodes.
		spawnPoints = new Transform[ (potentialWaypoints.Length - 1) ];
		
		//loop through the list and copy the nodes into the array.
		//start at 1 instead of 0 to skip the WaypointContainer’s transform.
		for (int i = 1; i < potentialWaypoints.Length; ++i ) 
		{
			spawnPoints[ i-1 ] = potentialWaypoints[i];
		}
	}
}
