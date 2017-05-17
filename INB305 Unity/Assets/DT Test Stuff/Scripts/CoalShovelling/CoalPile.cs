using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoalPile : MonoBehaviour {

	public Shovel shovelScript;
	public float maxAngle = 75f;
	public float giveMultiplier = 5f;
	public float minVelocty = 1.5f;
	float giveAmount = 0.0f;
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

	void Update() {
		Debug.DrawLine (projection + this.transform.position, this.transform.position, Color.blue);
		Debug.DrawLine (otherPos + this.transform.position, this.transform.position, Color.red);
		Debug.DrawLine (vel.normalized + this.transform.position, this.transform.position, Color.green);
		
//		Debug.DrawRay (otherPos, vel, Color.green);
	}

	void OnTriggerEnter(Collider other) {
		if (other.CompareTag("Shovel")) {
			// Convert to local
//			Debug.Log("rotation: " + Vector3.Angle(other.transform.rotation.eulerAngles, Vector3.up));
//			Rigidbody attach = other.transform.parent.GetComponent<Rigidbody> ();
//			if (attach) {
//				Debug.Log ("attached: " + attach.velocity);
//			}

			otherPos = this.transform.InverseTransformPoint (other.transform.position);
//			Debug.Log ("rel shovel pos: " + otherPos);
			vel = shovelScript.velocity;
//			Debug.Log ("vel: " + vel);
			projection = new Vector3 (-otherPos.x, 0f, -otherPos.z);
//			Debug.Log ("rel projection: " + projection);
			angle = Vector3.Angle (projection.normalized, vel.normalized);
//			Debug.Log ("Shovel angle: " + angle);

			// Check if entry angle is greater than max
			if (angle <= maxAngle) {
				// Check if shovel is upsidedown
				if (!shovelScript.isUpsideDown) {
					magnitude = vel.magnitude * giveMultiplier;
//					Debug.Log ("initial vel: " + vel.magnitude + " with multiplier " + giveMultiplier + ": " + magnitude);
					giveAmount = Mathf.Clamp (magnitude, 0, shovelScript.maxAmount);
					if (magnitude > minVelocty) {
						shovelScript.coalAmount = giveAmount;
						shovelScript.LoseCoal (0f);

						PlayAudioSounds ();
					}
				}

			} else {
				Debug.Log ("Unacceptable angle: " + angle);
			}
		}
	}

	void PlayAudioSounds() {
		if (!shovelTipAudioSource.isPlaying) {
			shovelTipAudioSource.pitch = 1 + Random.Range (-0.05f, 0.05f);
			shovelTipAudioSource.volume = 1f * magnitude / 10f;
			shovelTipAudioSource.clip = shovelHitCoalSound;
			shovelTipAudioSource.Play ();
		}

		if (!coalAudio.isPlaying) {
			coalAudio.pitch = 1f + Random.Range (-0.1f, 0.1f);
			coalAudio.volume = 1f * magnitude / 30f;
			coalAudio.Play ();
		}
	}
}
