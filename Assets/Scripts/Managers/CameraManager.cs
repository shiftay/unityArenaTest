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
	public float holder;
	public float c_distance;
	private Vector3 offset = new Vector3(0,1.2f,1f);
	public Vector3 testNormalize;
	bool chase = true;

	void Start() {
		carRB = car.GetComponent<Rigidbody>();
		c_distance = distance;
	}

	void Update() {
		if(Input.GetKeyDown(KeyCode.T)) {
			chase = !chase;
		}
	}


	void FixedUpdate() {
		desiredHeight = car.position.y + height;
		desiredAngle = car.eulerAngles.y;
		holder = carRB.velocity.magnitude * 2.23693629f;
		Vector3 localVelocity = car.InverseTransformDirection(carRB.velocity);
		// kind of works?
		if(holder > 60f) {
			desiredHeight += 0.05f;
			c_distance += 0.05f;
			if(c_distance > distance*2) {
				c_distance = distance*2;
			}
		} else if (holder <= 60f) {
			c_distance -= 0.05f;
			if(c_distance < distance) {
				c_distance = distance;
			}
		}

		if(localVelocity.z < -0.5f) {
			desiredAngle += 180;
		}

	}

	// Update is called once per frame
	void LateUpdate () {
		if(chase) {
			float currentAngle = transform.eulerAngles.y;
			float currentHeight = transform.position.y;

	//		desiredAngle = car.eulerAngles.y;

			currentAngle = Mathf.LerpAngle(currentAngle, desiredAngle, rotationDamping * Time.deltaTime);
			currentHeight = Mathf.Lerp(currentHeight, desiredHeight, heightDamping * Time.deltaTime);

			Quaternion currentRotation = Quaternion.Euler(0,currentAngle,0);
			Vector3 finalPosition = car.position - (currentRotation * Vector3.forward * c_distance);
			finalPosition.y = currentHeight;

			transform.position = finalPosition;
			transform.LookAt(car);	
		}
		else {

			testNormalize = carRB.velocity.normalized;
			if(Mathf.Abs(testNormalize.x) > Mathf.Abs(testNormalize.z)) {
				offset = new Vector3(0f, 1.75f, 0f);
			} else {
				offset = new Vector3(0f, 1.75f, 0f);
			}

			transform.position = car.transform.position + offset;
			transform.rotation = Quaternion.Euler(0,car.transform.eulerAngles.y, 0);
			
		}

	}
}
