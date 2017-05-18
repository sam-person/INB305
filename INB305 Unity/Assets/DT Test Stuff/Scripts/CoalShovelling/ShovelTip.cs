using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShovelTip : MonoBehaviour {

	// Used to determine if the shovel tip is in a trigger collider

	// If the shovel tip is IN coal
	public bool isInCoal = false;
	// If the shovel tip is IN furnace
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
