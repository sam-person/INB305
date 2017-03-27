using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;

public class Flag : NetworkBehaviour {

	[SyncVar]
	public int index;

	// Use this for initialization
	void Start () {
		TextMeshPro textChild;
		textChild = GetComponentInChildren<TextMeshPro>();
		if(!NetworkManager.singleton.GetComponent<GameManager>().trackerMode){
			//Destroy(textChild.gameObject);
			FindObjectOfType<Minimap>().AddTrackedObject(transform);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
