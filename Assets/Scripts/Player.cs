using UnityEngine;
// using UnityEngine.UI;
using System.Collections;

public class Player : MonoBehaviour {
	// TO DO: 
	// add firing of weapon
	//		-> tagging the bullet as X to determine who kills who
	// powerups
	// dying / respawning 
	public float mSpeed;
	public LayerMask floorMask;
	//public Slider reloadSlider;
	float camRayLength = 175f;
	Vector3 movement;
	Rigidbody playerRigidbody;
	bool reloading = false;
	public float reloadTimer = 5f;
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
			// if(reloadSlider.IsActive()) {
			// 	reloadSlider.maxValue = 5;
			// 	reloadSlider.value = reloadTimer;
			// 	// can add color, etc...
			// }
			reloadTimer -= Time.deltaTime;

			if(reloadTimer < 0f) {
				reloadTimerStarted = false;
				reloading = false;
				reloadTimer = 5;
				// deactivate slider
			}
		}
		if(!reloading && Input.GetButtonDown("Fire1")) {
			// fire bullet @ currentposition 
			Shoot();
			

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


	void Shoot() {
		// tag bullet as which character.
		// relative to transform.forward.

		// ripped from SMB3
		// GameObject go = (GameObject) Instantiate(m_FireBall, transform.position + (offset * transform.forward.normalized), Quaternion.identity);
   		// go.GetComponent<Rigidbody2D>().velocity = new Vector3();
		reloading = true;
		reloadTimerStarted = true;
	}
	void OnTriggerEnter(Collider other)	{
		if(other.gameObject.tag == "Powerup") {
			// determine what to do
			switch(other.gameObject.GetComponent<powerupTest>().type) {
				case powerupTest.PowerUPS.HEALTH:
				 break;
				case powerupTest.PowerUPS.DAMAGE:
				 break;
				case powerupTest.PowerUPS.SPEED:
				 break;
				case powerupTest.PowerUPS.WEAPON:
				 break;
			}
		}
	}

	void OnCollisionEnter(Collision other) {
		// getting hit.
	}


}
