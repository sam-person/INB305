using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Manager : MonoBehaviour {

	public GameObject UI_Panel;
	public TutorialManager tutorial;
	public TMPro.TextMeshProUGUI statusText, tutorialstageText;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Space)){
			UI_Panel.SetActive(!UI_Panel.activeSelf);
		}

		if(tutorial.tutorialStage >= 0){
			tutorialstageText.text = "Stage: " + tutorial.tutorialStage;
		}
		else{
			tutorialstageText.text = "";
		}

	}

	public void StartTutorialButton(){
		if(tutorial.StartTutorial()){
			statusText.text = "Tutorial started successfully!";
		}
		else{
			statusText.text = "Tutorial failed to start - is tutorial already running?";
		}
	}
}
