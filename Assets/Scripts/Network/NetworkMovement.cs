using UnityEngine;
using System.Collections;

public class NetworkMovement : MonoBehaviour {
	private Vector3 realPosition = Vector3.zero;
	private Quaternion realRotation;
	private Vector3 sendPosition = Vector3.zero;
	private Quaternion sendRotation;

	// Use this for initialization
	void Start () {
	
	}
	void OnSerializeNetworkView(BitStream stream, NetworkMessageInfo info) {
		if (stream.isWriting) {//this is your information you will send over the network
			sendPosition = this.transform.position;
			sendRotation = this.transform.rotation;
			stream.Serialize (ref sendPosition);//im pretty sure you have to use ref here, check that
			stream.Serialize (ref sendRotation);//same with the ref here...
		}else if(stream.isReading){//this is the information you will recieve over the network
			stream.Serialize (ref realPosition);//Vector3 position
			stream.Serialize (ref realRotation);//Quaternion postion
		}
	}

	 void Update(){
		if(!GetComponent<NetworkView>().isMine){
			transform.position = Vector3.Lerp(this.transform.position, realPosition, Time.deltaTime * 15);
			transform.rotation = Quaternion.Lerp(this.transform.rotation, realRotation, Time.deltaTime * 30);
		}
	}
}
