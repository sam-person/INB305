using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EngineSound : MonoBehaviour {

	public AudioClip idle;
	public AudioClip driving;
	public AudioClip startIdle;
	public AudioClip startUp;

	AudioSource audioSource;
	bool transitioned = false;
	float originalVolume = 0.0f;

	// Use this for initialization
	void Start () {
		audioSource = GetComponent<AudioSource> ();
		originalVolume = audioSource.volume;
	}
	
//	void Update() {
//		if (Input.GetKeyDown (KeyCode.Alpha1)) {
//			TransitionToIdle ();
//		} else if (Input.GetKeyDown (KeyCode.Alpha2)) {
//			TransitionToDriving ();
//		} else if (Input.GetKeyDown (KeyCode.Alpha3)) {
//			audioSource.clip = startIdle;
//			audioSource.Play ();
//		} else if (Input.GetKeyDown (KeyCode.Alpha4)) {
//			audioSource.clip = startUp;
//			audioSource.Play ();
//		}
//	}

	public void TransitionToDriving() {
		if (!audioSource.clip.Equals (driving) && !transitioned) {
			StopCoroutine (TransitionClip ());
			PlayClip (driving);
			StartCoroutine (Transitioned ());
//			StartCoroutine (TransitionClip ());
		}
	}
	public void TransitionToIdle() {
		if (!audioSource.clip.Equals (idle) && !transitioned) {
			StopCoroutine (TransitionClip ());
			PlayClip (idle);
			StartCoroutine (Transitioned ());
		}
	}

	public void SetVolume(float percent) {
		audioSource.volume = originalVolume * percent;
	}

	void PlayClip(AudioClip clip) {
		audioSource.clip = clip;
		audioSource.Play ();
	}

	IEnumerator TransitionClip() {
		yield return new WaitForSeconds (startUp.length);
		audioSource.clip = driving;
		audioSource.Play ();
	}

	IEnumerator Transitioned() {
		transitioned = true;
		yield return new WaitForSeconds (0.5f);
		transitioned = false;
	}
}
