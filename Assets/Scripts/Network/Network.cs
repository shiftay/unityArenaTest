using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Network : NetworkManager {
 
    public int chosenCharacter = 0;
	
     //subclass for sending network messages
    public class NetworkMessage : MessageBase {
        public int chosenClass;
    }
 
    public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId, NetworkReader extraMessageReader) {
        NetworkMessage message = extraMessageReader.ReadMessage< NetworkMessage>();
        int selectedClass = message.chosenClass;
        Debug.Log("server add with message " + GameManager.Instance.skinChoice );
        GameObject player = (GameObject)Instantiate(GameManager.Instance.PlayerPF[selectedClass], GetStartPosition().position ,Quaternion.identity);
        GameManager.Instance.AddPlayer(player);
        ScoreManager.Instance.AddPlayer(player.GetComponent<Player>());
        NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);
    }
 
    public override void OnClientConnect(NetworkConnection conn) {
        NetworkMessage test = new NetworkMessage();
        test.chosenClass = PlayerPrefs.GetInt("Character");
 
        ClientScene.AddPlayer(conn, 0, test);
    }
 
    
	public override void OnClientSceneChanged(NetworkConnection conn) {
			//base.OnClientSceneChanged(conn);
	}
}
