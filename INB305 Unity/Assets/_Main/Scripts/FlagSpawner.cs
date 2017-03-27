using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using VRTK;

public class FlagSpawner : NetworkBehaviour {

	public int index = 0;
	public GameObject flag;

	// Use this for initialization
	void Start () {
		GetComponent<VRTK_ControllerEvents>().TriggerPressed += new ControllerInteractionEventHandler(DoTriggerPressed);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void DoTriggerPressed(object sender, ControllerInteractionEventArgs e){
		if(!NetworkManager.singleton.GetComponent<GameManager>().trackerMode){
			Debug.Log("pressed trigger");
			return;
		}
		Debug.Log("pressed trigger");
        GameObject spawned = (GameObject)Instantiate(flag, transform.position, Quaternion.identity);
        spawned.GetComponent<Flag>().index = this.index;
        NetworkServer.Spawn(spawned);
        index++;
    }
}
