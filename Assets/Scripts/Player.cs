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
	public float cooldown;
	public float reloadTimer = 0f;
	public bool reloadTimerStarted = false;
	public float range = 100f;
	LineRenderer gunLine;
	Ray shootRay;
	RaycastHit shotHit;
	float timer;
	public LayerMask things;
	Animator anim;
	public int score;
	PlayerHealth health;

//--------- powerup Area ---------
	bool dblDmg;
	public bool DDMG { get {return dblDmg;}}
	bool dblSpeed;
	public bool DSPD { get { return dblSpeed;}}
	float powerupTimerSpd;
	float powerTimerDmg;

//--------- powerup Area ---------

	// Use this for initialization
	void Start () {	
		anim = GetComponent<Animator>();
		playerRigidbody = GetComponent<Rigidbody>();
		gunLine = GetComponent<LineRenderer>();
		health = GetComponent<PlayerHealth>();
	}
	
	void OnDrawGizmos()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawLine(transform.position + new Vector3(0,3,0), transform.position + transform.forward * range);
	}
	// Update is called once per frame
	void Update () {
//----- Powerups -----
		if(dblDmg) {
			powerTimerDmg += Time.deltaTime;

			if(powerTimerDmg >= 6f) {
				dblDmg = false;
				powerTimerDmg = 0;
			}
		}

		if(dblSpeed) {
			powerupTimerSpd += Time.deltaTime;

			if(powerupTimerSpd >= 10f) {
				dblSpeed = false;
				powerupTimerSpd = 0;
			}
		}

//----- Powerups -----
	}

	void FixedUpdate() {
		anim.SetBool("isWalking", false); // reset bool for walking
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

			if(reloadTimer > cooldown) {
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
		if(v != 0 || h != 0)
			anim.SetBool("isWalking", true);
		// movement.Set(h ,0f, v);
		if(!dblSpeed)
			movement = movement.normalized * mSpeed * Time.deltaTime;
		else
			movement = movement.normalized * (mSpeed * 2) * Time.deltaTime;

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
		Vector3 offset = new Vector3(0f, 3f,0f);
        gunLine.enabled = true;
        gunLine.SetPosition(0, transform.position + offset);
		gunLine.materials[1].color = Color.cyan;
        shootRay.origin = transform.position + offset;
        shootRay.direction = transform.forward;
		
        if(Physics.Raycast(shootRay, out shotHit, range))
        {
				if(shotHit.collider.tag == "Player") { 			// damage players.
				PlayerHealth enemy = shotHit.collider.GetComponent<PlayerHealth>();

				if(enemy.health != 0) {
					if(!dblDmg)
						enemy.health--;
					else 
						enemy.health -= 2;
					if(enemy.health <= 0) {
						score++;
					}
				}
			}
			gunLine.SetPosition (1, shotHit.point);

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
					if(health.health != 2)
						health.health++;
					 break;
				case powerupTest.PowerUPS.DAMAGE:
					dblDmg = true;
				 break;
				case powerupTest.PowerUPS.SPEED:
					dblSpeed = true;
				 break;
				case powerupTest.PowerUPS.WEAPON:
				 break;
			}

			Destroy(other.gameObject);
		}
	}

	void OnCollisionEnter(Collision other) {
		// getting hit.
	}
}