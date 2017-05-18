using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlarmSound : MonoBehaviour {

	AudioSource source;
	public GameObject fuelLightObject;
	Light fuelLight;

	// Blinking light
	float originalIntensity = 1f;
	bool blinking = false;
	float blinkTime = 0.2f; // how fast the light blinkns

	// Use this for initialization
	void Start () {
		source = GetComponent<AudioSource> ();
		fuelLight = fuelLightObject.GetComponent<Light> ();

		// Store intial value
		originalIntensity = fuelLight.intensity;
	}

	// Sets volume of the audio source
	public void SetVolume(float volume = 1f) {
		source.volume = volume;
	}

	// Checks if the audio source is playing
	public bool IsPlaying {
		get { return source.isPlaying; }
	}

	// Settings to start the alarm
	public void StartAlarm() {
		source.Play ();

		// Turn on fuel light
		fuelLightObject.SetActive (true);
		fuelLight.intensity = originalIntensity;

		// Blink light if it is not on
		if (!blinking) {
			blinking = true;
			StartCoroutine (BlinkLight ());
		}
	}

	// Settings to stop the alarm
	public void StopAlarm() {
		source.Stop ();
		fuelLightObject.SetActive (false);
		blinking = false;
		StopCoroutine (BlinkLight ());
	}

	// Blink the light repeatedly
	IEnumerator BlinkLight() {
		yield return new WaitForSeconds (blinkTime);

		while (blinking) {
			fuelLight.intensity = originalIntensity * 0.1f;
			yield return new WaitForSeconds (blinkTime - 0.03f);
			fuelLight.intensity = originalIntensity;
			yield return new WaitForSeconds (blinkTime + 0.03f);
		}
	}
}
