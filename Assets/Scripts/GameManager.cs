using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

	
	public Player player;
	public GameObject[] bots;
	public Transform respawnPoints;
	private Transform[] spawnPoints;
	private static GameManager instance = null;
	public static GameManager Instance {get { return instance;}}
	int lastSpawnPointUsed;
	public bool gameStarted;
	// Use this for initialization
	void Start () {
		if (instance == null)
			instance = this;
		else if (instance != this)
			Destroy(gameObject);
		
		DontDestroyOnLoad(gameObject);
		GetSpawnPoints();
	}
	
	// Update is called once per frame
	void Update () {
		if(gameStarted) {
			for (int i = 0; i < bots.Length; i++) {
				if(bots[i].GetComponent<PlayerHealth>().health <= 0) {
					//respawn the bots
					// turn something on -- make kinematic for timer
					// turn off said timer after X seconds
					lastSpawnPointUsed = Random.Range(0,spawnPoints.Length);
					bots[i].transform.position = spawnPoints[lastSpawnPointUsed].position;
				}
			}
		}

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
