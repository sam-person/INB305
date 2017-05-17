using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlarmSound : MonoBehaviour {

	AudioSource source;
	public GameObject fuelLightObject;
	Light fuelLight;

	float originalIntensity = 1f;
	bool blinking = false;
	float blinkTime = 0.2f; //0.632f
//	float originalVolume = 0f;

	// Use this for initialization
	void Start () {
		source = GetComponent<AudioSource> ();
		fuelLight = fuelLightObject.GetComponent<Light> ();
		originalIntensity = fuelLight.intensity;
//		originalVolume = source.volume;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Alpha1))
			source.Play ();
	}

	public void SetVolume(float volume = 1f) {
		source.volume = volume;
	}

	public bool IsPlaying {
		get { return source.isPlaying; }
	}

	public void StartAlarm() {
		source.Play ();
		fuelLightObject.SetActive (true);
		fuelLight.intensity = originalIntensity;
		if (!blinking) {
			blinking = true;
			StartCoroutine (BlinkLight ());
		}
	}

	public void StopAlarm() {
		source.Stop ();
		fuelLightObject.SetActive (false);
		blinking = false;
		StopCoroutine (BlinkLight ());
	}

	IEnumerator BlinkLight() {
		yield return new WaitForSeconds (blinkTime);
		Debug.Log ("Started Blinking");
		while (blinking) {
			fuelLight.intensity = originalIntensity * 0.1f;
			yield return new WaitForSeconds (blinkTime - 0.03f);
			fuelLight.intensity = originalIntensity;
			yield return new WaitForSeconds (blinkTime + 0.03f);
		}
	}
}
