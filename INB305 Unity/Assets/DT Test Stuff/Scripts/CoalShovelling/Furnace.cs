using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Furnace : MonoBehaviour {

	public float fuelAmount = 0.0f;
	public float fuelUpRate = 2.0f;
	public float fuelMax = 100f;
	public float burnRate = 1.0f;
	float burnAmount = 0.0f;

	public FakeTank_Manager tankManager;

	void Update() {
		burnAmount = burnRate * Time.deltaTime;
		if (fuelAmount - burnAmount >= 0) {
			tankManager.fuel += fuelUpRate / fuelMax * Time.deltaTime;
		} else {
			if (fuelAmount > 0) {
				tankManager.fuel += (burnAmount - fuelAmount) / fuelMax * Time.deltaTime;
			}
		}
		fuelAmount = Mathf.Clamp (fuelAmount - burnAmount, 0f, 1000f);
	}

	public void AcceptFuel(float amount) {
		fuelAmount = Mathf.Clamp (fuelAmount + amount, 0f, 1000f);
	}
}
