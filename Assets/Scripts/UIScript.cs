using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIScript : MonoBehaviour {

	public GameObject player;
	public Slider slider;
	private Player script;
	// Use this for initialization
	void Start () {
		script = player.GetComponent<Player>();
		slider.maxValue = 5f;
	}
	
	// Update is called once per frame
	void Update () {
		if(script.reloadTimerStarted) {
			// if(!timerStarted) {
			// 	timerStarted = true;
			// 	slider.value = 0;
			// }
			slider.value = script.reloadTimer;
		} else {
			slider.value = 5f;
		}
	}
}
