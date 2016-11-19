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
	float camRayLength = 175f;
	Vector3 movement;
	Rigidbody playerRigidbody;
	bool reloading = false;
	public float reloadTimer = 5f;
	public bool reloadTimerStarted = false;
	public float range = 100f;
	LineRenderer gunLine;
	Ray shootRay;
	RaycastHit shotHit;
	float timer;
	public LayerMask things;

	// Use this for initialization
	void Start () {	
		playerRigidbody = GetComponent<Rigidbody>();
		gunLine = GetComponent <LineRenderer> ();
	}
	

	void OnDrawGizmos()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawLine(transform.position + new Vector3(0,4,0), transform.position + transform.forward * range);
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
			timer += Time.deltaTime;
			if(timer > 0.25f)
				gunLine.enabled = false;

			reloadTimer += Time.deltaTime;

			if(reloadTimer > 5f) {
				reloadTimerStarted = false;
				reloading = false;
				reloadTimer = 0;
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

        timer = 0f;

        // gunLight.enabled = true;

        // gunParticles.Stop ();
        // gunParticles.Play ();
		Vector3 offset = new Vector3(0f, 4f,0f);
        gunLine.enabled = true;
        gunLine.SetPosition(0, transform.position + offset);
		gunLine.materials[1].color = Color.cyan;
        shootRay.origin = transform.position + offset;
        shootRay.direction = transform.forward;
		
        if(Physics.Raycast(shootRay, out shotHit, range, things))
        {
        
			PlayerHealth enemy = shotHit.collider.GetComponent<PlayerHealth>();

			if(enemy.health != 0) {
				enemy.health--;
			}

			gunLine.SetPosition (1, shotHit.point);
			// damage players.
     	}
        else
        {
            gunLine.SetPosition (1, shootRay.origin + shootRay.direction * range);
        }

		

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
