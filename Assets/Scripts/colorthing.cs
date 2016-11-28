using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class colorthing : MonoBehaviour {

	public Slider red;
	public Slider green;
	public Slider blue;
	public Image colorHolder;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		colorHolder.color = new Color(red.value, green.value, blue.value,1);
	}
}
