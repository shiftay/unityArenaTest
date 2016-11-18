using UnityEngine;


public class CameraManager : MonoBehaviour {
	public Transform car;
	public float distance;
	public float height;
	public float rotationDamping = 3f;
	public float heightDamping = 2f;
	private float desiredAngle = 0;
	float desiredHeight;
	private Rigidbody carRB;


	void Start() {
		carRB = car.GetComponent<Rigidbody>();
	}

	void Update() {

	}

	void FixedUpdate() {
		desiredHeight = car.position.y + height;
		desiredAngle = car.eulerAngles.y;

		Vector3 localVelocity = car.InverseTransformDirection(carRB.velocity);
	}

	// Update is called once per frame
	void LateUpdate () {
		float currentAngle = transform.eulerAngles.y;
		float currentHeight = transform.position.y;

//		desiredAngle = car.eulerAngles.y;

		currentAngle = Mathf.LerpAngle(currentAngle, desiredAngle, rotationDamping * Time.deltaTime);
		currentHeight = Mathf.Lerp(currentHeight, desiredHeight, heightDamping * Time.deltaTime);

		Quaternion currentRotation = Quaternion.Euler(0,currentAngle,0);
		Vector3 finalPosition = car.position - (currentRotation * Vector3.forward * distance);
		finalPosition.y = currentHeight;

		transform.position = finalPosition;
		transform.LookAt(car);	
	}
}
