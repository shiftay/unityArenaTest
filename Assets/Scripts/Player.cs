using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	// TO DO: 
	// add firing of weapon
	//		-> tagging the bullet as X to determine who kills who
	// powerups
	// dying / respawning 
	public float mSpeed;
	public LayerMask floorMask;
	float camRayLength = 175f;
	Vector3 movement;
	Rigidbody playerRigidbody;
	bool reloading = false;

	float reloadTimer;
	bool reloadTimerStarted = false;
	
	// Use this for initialization
	void Start () {	
		playerRigidbody = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
	}

	void FixedUpdate() {
//--------- Shooting ---------
		if(reloadTimerStarted) {
			reloadTimer += Time.deltaTime;

			if(reloadTimer > 5f) {
				reloadTimerStarted = false;
				reloading = false;
				reloadTimer = 0;
			}
		}
		if(!reloading && Input.GetButtonDown("Fire1")) {
			// fire bullet @ currentposition 
			// tag bullet as which character.
			// relative to transform.forward.
			// set reload to true
			// start a timer
			// activate a slider gameobject.
		}
//--------- Shooting ---------

//--------- Movement ---------
		float v = Input.GetAxis("Vertical");
		float h = Input.GetAxis("Horizontal");


		Move(v,h);
		Turning();
//--------- Movement ---------

	}

	void Move(float v, float h) {
		Vector3 right = transform.right * h;
		Vector3 ahead = transform.forward * v;
		movement = right + ahead;

		// movement.Set(h ,0f, v);

		movement = movement.normalized * mSpeed * Time.deltaTime;
		playerRigidbody.MovePosition(transform.position + movement);

	}

	void Turning() {
        Ray camRay = Camera.main.ScreenPointToRay (Input.mousePosition);
        RaycastHit floorHit;

        if(Physics.Raycast (camRay, out floorHit, camRayLength, floorMask))
        {
        	Vector3 playerToMouse = floorHit.point - transform.position;
        	playerToMouse.y = 0f;
            Quaternion newRotation = Quaternion.LookRotation (playerToMouse);
            playerRigidbody.MoveRotation(newRotation);
        }
    }



	void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.tag == "Powerup") {
			// check script attached to it, to determine what to do
		}
	}



}
