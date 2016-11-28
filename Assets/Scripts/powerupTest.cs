using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using System.Collections;

public class powerupTest : NetworkBehaviour {
	public enum PowerUPS {
		HEALTH,
		DAMAGE,
		SPEED,
		WEAPON
	}
	[SyncVar]
	public PowerUPS type;

	
	// Use this for initialization
	void Start () {
		if(!isServer)
			return;

		switch(Random.Range(0,4)) {
			case 0:
				type = powerupTest.PowerUPS.HEALTH;
				break;
			case 1:
				type = powerupTest.PowerUPS.HEALTH;
				break;
			case 2:
				type = powerupTest.PowerUPS.SPEED;
				GetComponent<MeshRenderer>().materials[0].color = Color.yellow;
				break;
			case 3:
				type = powerupTest.PowerUPS.DAMAGE;
				GetComponent<MeshRenderer>().materials[0].color = Color.blue;
				break;
		}
	}
	
	// Update is called once per frame
	void Update () {

		switch(type) {
			case powerupTest.PowerUPS.HEALTH:
				GetComponent<MeshRenderer>().materials[0].color = Color.red;
				break;
			case powerupTest.PowerUPS.DAMAGE:
				GetComponent<MeshRenderer>().materials[0].color = Color.blue;
				break;
			case powerupTest.PowerUPS.SPEED:
				GetComponent<MeshRenderer>().materials[0].color = Color.yellow;
				break;
		}
	}

	void OnDestroy()
	{
		if(!isServer)
			return;
			
		if(SceneManager.GetActiveScene().name.Contains("Arena")) {
			GameObject.Find("BalloonSpawns").GetComponent<BalloonManager>().balloonsAlive--;
		}
	}
}
