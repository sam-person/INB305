using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VR;
using TMPro;

public class VrToggle : MonoBehaviour {

	[SerializeField]
	private GameObject VRCharacter;
	[SerializeField]
	private GameObject FPSCharacter;
	[SerializeField]
	private TextMeshProUGUI statusText;

	public void VRMode(){
		statusText.SetText("VR Mode");
		Destroy(this.gameObject);
	}

	public void FPSMode(){
		statusText.SetText("FPS Mode");
		VRSettings.LoadDeviceByName("None");
		VRCharacter.SetActive(false);
		FPSCharacter.SetActive(true);
		Destroy(this.gameObject);
	}

	public void TrackerMode(){
		statusText.SetText("Tracker Mode");
		FindObjectOfType<ControllerStatsDisplay>().trackerMode = true;
		Destroy(this.gameObject);
	}
}
