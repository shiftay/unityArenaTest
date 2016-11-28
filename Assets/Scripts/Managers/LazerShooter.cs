using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class LazerShooter : NetworkBehaviour {

	private static LazerShooter instance = null;
	public static LazerShooter Instance { get { return instance;}}
	Vector3 offset = new Vector3(0,3,0);
	[SyncVar]
	public LineRenderer lazer;
	// Use this for initialization
	void Start () {
		if (instance == null)
			instance = this;
		else if (instance != this)
			Destroy(gameObject);
		
		DontDestroyOnLoad(gameObject);

		lazer = GetComponent<LineRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	public void Shoot(Vector3 position, Vector3 forward, Color color, int number) {
		Ray shoot = new Ray();
		RaycastHit shotHit = new RaycastHit();

		shoot.origin = position + offset;
		shoot.direction = forward;
		lazer.enabled = true;
		lazer.SetPosition(0,position+offset);
		lazer.materials[0].color = color;
		lazer.materials[0].shader = Shader.Find("Unlit/Color");

		if(Physics.Raycast(shoot, out shotHit, 200))
        {
			if(shotHit.collider.tag == "Player") { 			// damage players.
				PlayerHealth enemy = shotHit.collider.GetComponent<PlayerHealth>();
				enemy.TakeDmg(false);
				if(enemy.health <= 0)
					GameManager.Instance.scores[number]++;
			}
			lazer.SetPosition (1, shotHit.point);
     	}
        else
        {
            lazer.SetPosition (1, shoot.origin + shoot.direction * 200);
        }


	}
}
