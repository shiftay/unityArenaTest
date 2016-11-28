using UnityEngine;
// using UnityEngine.UI;
// using UnityEngine.Networking;
using System.Collections;

public class Player1 : MonoBehaviour {
	// TO DO: 
	// add firing of weapon
	//		-> tagging the bullet as X to determine who kills who
	// powerups
	// dying / respawning 
	
	public int health = 2;
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
	public float timer;
	public LayerMask things;
	Animator anim;
	public int score;
//--------- powerup Area ---------

	public bool dblDmg;

	public bool dblSpeed;
	float powerupTimerSpd;
	float powerTimerDmg;
//--------- powerup Area ---------

	// Use this for initialization
	void Start () {	
		anim = GetComponent<Animator>();
		playerRigidbody = GetComponent<Rigidbody>();
		gunLine = GetComponent<LineRenderer>();
		Camera.main.GetComponent<CameraManager>().player = transform;
		// if(isLocalPlayer) {
		// 	Camera.main.GetComponent<CameraManager>().player = transform;
		// 	GameObject.Find("Canvas").GetComponent<UIScript>().player = this.gameObject;
		// 	GameManager.Instance.AddPlayer(this);
		// }
	}
	
	void OnDrawGizmos()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawLine(transform.position + new Vector3(0,3,0), transform.position + transform.forward * range);
	}
	// Update is called once per frame
	void Update () {

	}

	void FixedUpdate() {
		// if(!isLocalPlayer) {
		// 	return;
		// }

		anim.SetBool("isWalking", false); // reset bool for walking


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



//--------- Shooting ---------
		if(reloadTimerStarted) {
			// if(reloadSlider.IsActive()) {
			// 	reloadSlider.maxValue = 5;
			// 	reloadSlider.value = reloadTimer;
			// 	// can add color, etc...
			// }
			timer += Time.deltaTime;
			if(timer > 0.25f) {
				gunLine.enabled = false;
				// CmdTurnLazerOff();
			}

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
			 CmdShoot();
				// LazerShooter.Instance.Shoot(transform.position, transform.forward, Color.cyan, number);
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

	// [CommandAttribute]
	// void CmdTurnLazerOff() {
	// 	gunLine.enabled = false;
	// }
	// [CommandAttribute]
	void CmdShoot() {
		// tag bullet as which character.
		// relative to transform.forward.
        timer = 0f;

        // gunLight.enabled = true;s
        // gunParticles.Stop ();
        // gunParticles.Play ();
		Vector3 offset = new Vector3(0f, 3f,0f);
        gunLine.enabled = true;
        gunLine.SetPosition(0, transform.position + offset);
		gunLine.materials[0].color = GameManager.Instance.LaserCol;
		gunLine.materials[0].shader = Shader.Find("Unlit/Color");
        shootRay.origin = transform.position + offset;
        shootRay.direction = transform.forward;
		
        if(Physics.Raycast(shootRay, out shotHit, range))
        {
			if(shotHit.collider.tag == "Player") { 			// damage players.
				PlayerHealth enemy = shotHit.collider.GetComponent<PlayerHealth>();
				enemy.TakeDmg(dblDmg);
				if(enemy.health <= 0) {
					score++;
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
		// Rpcshot();
	}

	// [ClientRpc]
	// void Rpcshot() {
	// 	timer = 0f;
	// 	Vector3 offset = new Vector3(0f, 3f,0f);
    //     gunLine.enabled = true;
    //     gunLine.SetPosition(0, transform.position + offset);
	// 	gunLine.materials[0].color = Color.cyan;
	// 	gunLine.materials[0].shader = Shader.Find("Unlit/Color");
    //     shootRay.origin = transform.position + offset;
    //     shootRay.direction = transform.forward;

	// 	if(Physics.Raycast(shootRay, out shotHit, range))
    //     {
	// 		// if(shotHit.collider.tag == "Player") { 			// damage players.
	// 		// 	PlayerHealth enemy = shotHit.collider.GetComponent<PlayerHealth>();
	// 		// 	enemy.TakeDmg();
	// 		// 	if(enemy.health <= 0) {
	// 		// 		score++;
	// 		// 	}
	// 		// }
	// 		gunLine.SetPosition (1, shotHit.point);
    //  	}
    //     else
    //     {
    //         gunLine.SetPosition (1, shootRay.origin + shootRay.direction * range);
    //     }


	// 	reloading = true;
	// 	reloadTimerStarted = true;
	// }


	void OnTriggerEnter(Collider other)	{
		if(other.gameObject.tag == "Powerup") {
			// determine what to do
			CmdPowerUp(other.gameObject.GetComponent<powerupTest>().type, other.gameObject);
			// switch(other.gameObject.GetComponent<powerupTest>().type) {
			// 	case powerupTest.PowerUPS.HEALTH:
			// 		if(health != 2)
			// 			health++;
			// 		 break;
			// 	case powerupTest.PowerUPS.DAMAGE:
			// 		dblDmg = true;
			// 	 break;
			// 	case powerupTest.PowerUPS.SPEED:
			// 		dblSpeed = true;
			// 	 break;
			// }
		}
	}



	// [CommandAttribute]
	void CmdPowerUp(powerupTest.PowerUPS type, GameObject other) {
			switch(type) {
				case powerupTest.PowerUPS.HEALTH:
					if(health != 2)
						health++;
					 break;
				case powerupTest.PowerUPS.DAMAGE:
					dblDmg = true;
				 break;
				case powerupTest.PowerUPS.SPEED:
					dblSpeed = true;
				 break;
			}
			Destroy(other);
	}

	void OnCollisionEnter(Collision other) {
		// getting hit.
	}
}