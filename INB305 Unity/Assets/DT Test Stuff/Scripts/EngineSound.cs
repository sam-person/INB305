using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EngineSound : MonoBehaviour {

	// Controller for the Engine audio

	AudioSource audioSource;

	float originalVolume = 0.0f;
	// Thresholds for pitch
	public Vector2 pitchThreshold = new Vector2();

	// Use this for initialization
	void Start () {
		audioSource = GetComponent<AudioSource> ();

		// Store initial volume
		originalVolume = audioSource.volume;
	}

	// Sets the pitch of this audio source
	public void SetPitch(float speed) {
		// Lerps pitch based on given speed between the pitch thresholds
		audioSource.pitch = Mathf.Lerp (pitchThreshold.x, pitchThreshold.y, Mathf.Abs (speed));
	}

	// Sets the volume of this audio source given the percent
	public void SetVolume(float percent) {
		audioSource.volume = originalVolume * percent;
	}
}
