using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LaserColor : MonoBehaviour 
{
	public Slider Red;
	public Slider Green;
	public Slider Blue;
	public Image laserC;

	// Use this for initialization
	void Start () 
	{
		laserC = gameObject.GetComponent<Image>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		laserC.color = new Color(Red.value, Green.value, Blue.value,1);
		GameManager.Instance.LaserCol = new Color(Red.value, Green.value, Blue.value,1);
	}
}
