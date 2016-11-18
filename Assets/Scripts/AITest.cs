using UnityEngine;
using System.Collections;

public class AITest : SteeringBehaviour {

	public float playerTrackingRad = 20f;
	public float trackCloseObjects = 10f;
	public LayerMask wallsEtc;
	public LayerMask combatants;
	bool reloading;
	// Update is called once per frame
	void Update () {
		Collider[] holder = Physics.OverlapSphere(transform.position, trackCloseObjects, wallsEtc);
		if(holder.Length > 1) {
			for(int i = 0; i < holder.Length; i++) {
				// if the object is a hill / bridge or something attempt to evade it
				// break;
			}
		}

		// check for players.
		// check if okay to shoot
		// check if reloading
		// shoot towards player, rotate body to make it not look awkward





		Wander();
		ApplySteering();



	}
}
