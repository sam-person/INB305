using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeTank_Manager : MonoBehaviour {
	public float speed, rotation;
	[Header("Settings")]
	public VRTK.VRTK_SpringLever crank;
	public VRTK.VRTK_Wheel wheel;
	public float minimumSpeed, fuelThreshold, fuelConsumption;
	[Header("Stats")]
	[Range(0,1)]
	public float fuel = 1;
	public float underThresholdClamp;

	public TMPro.TextMeshPro display;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		speed = (-(crank.GetNormalizedValue()/100.0f)+0.5f)*2.0f;
		if(fuel < fuelThreshold){
			underThresholdClamp = Mathf.Max(minimumSpeed,Mathf.InverseLerp(0,fuelThreshold, fuel));
			speed = Mathf.Clamp(speed, -underThresholdClamp, underThresholdClamp);
		}
		rotation = -((wheel.GetNormalizedValue()/100.0f)-0.5f);
		fuel = Mathf.Max(0,fuel-(fuelConsumption*Time.deltaTime*0.001f));

		display.text = "Speed: " + (speed * 100.0f).ToString("F2") + "\nRotation: " + (rotation * 100.0f).ToString("F2") + "\nFuel: " + (fuel * 100.0f).ToString("F2");
	}


}
