﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour {
	
	[System.Serializable]
	public class TutorialStage{
		public AudioClip voiceLine;
		public MeshRenderer[] meshes;
		public Transform leftHandTarget;
		public Transform rightHandTarget;

		public Material highlight;

		List<Material> oldMaterials;

		public void Setup(){
			if(meshes.Length == 0){
				return;
			}
			oldMaterials = new List<Material>();
			for(int i = 0; i < meshes.Length; i++){
				oldMaterials.Add(meshes[i].sharedMaterial);
			}
		}

		public void highlightMeshes(){
			if(meshes.Length == 0){
				return;
			}
			for(int i = 0; i < meshes.Length; i++){
				meshes[i].material = highlight;
			}
		}

		public void unHighlightMeshes(){
			for(int i = 0; i < meshes.Length; i++){
				meshes[i].material = oldMaterials[i];
			}
		}

	}

	[SerializeField]
	int tutorialStage = -1;

	public AudioSource voiceoverSource;
	public TutorialStage[] stages;
	public ControllerLineRenderer leftHandLine, rightHandLine;

	FakeTank_Manager tank;

	void Awake(){
		tank = FindObjectOfType<FakeTank_Manager>();
	}

	// Use this for initialization
	void Start () {
		foreach(TutorialStage s in stages){
			s.Setup();
		}
	}
	
	// Update is called once per frame
	void Update () {
		if(tutorialStage == -1 && Input.GetKeyDown(KeyCode.T)){
			tutorialStage = 0;
			StartStage(stages[0]);
		}

		switch(tutorialStage){
			case 0:
				if(!voiceoverSource.isPlaying){
					AdvanceStage();
				}
				break;
			case 1:
				if(!voiceoverSource.isPlaying && tank.crankL != 0 && tank.crankR != 0){
					AdvanceStage();
				}
				break;
		}
	}

	void AdvanceStage(){
		stages[tutorialStage].unHighlightMeshes();

		tutorialStage++;

		StartStage(stages[tutorialStage]);
	}

	void StartStage(TutorialStage stage){
		voiceoverSource.Stop();
		voiceoverSource.clip = stage.voiceLine;
		voiceoverSource.Play();

		SetTargetLines(stage);
		stage.highlightMeshes();
	}

	void SetTargetLines(TutorialStage stage){
		leftHandLine.setRender(false);
		if(stage.leftHandTarget != null){
			leftHandLine.setRender(true);
			leftHandLine.target = stage.leftHandTarget;
		}

		rightHandLine.setRender(false);
		if(stage.rightHandTarget != null){
			rightHandLine.setRender(true);
			rightHandLine.target = stage.rightHandTarget;
		}

	}
}
