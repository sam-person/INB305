using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShovelTip : MonoBehaviour {

	public bool isInCoal = false;
	public bool isInFurnace = false;

	void OnTriggerStay(Collider other) {
		if (other.CompareTag ("Coal")) {
			isInCoal = true;
		} else if (other.CompareTag ("Furnace")) {
			isInFurnace = true;
		}
	}

	void OnTriggerExit(Collider other) {
		if (other.CompareTag ("Coal")) {
			isInCoal = false;
		} else if (other.CompareTag ("Furnace")) {
			isInFurnace = false;
		}
	}
}
