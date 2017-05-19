using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FakeTank_Manager : MonoBehaviour {
	public bool useNetwork = false;

	public float speed, rotation;
	[Header("Settings")]
	public VRTK.VRTK_SpringLever crank;
	public VRTK.VRTK_Wheel wheel;
	public float minimumSpeed, fuelThreshold, fuelConsumption;
	public Image fuelBar, speedBar, reverseBar;
	[Header("Stats")]
	[Range(0,1)]
	public float fuel = 1;
	public float underThresholdClamp;
	public TMPro.TextMeshPro display;

	public EngineSound engineSound;

	// Low Fuel Alarms
	//bool lowFuel = false;
	//float lowFuelThreshold = 0.1f;

		// Lights for low fuel
	public Light[] normalLights;
	float originalLightIntensity = 1f;
	public GameObject[] alarmLights;
	public AlarmSound alarmSound;
	public Color dimmedRedLightColor;
	Color originalLightColor;
	public Vector2 lightIntensityThresholds = new Vector2();

	Tank_Network net;

	// Use this for initialization
	void Start () {
		// Store initial values
		originalLightColor = normalLights [0].color;
		originalLightIntensity = normalLights [0].intensity;
		net = GetComponent<Tank_Network> ();
	}
	
	// Update is called once per frame
	void Update () {
		speed = (-(crank.GetNormalizedValue()/100.0f)+0.5f)*2.0f;

		EngineAudio (); // Update Engine sounds

		if(fuel < fuelThreshold){
			underThresholdClamp = Mathf.Max(minimumSpeed, Mathf.InverseLerp(0, fuelThreshold, fuel));
			speed = Mathf.Clamp(speed, -underThresholdClamp, underThresholdClamp);
		}
		rotation = (wheel.GetNormalizedValue()/100.0f);
		fuel = Mathf.Max(0,fuel-(fuelConsumption*Time.deltaTime*0.001f));

		CheckLowFuel (); // Update low fuel alarms

		display.text = "Speed: " + (speed * 100.0f).ToString("F2") + "\nRotation: " + (rotation * 100.0f).ToString("F2") + "\nFuel: " + (fuel * 100.0f).ToString("F2");
		fuelBar.fillAmount = fuel;
		speedBar.fillAmount = speed;
		reverseBar.fillAmount = -speed;
	}

	void FixedUpdate(){
		if (!useNetwork) {
			return;
		}
		float angle_ = Mathf.Lerp (0, 180, rotation);
		Debug.Log ("Angle: " + angle_);

		float speed_ = Mathf.Lerp (50, 60, Mathf.InverseLerp (-1, 1, speed));
		Debug.Log ("Speed:" + speed_);

		net.SendAngle (angle_);
		net.SendSpeed (speed_);

	}

	// Change pitch and volume of Engine sound based on speed;
	void EngineAudio() {
		engineSound.SetPitch (speed); // Pitch

		engineSound.SetVolume (fuel /1.5f + 0.1f); // Volume
	}

	public int lowFuelStage = 0;

	void CheckLowFuel() {

		// Check for fuel thresholds
		if (fuel < (fuelThreshold * 0.5f)) {
			lowFuelStage = 2;
		} else if (fuel < fuelThreshold) {
			lowFuelStage = 1;
		} else {
			lowFuelStage = 0;
		}

		switch (lowFuelStage) {
		case 1:
			alarmSound.SetVolume (0.3f); // set volume to low

			// Stage 1 settings
			// Play alarm sound if alarm is not playing
			if (!alarmSound.IsPlaying) 
				alarmSound.StartAlarm ();

			// Set normal lights to original colour if they are not the original colour
			if (fuel > 0 && !normalLights [0].color.Equals (originalLightColor)) {
				SetLightColour (originalLightColor);
			}

			// Out of Stage 2 settings
			// Turn alarm lights off if they are on
			if (alarmLights[0].activeSelf)
				ToggleAlarmLights (false);

			// Set normal lights intensity to original intensity if it is not
			if (normalLights [0].intensity < originalLightIntensity)
				DimLights (false);

			break;
		case 2:

			// Stage 2 settings
			DimLights (true); // continuously dim lights based on fuel
			alarmSound.SetVolume (); // Sets volume to 1

			// turn alarm lights on if they aren't on
			if (!alarmLights [0].activeSelf)
				ToggleAlarmLights (true);

			// turn normal lights red if they aren't red 
			if (fuel <= 0 && !normalLights [0].color.Equals (dimmedRedLightColor)) {
				SetLightColour (dimmedRedLightColor);
			}

			break;

		default:
			// Normal Stage 0 settings
			// Turn alarm sound off if it is on
			if (alarmSound.IsPlaying)
				alarmSound.StopAlarm ();

			// Turn alarm lights off if it is on
			if (!alarmLights [0].activeSelf)
				ToggleAlarmLights (false);

			// Set normal lights to the original colour if they are not the original colour
			if (fuel > 0 && !normalLights [0].color.Equals (originalLightColor)) {
				SetLightColour (originalLightColor);
			}

			// Set normal lights intensity to original intensity if it is not
			if (normalLights [0].intensity < originalLightIntensity)
				DimLights (false);
			break;
		}
	}

	// Turns the alarm lights on and off 
	void ToggleAlarmLights(bool state) {
		for (int i = 0; i < alarmLights.Length; i++) {
			alarmLights [i].SetActive (state);
		}
	}

	// Dim the lights based on fuel and low fuel threshold
	void DimLights(bool dim = true) {
		if (dim) {
			// Get value between [0, 1] based on fuel to use for Lerping
			float t = Mathf.InverseLerp (fuelThreshold / 2.0f, 0, fuel);
			// Lerp value between lightIntensityThresholds
			float intensity = Mathf.Lerp (lightIntensityThresholds.x, lightIntensityThresholds.y, t);

			// Set normal lights to new intensity
			for (int i = 0; i < normalLights.Length; i++) {
				normalLights [i].intensity = intensity;	
			}
		} else {
			// Set normal lights to original intensity
			for (int i = 0; i < normalLights.Length; i++) {
				normalLights [i].intensity = originalLightIntensity;
			}
		}
	}

	// Sets normal lights to the given colour
	void SetLightColour(Color newColor) {
		for (int i = 0; i < normalLights.Length; i++) {
			normalLights [i].color = newColor;	
		}
	}
}
