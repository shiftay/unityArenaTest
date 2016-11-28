using UnityEngine;
using UnityEngine.UI;

using System.Collections;

public class UIScript1 : MonoBehaviour {

	public GameObject player;
	public Slider slider;
	public GameObject[] hearts;
	public Sprite[] powerups;
	public GameObject[] gamobjPowerups;
	public Image[] currPowerup;
	public Text scoreText;
	private PlayerHealth1 hp;
	private Player1 script;
	bool setVars = false;
	
	void Start() {
		hp = player.GetComponent<PlayerHealth1>();
		script = player.GetComponent<Player1>();
	}

	// Update is called once per frame
	void Update () {



		switch(hp.healthPoints) {
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
		// 0 - missiles
		// 1 - Lightning

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

		if(script.dblDmg && !currPowerup[0].IsActive() && !currPowerup[1].IsActive()) {
			currPowerup[0].sprite = powerups[0];
			gamobjPowerups[0].SetActive(true);
		}

		if(script.dblSpeed && !currPowerup[0].IsActive() && !currPowerup[1].IsActive()) {
			currPowerup[0].sprite = powerups[1];
			gamobjPowerups[0].SetActive(true);
		}


		if(script.dblSpeed && script.dblDmg && currPowerup[0].IsActive()) {
			if(currPowerup[0].sprite == powerups[0])
				currPowerup[1].sprite = powerups[1];
			else
				currPowerup[1].sprite = powerups[0];
				
			gamobjPowerups[1].SetActive(true);
		}

	}


	void checkPowerups() {
		if(currPowerup[1].IsActive()) {
			if(currPowerup[0].sprite == powerups[0] && !script.dblDmg)
				gamobjPowerups[0].SetActive(false);

			if(currPowerup[0].sprite == powerups[1] && !script.dblSpeed)
				gamobjPowerups[0].SetActive(false);

			if(currPowerup[1].sprite == powerups[0] && !script.dblDmg)
				gamobjPowerups[1].SetActive(false);

			if(currPowerup[1].sprite == powerups[1] && !script.dblSpeed)
				gamobjPowerups[1].SetActive(false);
		}

		if(!script.dblDmg && !script.dblSpeed) {
			for(int i = 0; i < gamobjPowerups.Length; i++)
				gamobjPowerups[i].SetActive(false);
		}
	}
}
