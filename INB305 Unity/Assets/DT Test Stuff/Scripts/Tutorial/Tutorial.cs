﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour {

	[Tooltip("Start tutorial on when game Starts")]
	public bool startWithTutorial = true;
	[Tooltip("If true, press 'T' to start tutorial. Warning: will overlap tutorial if one is already in progress")]
	public bool canRestartTutorial = true;
	public float startWaitTime = 5f;

	MeshRenderer[] originalRenderers;
	Color[] originalColours;
	public Color highlightColour;

	public int tutorialStage = 1;
	public bool audioSourcePlaying = false;

	// Reference
	VoiceOverController voiceController;

	void Start() {
		voiceController = GetComponent<VoiceOverController> ();
		if (startWithTutorial) {
			StartTutorial ();
		}
	}

	void Update() {
		if (canRestartTutorial && Input.GetKeyDown (KeyCode.T)) {
			StartTutorial ();
		}

		TutorialStages ();
	}

	void StartTutorial() {
		canRestartTutorial = false;
		StartCoroutine (StartTutorialCoroutine ());
	}

	void TutorialStages() {

	}

	IEnumerator StartTutorialCoroutine() {
		yield return new WaitForSeconds (startWaitTime);
		voiceController.PlayClipAtStage (tutorialStage);
		
	}

	void HighlightMesh(MeshRenderer[] renderers) {
		originalRenderers = renderers;
		originalColours = new Color[renderers.Length];

		for (int i = 0; i < renderers.Length; i++) {
			originalColours [i] = renderers [i].material.color;

			renderers [i].material.color = highlightColour;
		}
	}

	void UnHighlight() {
		for (int i = 0; i < originalRenderers.Length; i++) {
			originalRenderers [i].material.color = originalColours [i];
		}
	}

	public void AudioDonePlaying() {
		audioSourcePlaying = false;
	}

	public void TriggerNextStage() {

	}
}
