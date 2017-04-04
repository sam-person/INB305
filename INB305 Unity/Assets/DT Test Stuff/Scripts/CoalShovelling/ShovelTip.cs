using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShovelTip : MonoBehaviour {

	public bool IsInCoal {get; set;}
//	float colliderTimer = 0.0f;
//	float colliderTime = 0.5f;

	void OnTriggerStay(Collider other) {
		if (other.CompareTag ("Coal")) {
			IsInCoal = true;
		}
	}

	void OnTriggerExit(Collider other) {
		if (other.CompareTag ("Coal")) {
			IsInCoal = false;
		}
	}
}
