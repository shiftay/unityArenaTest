using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class testSelect : MonoBehaviour {

	public Text selection;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Selected() {
		GameManager.Instance.test = selection.text;
		SceneManager.LoadScene("Menu");
	}
}
