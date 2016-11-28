using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;

public class UIScript :	NetworkBehaviour {

	public GameObject player;
	public Slider slider;
	public GameObject[] hearts;
	public Sprite[] powerups;
	public GameObject[] gamobjPowerups;
	public Image[] currPowerup;
	public Text scoreText;
	public GameObject respawnText;
	public Text spawnBack;
	private PlayerHealth hp;
	private Player script;
	public GameObject[] gameOverObjs;
	public Text scoreBoard;
	public BufferServerInput serverStatus;
	public GameObject startupText;
	public Text startup;
	public Image sliderImage;
	bool setVars = false;
	float respawnTimer;


	void Start() {
		sliderImage.color = GameManager.Instance.LaserCol;
	}
	
	// Update is called once per frame
	void Update () {
		if(isServer && !isClient)
			return;

		
		if(GameManager.Instance.gameOver) {
			if(!gameOverObjs[0].activeInHierarchy) {
				for(int i = 0; i < gameOverObjs.Length; i++)
					gameOverObjs[i].SetActive(true);
			}

		//	scoreBoard.text = "Scoreboard\n";
		}

		if(!serverStatus.toggle) {
			startupText.SetActive(true);
			if(serverStatus.startDelay == 0)
				startup.text = "Waiting for Partiers";
			else 
				startup.text = "Get Ready to Party!";
		} else {
			startupText.SetActive(false);
		}


		if(player != null && !setVars) {
			hp = player.GetComponent<PlayerHealth>();
			script = player.GetComponent<Player>();
			setVars = true;
		}
		
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

		if(script.isDead) {
			respawnText.SetActive(true);
			if(script.RespawnTimer > 3) {
				spawnBack.text = "Time to party!";
			}

			if(script.RespawnTimer < 1) {
				spawnBack.text = "Respawning Soon...";
			}
		} else {
			respawnText.SetActive(false);
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
