using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VR;

public class VrToggle : MonoBehaviour {

	[SerializeField]
	private GameObject VRCharacter;
	[SerializeField]
	private GameObject FPSCharacter;
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.V)){
			//VRSettings.LoadDeviceByName("OpenVR");
			//VRCharacter.SetActive(true);
			//FPSCharacter.SetActive(false);
			Destroy(this.gameObject);
		}
		if(Input.GetKeyDown(KeyCode.F)){
			VRSettings.LoadDeviceByName("None");
			VRCharacter.SetActive(false);
			FPSCharacter.SetActive(true);
			Destroy(this.gameObject);
		}
	}
}
