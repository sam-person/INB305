using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ControllerStatsDisplay : NetworkBehaviour {

	public GameObject flag;
	public Vector3 controller1vector, controller2vector;

	[SerializeField]
	private Transform controller1;
	[SerializeField]
	private Transform controller2;

	void Start(){
		controller1 = NetworkManager.singleton.GetComponent<SteamControllerHolder>().controller1.transform;
		controller2 = NetworkManager.singleton.GetComponent<SteamControllerHolder>().controller2.transform;
	}

	// Update is called once per frame
	void Update () {
		if(!isLocalPlayer){
			return;
		}
		if(NetworkManager.singleton.GetComponent<GameManager>().trackerMode){
			CmdUpdatePosition(controller1.position, controller2.position);
			//CmdUpdatePosition(Vector3.back, Vector3.left);
		}
		
	}
	[Command]
	public void CmdUpdatePosition(Vector3 pos1, Vector3 pos2){
		RpcGetPosition(pos1, pos2);
	}

	[ClientRpc]
	public void RpcGetPosition(Vector3 pos1, Vector3 pos2){
		controller1vector = pos1;
		controller2vector = pos2;
		NetworkManager.singleton.GetComponent<SteamControllerHolder>().netcontroller1.transform.position = controller1vector;
		NetworkManager.singleton.GetComponent<SteamControllerHolder>().netcontroller2.transform.position = controller2vector;
	}
}
