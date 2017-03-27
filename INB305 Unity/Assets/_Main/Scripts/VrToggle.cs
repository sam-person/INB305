using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VR;
using TMPro;
using VRTK;
using UnityEngine.Networking;

public class VrToggle : MonoBehaviour {

	[SerializeField]
	private TextMeshProUGUI statusText;


	public void VRMode(){
		statusText.SetText("VR Mode");
		Destroy(this.gameObject);
	}

	public void TrackerMode(){
		statusText.SetText("Tracker Mode");
		NetworkManager.singleton.GetComponent<GameManager>().trackerMode = true;
		Destroy(this.gameObject);
	}
}
