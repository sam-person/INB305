using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Furnace : MonoBehaviour {

	public float fuelAmount = 0.0f;
	public float fuelUpRate = 2.0f;
	public float fuelMax = 100f;
	public float burnRate = 1.0f;
	float burnAmount = 0.0f;
	float maxBurnRate = 5f;

	// Light
	public Light furnaceLight;
	[Range(0,8)]
	public float maxLight;

	// Audio
	AudioSource[] audioSources;

	public FakeTank_Manager tankManager;

	void Start() {
		audioSources = GetComponents<AudioSource> ();
	}

	void Update() {
//		burnAmount = burnRate * Time.deltaTime;
//		if (fuelAmount - burnAmount >= 0) {
//			tankManager.fuel += fuelUpRate * Time.deltaTime / 100f;
//		} else {
//			if (fuelAmount > 0) {
//				tankManager.fuel += (burnAmount - fuelAmount) * Time.deltaTime / 100f;
//			}
//		}
//		tankManager.fuel = Mathf.Clamp (tankManager.fuel, 0f, 1f);
//		if (tankManager.fuel >= 1)
//			fuelAmount -= maxBurnRate * Time.deltaTime;
//
//		fuelAmount = Mathf.Clamp (fuelAmount - burnAmount, 0f, 1000f);
		UpdateLight();
		FurnaceAmbience ();
	}


	public void AcceptFuel(float amount) {
		tankManager.fuel = Mathf.Clamp (tankManager.fuel + amount / 100f, 0f, 1f);
		fuelAmount = Mathf.Clamp (fuelAmount + amount, 0f, 1000f);
	}

	void UpdateLight(){
		furnaceLight.intensity = Mathf.Lerp (0, maxLight, tankManager.fuel);
	}

	void FurnaceAmbience() {
		audioSources [0].volume = tankManager.fuel / 8f + 0.05f; // Ambience
		audioSources [1].volume = tankManager.fuel / 14f + 0.05f; // fire crackling
	}
}
