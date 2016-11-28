using UnityEngine;
using System.Collections;

public class GM : MonoBehaviour 
{
	public static GM Instance {get {return instance;}}
	private static GM instance = null;
	public Color LaserCol;

	// Use this for initialization
	void Start () 
	{
		if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
        
        DontDestroyOnLoad(gameObject);
	}
	
	// Update is called once per frame
	void Update () 
	{

	}
}
