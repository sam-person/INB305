using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FakeTank_Manager : MonoBehaviour {

	public bool online = false;
	public float crankL, crankR;
	[Header("Settings")]
	public VRTK.VRTK_SpringLever crankL_SpringLever, crankR_SpringLever;
	public float minimumSpeed, fuelThreshold, fuelConsumption;
	public Image fuelBar, fuelBarFurnace, speedBarL, reverseBarL, speedBarR, reverseBarR;
	[Header("Stats")]
	[Range(0,1)]
	public float fuel = 1;
	public float underThresholdClamp;
	public TMPro.TextMeshProUGUI display;
	public TMPro.TextMeshProUGUI lowFuel;
	public int lowFuelStage = 0;

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
	TutorialManager voiceover;
	public FlagTracker flags;
	public GameObject gameOverlay;

	[Header("Timer")]
	public TMPro.TextMeshProUGUI timer;
	public bool countdownActive = false;
	public float maxTime;
	public float timeRemaining;
	public AudioClip outOfTimeClip;

	// Use this for initialization
	void Start () {
		// Store initial values
		originalLightColor = normalLights [0].color;
		originalLightIntensity = normalLights [0].intensity;
		net = GetComponent<Tank_Network> ();
		timeRemaining = maxTime;
		voiceover = FindObjectOfType<TutorialManager>();
	}

	public void StartupTank(){
		engineSound.GetComponent<AudioSource>().Play();
		online = true;
		timeRemaining = maxTime;
		flags.ActivateAllFlags();
		gameOverlay.SetActive (false);
	}

	public void ShutdownTank(){
		engineSound.GetComponent<AudioSource>().Stop();
		online = false;
		countdownActive = false;
		timeRemaining = 0;
		voiceover.tutorialStage = -1;
		gameOverlay.SetActive (true);
		net.AllStop ();
	}
	
	// Update is called once per frame
	void Update () {
		if(!online){
			display.text = "Tank Offline";
			lowFuel.text = "OFFLINE";
			timer.text = "-:--";
			timer.color = Color.red;
			//net.AllStop ();
			return;
		}
		crankL = (-(crankL_SpringLever.GetNormalizedValue()/100.0f)+0.5f)*2.0f;
		crankR = (-(crankR_SpringLever.GetNormalizedValue()/100.0f)+0.5f)*2.0f;

		EngineAudio (); // Update Engine sounds

		if(fuel < fuelThreshold){
			underThresholdClamp = Mathf.Max(minimumSpeed, Mathf.InverseLerp(0, fuelThreshold, fuel));
			crankL = Mathf.Clamp(crankL, -underThresholdClamp, underThresholdClamp);
			crankR = Mathf.Clamp(crankR, -underThresholdClamp, underThresholdClamp);
		}
		fuel = Mathf.Max(0,fuel-(fuelConsumption*Time.deltaTime*0.001f*(Mathf.Abs(crankL)+Mathf.Abs(crankR)+0.1f)));

		CheckLowFuel (); // Update low fuel alarms

		display.text = "Left: " + (crankL * 100.0f).ToString("F2") + "\nRight: " + (crankR * 100.0f).ToString("F2") + "\nFuel: " + (fuel * 100.0f).ToString("F2");
		fuelBar.fillAmount = fuel;
		fuelBarFurnace.fillAmount = fuel;
		speedBarL.fillAmount = crankL;
		reverseBarL.fillAmount = -crankL;
		speedBarR.fillAmount = crankR;
		reverseBarR.fillAmount = -crankR;


		if(countdownActive){
			if(timeRemaining > 0){
				timeRemaining = Mathf.Max(timeRemaining - Time.deltaTime, 0);
			}
			else{
				countdownActive = false;
				voiceover.PlayClip(outOfTimeClip);
				ShutdownTank();
			}
		}

		if(timeRemaining < 10){
			timer.color = new Color(1,0,0,Mathf.Sin(Time.time*20));
		}
		string clockText = Mathf.Floor(timeRemaining / 60).ToString("0") + ":" + Mathf.Floor(timeRemaining % 60).ToString("00");
		timer.text = clockText;
	}

	void FixedUpdate(){
		if (!net.useNetwork | !online) {
			return;
		}

		net.SendSpeed (crankL * 100f, crankR * 100f);

	}

	// Change pitch and volume of Engine sound based on speed;
	void EngineAudio() {
		engineSound.SetPitch (crankL + crankR /2.0f); // Pitch

		engineSound.SetVolume (fuel /1.5f + 0.1f); // Volume
	}



	void CheckLowFuel() {
		

		// Check for fuel thresholds
		if (fuel < (fuelThreshold * 0.5f)) {
			lowFuelStage = 2;
			lowFuel.text = "FUEL CRITICAL";
		} else if (fuel < fuelThreshold) {
			lowFuelStage = 1;
			lowFuel.text = "LOW FUEL";
		} else {
			lowFuelStage = 0;
			lowFuel.text = "FUEL : " + fuel.ToString("P0");
		}

		switch (lowFuelStage) {
		case 1:
			alarmSound.SetVolume (0.0f); // set volume to low

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
			alarmSound.SetVolume (0.1f); // Sets volume to 1

			// turn alarm lights on if they aren't on
			if (!alarmLights [0].activeSelf)
				ToggleAlarmLights (true);

			// turn normal lights red if they aren't red 
			if (fuel <= 0 && !normalLights [0].color.Equals (dimmedRedLightColor)) {
				SetLightColour (dimmedRedLightColor);
			}

			break;

		case 0:
			// Normal Stage 0 settings
			// Turn alarm sound off if it is on
			if (alarmSound.IsPlaying)
				alarmSound.StopAlarm ();

			// Turn alarm lights off if it is on
			if (alarmLights [0].activeSelf)
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
