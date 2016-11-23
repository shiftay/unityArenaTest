using UnityEngine;
using System.Collections;

public class gizmoSpawnPoint : MonoBehaviour {

	public Color hldr;
	public float radius;

	void OnDrawGizmos()
	{
		Gizmos.color = hldr;
		Gizmos.DrawSphere(transform.position, radius);
	}


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
