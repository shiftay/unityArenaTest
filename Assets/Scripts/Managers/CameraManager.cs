using UnityEngine;
using UnityEngine.Networking;

public class CameraManager : NetworkBehaviour {
	public Transform player;
	public float distance;
	public float height;
	public float rotationDamping = 3f;
	public float heightDamping = 2f;
	private float desiredAngle = 0;
	float desiredHeight;
	private Rigidbody playerRB;

	void Start() {
		playerRB = player.GetComponent<Rigidbody>();
	}

	void Update() {

	}

	void FixedUpdate() {
		desiredHeight = player.position.y + height;
		desiredAngle = player.eulerAngles.y;
	}

	// Update is called once per frame
	void LateUpdate () {
		RaycastHit camHit;

		if(Physics.Linecast(transform.position, player.position, out camHit)) {
			if(camHit.collider.tag == "Player") {
				// draw a line behind player to see if can move back
				RaycastHit test;
				if(!Physics.Linecast(transform.position, transform.position + (-transform.forward * 5), out test)) {
					// nothing behind
				}
			} else {
				// zoom a little
				// Debug.Log("hit something else");
				// distance -= 0.5f;
				// height -= 0.25f;
			}
		}


		float currentAngle = transform.eulerAngles.y;
		float currentHeight = transform.position.y;

//		desiredAngle = player.eulerAngles.y;

		currentAngle = Mathf.LerpAngle(currentAngle, desiredAngle, rotationDamping * Time.deltaTime);
		currentHeight = Mathf.Lerp(currentHeight, desiredHeight, heightDamping * Time.deltaTime);

		Quaternion currentRotation = Quaternion.Euler(0,currentAngle,0);
		Vector3 finalPosition = player.position - (currentRotation * Vector3.forward * distance);
		finalPosition.y = currentHeight;

		transform.position = finalPosition;
		transform.LookAt(player);	
	}
}
