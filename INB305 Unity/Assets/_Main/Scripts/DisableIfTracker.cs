using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class DisableIfTracker : MonoBehaviour {

	// Use this for initialization
	void Update () {
		if(NetworkManager.singleton.GetComponent<GameManager>().trackerMode){
			gameObject.SetActive(false);
		}
	}
}
