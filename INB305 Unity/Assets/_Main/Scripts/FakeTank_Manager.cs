using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FakeTank_Manager : MonoBehaviour {
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
	bool lowFuel = false;
	float lowFuelThreshold = 0.1f;
	public Light[] normalLights;
	public GameObject[] alarmLights;
	public AlarmSound alarmSound;
	public Color dimmedRedLightColor;
	Color originalLightColor;
	public Vector2 lightIntensityThresholds = new Vector2();

	// Use this for initialization
	void Start () {
		originalLightColor = normalLights [0].color;
	}
	
	// Update is called once per frame
	void Update () {
		speed = (-(crank.GetNormalizedValue()/100.0f)+0.5f)*2.0f;
		EngineAudio ();
		if(fuel < fuelThreshold){
			underThresholdClamp = Mathf.Max(minimumSpeed,Mathf.InverseLerp(0,fuelThreshold, fuel));
			speed = Mathf.Clamp(speed, -underThresholdClamp, underThresholdClamp);
		}
		rotation = (wheel.GetNormalizedValue()/100.0f)-0.5f;
		fuel = Mathf.Max(0,fuel-(fuelConsumption*Time.deltaTime*0.001f));
		CheckLowFuel ();

		display.text = "Speed: " + (speed * 100.0f).ToString("F2") + "\nRotation: " + (rotation * 100.0f).ToString("F2") + "\nFuel: " + (fuel * 100.0f).ToString("F2");
		fuelBar.fillAmount = fuel;
		speedBar.fillAmount = speed;
		reverseBar.fillAmount = -speed;
	}

	void EngineAudio() {
//		if (-0.01f <= speed && speed <= 0.01f)
//			engineSound.TransitionToIdle ();
//		else
//			engineSound.TransitionToDriving ();

		engineSound.SetPitch (speed);

		engineSound.SetVolume (fuel + 0.5f);
	}

	void CheckLowFuel() {

		if (fuel < fuelThreshold) {

			if (!alarmSound.IsPlaying) {
				alarmSound.StartAlarm ();
				alarmSound.SetVolume (0.3f);
			}


			if (fuel < (fuelThreshold * 0.5f)) {
				alarmSound.SetVolume (0.5f);
				DimLights ();
				if (!alarmLights [0].activeSelf)
					ToggleAlarmLights (true);

				if (fuel <= 0 && !normalLights [0].color.Equals (dimmedRedLightColor)) {
					SetLightColour (dimmedRedLightColor);
					alarmSound.SetVolume ();

				}

				if (fuel > 0 && !normalLights [0].color.Equals (originalLightColor)) {
					SetLightColour (originalLightColor);
				}
			} else {
				if (alarmLights[0].activeSelf)
					ToggleAlarmLights (false);
			}
		} else {
			if (alarmSound.IsPlaying)
				alarmSound.StopAlarm ();
		}

//		if (!lowFuel && fuel <= lowFuelThreshold) {
//			lowFuel = true;
//			ToggleAlarmLights (true);
//			alarmSound.StartAlarm ();
//		}
//		if (lowFuel && fuel > lowFuelThreshold) {
//			lowFuel = false;
//			ToggleAlarmLights (false);
//			alarmSound.StopAlarm ();
//		}

	}

	void ToggleAlarmLights(bool state) {
		for (int i = 0; i < alarmLights.Length; i++) {
			alarmLights [i].SetActive (state);
		}
	}

	void DimLights() {
		float t = Mathf.InverseLerp (fuelThreshold/2.0f, 0, fuel);
		float intensity = Mathf.Lerp (lightIntensityThresholds.x, lightIntensityThresholds.y, t);

		for (int i = 0; i < normalLights.Length; i++) {
			normalLights [i].intensity = intensity;	
		}
	}

	void SetLightColour(Color newColor) {
		for (int i = 0; i < normalLights.Length; i++) {
			normalLights [i].color = newColor;	
		}
	}
}
