using UnityEngine;

public class gizmoSpawnPoint : MonoBehaviour {

	public Color hldr;
	public float radius;

	void OnDrawGizmos()
	{
		Gizmos.color = hldr;
		Gizmos.DrawSphere(transform.position, radius);
	}

}
