using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class NetworkManagerHUD : NetworkManager 
{
	public void StartupHost()
	{
		SetPort();
		NetworkManager.singleton.StartHost();
	}

	public void JoinGame()
	{
		SetIPAddress();
		SetPort();
		NetworkManager.singleton.StartClient();
	}
	
	void SetPort()
	{
		NetworkManager.singleton.networkPort = 7777;
	}

	void SetIPAddress()
	{
		string ipAddress = GameObject.Find("InputFieldIPAddress").transform.FindChild("Text").GetComponent<Text>().text;
		NetworkManager.singleton.networkAddress = ipAddress;
	}

	void OnLevelWasLoaded(int level)
	{
		if(level == 0)
		{
			SetupMenuSceneButtons();
		}
		else
		{
			SetupOtherSceneButtons();
		}
	}

	void SetupMenuSceneButtons()
	{
		GameObject.Find("ButtonStartHost").GetComponent<Button>().onClick.RemoveAllListeners();
		GameObject.Find("ButtonStartHost").GetComponent<Button>().onClick.AddListener(StartupHost);
	
		GameObject.Find("ButtonStartHost").GetComponent<Button>().onClick.RemoveAllListeners();
		GameObject.Find("ButtonStartHost").GetComponent<Button>().onClick.AddListener(JoinGame);
	}

	void SetupOtherSceneButtons()
	{
		GameObject.Find("ButtonStartHost").GetComponent<Button>().onClick.RemoveAllListeners();
		GameObject.Find("ButtonStartHost").GetComponent<Button>().onClick.AddListener(NetworkManager.singleton.StopHost);
	}	
}