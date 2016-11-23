using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(LineRenderer))]
public class AITest : SteeringBehaviour {

	public float playerTrackingRad = 20f;
	public float trackCloseObjects = 10f;
	public LayerMask wallsEtc;
	public LayerMask combatants;
	private Vector3 currentEvade;
	public bool evading = false;
	public float distanceHolder;
	bool reloading;
	float timer;
	float cooldown = 2.5f;
	int score;
	Ray shootRay;
	RaycastHit shotHit;
	float range = 100f;
	LineRenderer gunLine;

	void OnDrawGizmos()
	{
		Gizmos.color = Color.black;
		Gizmos.DrawWireSphere(transform.position, trackCloseObjects);
		Gizmos.color = Color.white;
		Gizmos.DrawWireSphere(transform.position, playerTrackingRad);
	}

	void Start() {
		gunLine = GetComponent<LineRenderer>();
	}
	// Update is called once per frame
	void Update () {
//----- Evasion -----
		if(!evading) {
			Collider[] holder = Physics.OverlapSphere(transform.position, trackCloseObjects, wallsEtc);
			if(holder.Length > 0) {
				currentEvade = holder[0].ClosestPointOnBounds(transform.position);
				evading = true;
			}
		} else {
			Evade( transform.position + Steering , currentEvade);
			distanceHolder = transform.position.magnitude - currentEvade.magnitude;
			if(Mathf.Abs(transform.position.magnitude - currentEvade.magnitude) > 0.5f )
				evading = false;
		}
//----- Evasion -----

//----- Shooting -----
		Collider[] target = Physics.OverlapSphere(transform.position, playerTrackingRad, combatants);
		if(target.Length > 1 && !reloading) {
			for(int i = 0; i < target.Length; i++) {
				if(target[i] != this) {
					Shoot(target[1].ClosestPointOnBounds(transform.position));
					break;
				}
			}
			
		}

		if(reloading) {
			timer += Time.deltaTime;
			if(timer > 0.25f)
				gunLine.enabled = false;
			if(timer >= cooldown) {
				reloading = false;
				
			}
		}
//----- Shooting -----

		Wander();
		ApplySteering();

	}


	void Shoot(Vector3 target) {
		 timer = 0f;

        // gunLight.enabled = true;

        // gunParticles.Stop ();
        // gunParticles.Play ();
		Vector3 offset = new Vector3(0f, 3f,0f);
        gunLine.enabled = true;
        gunLine.SetPosition(0, transform.position + offset);
		gunLine.materials[1].color = Color.red;
        shootRay.origin = transform.position + offset;
		Vector3 sphere = Random.insideUnitSphere * 5;
		sphere.y = 0;
		transform.LookAt((target + sphere));
        shootRay.direction = transform.forward;
		
        if(Physics.Raycast(shootRay, out shotHit, range))
        {
			if(shotHit.collider.tag == "Player") { 			// damage players.
				PlayerHealth enemy = shotHit.collider.GetComponent<PlayerHealth>();

				if(enemy.health != 0) {
					enemy.health--;
					if(enemy.health == 0) {
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

	}
}
