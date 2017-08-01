using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Manager : MonoBehaviour {

	public GameObject UI_Panel;
	public TutorialManager tutorial;
	public TMPro.TextMeshProUGUI statusText, tutorialstageText, toggleFlagText;
	public FlagTracker flagTracker;

	public List<RectTransform> UI_Flags;

	public RectTransform minimapPanel;
	public GameObject flagPrefab;

	public Vector2 MinimapSize;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Space)){
			UI_Panel.SetActive(!UI_Panel.activeSelf);
		}

		if(UI_Flags.Count - 1 < flagTracker.flags.Count){
			UI_Flags.Add(Instantiate(flagPrefab, minimapPanel).GetComponent<RectTransform>());
		}

		if(tutorial.tutorialStage >= 0){
			tutorialstageText.text = "Stage: " + tutorial.tutorialStage;
		}
		else{
			tutorialstageText.text = "";
		}

		if(flagTracker.setup){
			toggleFlagText.text = "Setup Active.\n" + flagTracker.flags.Count + " Flags Registered.";
		}
		else{
			toggleFlagText.text = "Setup Inactive.\n" + flagTracker.flags.Count + " Flags Registered.";
		}

		UI_Flags[0].anchoredPosition = getRelativePosition(flagTracker.tracker.transform.position);

		for(int i = 1; i < UI_Flags.Count; i++){
			UI_Flags[i].anchoredPosition = getRelativePosition(flagTracker.flags[i-1]);
		}

	}

	void DisplayMessage(string message){
		statusText.text = message;
	}

	public void StartTutorialButton(){
		if(tutorial.StartTutorial()){
			DisplayMessage("Tutorial started successfully!");
		}
		else{
			DisplayMessage("Tutorial failed to start - is tutorial already running?");
		}
	}

	public void ToggleFlagButton(){
		flagTracker.setup = !flagTracker.setup;
		if(flagTracker.setup){
			DisplayMessage("Setup Active. Pull the trigger on right hand controller to add flags.");
		}
		else{
			DisplayMessage("Setup Disabled.");
		}
	}

	Vector2 getRelativePosition(Vector3 input){
		float x, y;
		x = Mathf.Lerp(0,MinimapSize.x,flagTracker.GetPercentagePosition(input).x);
		y = Mathf.Lerp(0,MinimapSize.y,flagTracker.GetPercentagePosition(input).z);
		return new Vector2(x,y);
	}
}
