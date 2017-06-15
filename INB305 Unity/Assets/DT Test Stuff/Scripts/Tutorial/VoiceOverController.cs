using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoiceOverController : MonoBehaviour {

	AudioSource audioSource;
	float clipWaitBuffer = 1f;

	public AudioClip[] voiceOverClips;

	public AudioClip startVoiceOver;

	public AudioClip[] controlClips;

	Tutorial tutorialScript;

	void Start() {
		tutorialScript = GetComponent<Tutorial> ();
	}

	public void PlayClipAtStage(int stage) {
		switch (stage) {
		case 1:
			PlayAudioClip (startVoiceOver);
			break;
		case 2:
			PlayAudioClip (controlClips [0]);
			break;
		case 3:
			break;
		}
	}

	void PlayAudioClip(AudioClip clip) {
		audioSource.Stop ();
		audioSource.clip = clip;
		audioSource.Play ();
		StopCoroutine (WaitForClip (clip));
		StartCoroutine (WaitForClip (clip));
		tutorialScript.audioSourcePlaying = true;
	}

	IEnumerator WaitForClip(AudioClip clip) {
		yield return new WaitForSeconds (clip.length + clipWaitBuffer);

		tutorialScript.AudioDonePlaying ();
	}
}
