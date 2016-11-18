using UnityEngine;
using System.Collections;

public abstract class SteeringBehaviour : MonoBehaviour {
	public Rigidbody objectToMove;
	public float maxSpeed;
	public float turnSpeed;
	public float wanderCircleDistance;
	public float wanderCircleRadius;
	public float wanderAngleAdjustment;

	public Vector3 Steering {get {return steering;}
							 set {steering = value;}}
	private float wanderAngle;
	private Vector3 steering = Vector3.zero;

	// void OnDrawGizmos()
	// {	
	// 	Gizmos.color = Color.red;
	// 	Gizmos.DrawWireSphere(transform.position, 3f);	
	// }
	void Start() {
		
	}

	// void FixedUpdate()
	// {
	// 	Wander();
	// 	ApplySteering();
	// }


	public void Reset() {
		steering = Vector3.zero;
	}
	
	public void Seek(Vector3 position, float slowingRadius = 0) {
		steering += DoSeek(position, slowingRadius);
	}
	
	public void Flee(Vector3 position) {
		steering += DoFlee(position);
	}
	
	public void Wander(){
		steering += DoWander();
	}
	// ------------ Experimental ------------
	public void Evade(Vector3 ahead,Vector3 evadePos) {
		steering += DoEvade(ahead, evadePos);
	}

	private Vector3 DoEvade(Vector3 ahead,Vector3 evadePos) {
		Vector3 desiredVelocity = new Vector3(ahead.x - evadePos.x, 0f, ahead.z - evadePos.z);
		return desiredVelocity.normalized * maxSpeed;
	}
	// ------------ Experimental ------------
	private Vector3 DoSeek(Vector3 position, float slowingRadius) {
		Vector3 desiredVelocity = position - transform.position;
		float distance = desiredVelocity.magnitude;
		if(distance < slowingRadius) {
			desiredVelocity = desiredVelocity.normalized * maxSpeed * (distance / slowingRadius);	
		}
		else {
			desiredVelocity = desiredVelocity.normalized * maxSpeed;
		}

		return desiredVelocity - objectToMove.velocity;
	}

	private Vector3 DoFlee(Vector3 position) {
		Vector3 desiredVelocity = transform.position - position;
		return desiredVelocity - objectToMove.velocity;
	}

	private Vector3 DoWander() {
		Vector3 circleCenter = objectToMove.velocity.normalized * wanderCircleDistance;
		Vector3 displacement = new Vector3(0,0,wanderCircleRadius);
		displacement = SetAngle(displacement, wanderAngle);
		wanderAngle += Random.Range(-1f,1f) * wanderAngleAdjustment;
		return circleCenter + displacement;
	}

	private Vector3 SetAngle(Vector3 vector, float rotation) {
		float length = vector.magnitude;
		vector.x = Mathf.Cos(rotation) * length;
		vector.z = Mathf.Sin(rotation) * length;
		return vector;
	}

	public void ApplySteering() {
		if(steering.magnitude > turnSpeed) {
			steering = steering.normalized * turnSpeed;
		}
		objectToMove.GetComponent<Rigidbody>().velocity = objectToMove.GetComponent<Rigidbody>().velocity + steering;
		// objectToMove.velocity += steering;

		if(objectToMove.GetComponent<Rigidbody>().velocity.magnitude > maxSpeed) {
			objectToMove.GetComponent<Rigidbody>().velocity = (objectToMove.GetComponent<Rigidbody>().velocity).normalized * maxSpeed;
		}
		// objectToMove.transform.rotation = Quaternion.LookRotation(objectToMove.velocity, Vector3.right);
		objectToMove.transform.rotation = Quaternion.LookRotation(objectToMove.velocity);

		Quaternion temp = objectToMove.transform.rotation;
		temp = Quaternion.Euler(90f, temp.eulerAngles.y, temp.eulerAngles.z);
		objectToMove.transform.rotation = temp;
	}
}
