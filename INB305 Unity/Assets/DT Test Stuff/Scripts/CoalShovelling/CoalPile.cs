using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoalPile : MonoBehaviour {

	// Stats
	public Shovel shovelScript;
	public float maxAngle = 75f;
	public float giveMultiplier = 5f;
	public float minVelocty = 1.5f;
	float magnitude = 0.0f;

	// Angle Calculations
	Vector3 vel = new Vector3();
	Vector3 projection = new Vector3();
	Vector3 otherPos = new Vector3();
	float angle = 0.0f;

	// Audio
	AudioSource coalAudio;
	public AudioSource shovelTipAudioSource;
	public AudioClip shovelHitCoalSound;

	void Start() {
		coalAudio = GetComponent<AudioSource> ();
	}

//	void Update() {
//		// Debugging
//		Debug.DrawLine (projection + this.transform.position, this.transform.position, Color.blue);
//		Debug.DrawLine (otherPos + this.transform.position, this.transform.position, Color.red);
//		Debug.DrawLine (vel.normalized + this.transform.position, this.transform.position, Color.green);
//	}

	void OnTriggerEnter(Collider other) {
		if (other.CompareTag("Shovel")) {
			// Store and convert shovel coordinate at impact
			otherPos = this.transform.InverseTransformPoint (other.transform.position);

			// Store the velocity of the shovel at impact
			vel = shovelScript.velocity;

			// project the local shovel coordinate at impact to local XZ plane
			projection = new Vector3 (-otherPos.x, 0f, -otherPos.z);

			// Calculate angle between the projection and velocity of shovel at impact
			angle = Vector3.Angle (projection.normalized, vel.normalized);

			// Check if entry angle is greater than max
			if (angle <= maxAngle) {
				
				// Check if shovel is upsidedown
				if (!shovelScript.isUpsideDown) {
					magnitude = vel.magnitude * giveMultiplier; // scale the magnitude

					// Check if magnitude is greater than velocity
					if (magnitude > minVelocty) {
						// Successfully got some coal
						shovelScript.coalAmount = shovelScript.maxAmount;
						shovelScript.LoseCoal (0f); // This is for resetting the scaled object on the shovel to show

						PlayAudioSounds ();
					}
				}

			} else {
				Debug.Log ("Unacceptable angle: " + angle);
			}
		}
	}

	// Play shoveling sound when shoveling
	void PlayAudioSounds() {
		// Check if audio source is playing (so sound doesn't cut off)
		if (!shovelTipAudioSource.isPlaying) {
			shovelTipAudioSource.pitch = 1 + Random.Range (-0.05f, 0.05f); // Randomise pitch
			shovelTipAudioSource.volume = 1f * magnitude / 10f; // Scale volume
			shovelTipAudioSource.clip = shovelHitCoalSound; // Set clip
			shovelTipAudioSource.Play ();
		}

		if (!coalAudio.isPlaying) {
			coalAudio.pitch = 1f + Random.Range (-0.1f, 0.1f); // Randomise pitch
			coalAudio.volume = 1f * magnitude / 30f; // Scale volume
			coalAudio.Play ();
		}
	}
}
