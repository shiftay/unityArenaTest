using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIScript : MonoBehaviour {

	public GameObject player;
	public Slider slider;
	public GameObject[] hearts;
	public Sprite[] powerups;
	public GameObject[] gamobjPowerups;
	public Image[] currPowerup;
	public Text scoreText;
	private PlayerHealth hp;
	private Player script;

	// Use this for initialization
	void Start () {
		script = player.GetComponent<Player>();
		hp = player.GetComponent<PlayerHealth>();
		slider.maxValue = script.cooldown;
	}

	// Update is called once per frame
	void Update () {
		switch(hp.health) {
			case 0:
				for(int i=0; i < hearts.Length; i++)
					hearts[i].SetActive(false);
				break;
			case 1:
				hearts[0].SetActive(false);
				break;
			case 2:
				for(int i=0; i < hearts.Length; i++)
					hearts[i].SetActive(true);
				break;
		}


		Powerups();
		checkPowerups();
		scoreText.text = script.score.ToString();
		// 1 - missiles
		// 2 - Lightning

		if(script.reloadTimerStarted) {
			// if(!timerStarted) {
			// 	timerStarted = true;
			// 	slider.value = 0;
			// }
			slider.value = script.reloadTimer;
		} else {
			slider.value = script.cooldown;
		}
	}


	void Powerups() {

		if(script.DDMG && !currPowerup[0].IsActive() && !currPowerup[1].IsActive()) {
			currPowerup[0].sprite = powerups[0];
			gamobjPowerups[0].SetActive(true);
		}

		if(script.DSPD && !currPowerup[0].IsActive() && !currPowerup[1].IsActive()) {
			currPowerup[0].sprite = powerups[1];
			gamobjPowerups[0].SetActive(true);
		}


		if(script.DSPD && script.DDMG && currPowerup[0].IsActive()) {
			if(currPowerup[0].sprite == powerups[0])
				currPowerup[1].sprite = powerups[1];
			else
				currPowerup[1].sprite = powerups[0];
				
			gamobjPowerups[1].SetActive(true);
		}

	}


	void checkPowerups() {
		if(currPowerup[1].IsActive()) {
			if(currPowerup[0].sprite == powerups[0] && !script.DDMG)
				gamobjPowerups[0].SetActive(false);

			if(currPowerup[0].sprite == powerups[1] && !script.DSPD)
				gamobjPowerups[0].SetActive(false);

			if(currPowerup[1].sprite == powerups[0] && !script.DDMG)
				gamobjPowerups[1].SetActive(false);

			if(currPowerup[1].sprite == powerups[1] && !script.DSPD)
				gamobjPowerups[1].SetActive(false);
		}

		if(!script.DDMG && !script.DSPD) {
			for(int i = 0; i < gamobjPowerups.Length; i++)
				gamobjPowerups[i].SetActive(false);
		}
	}
}
