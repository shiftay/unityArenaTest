using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneManage : MonoBehaviour 
{

	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	public void Play()
	{
		SceneManager.LoadScene("CharacterSelect");
	}

	public void HowTo()
	{
		SceneManager.LoadScene("How To");
	}

	public void Exit()
	{
		Application.Quit();
	}

	public void SinglePlayer()
	{
		SceneManager.LoadScene("Main");
	}

	public void MultiPlayer()
	{
		PlayerPrefs.SetInt("Character", GameManager.Instance.skinChoice);
		PlayerPrefs.SetFloat("red", GameManager.Instance.LaserCol.r);
		PlayerPrefs.SetFloat("green", GameManager.Instance.LaserCol.g);
		PlayerPrefs.SetFloat("blue", GameManager.Instance.LaserCol.b);
		SceneManager.LoadScene("Arena1");
	}

	public void Back()
	{
		SceneManager.LoadScene("Menu");
	}
}
