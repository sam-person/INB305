using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Manager : MonoBehaviour {

	public GameObject UI_Panel;
	public TutorialManager tutorial;
	public TMPro.TextMeshProUGUI statusText, tutorialstageText, toggleFlagText, networkText;
	public FlagTracker flagTracker;
	public GameObject flagPrefab;
	public Tank_Network network;

	[Header("Minimap 1")]
	public List<RectTransform> UI_Flags;
	public RectTransform minimapPanel;
	public Vector2 MinimapSize;

	[Header("Minimap 2")]
	public List<RectTransform> UI_Flags2;
	public RectTransform minimapPanel2;
	public Vector2 MinimapSize2;

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

		if(UI_Flags2.Count - 1 < flagTracker.flags.Count){
			UI_Flags2.Add(Instantiate(flagPrefab, minimapPanel2).GetComponent<RectTransform>());
			UI_Flags2[UI_Flags2.Count-1].GetComponent<RectTransform>().localScale = new Vector3(1,1,1);
			UI_Flags2[UI_Flags2.Count-1].GetComponent<RectTransform>().position = minimapPanel2.position;
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


		//1
		UI_Flags[0].anchoredPosition = getRelativePosition(flagTracker.tracker.transform.position, MinimapSize);

		for(int i = 1; i < UI_Flags.Count; i++){
			UI_Flags[i].anchoredPosition = getRelativePosition(flagTracker.flags[i-1], MinimapSize);
		}

		//2
		UI_Flags2[0].anchoredPosition = getRelativePosition(flagTracker.tracker.transform.position, MinimapSize2);

		for(int i = 1; i < UI_Flags2.Count; i++){
			UI_Flags2[i].anchoredPosition = getRelativePosition(flagTracker.flags[i-1], MinimapSize2);
		}

		if(!network.useNetwork){
			networkText.text = "Tank not connected - networking disabled!";
		}
		else{
			if(network.connected){
				networkText.text = "Tank connected!";
			}
			else{
				networkText.text = "Warning: Tank Failed to Connect!";
			}
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

	public void ResetFlagButton(){
		flagTracker.ResetFlags();
		RectTransform tank1, tank2;
		tank1 = UI_Flags[0];
		tank2 = UI_Flags2[0];
		for(int i = 1; i < UI_Flags.Count; i++){
			Destroy(UI_Flags[i].gameObject);
			Destroy(UI_Flags2[i].gameObject);
		}
		UI_Flags.Clear();
		UI_Flags2.Clear();
		UI_Flags.Add(tank1);
		UI_Flags2.Add(tank2);
		DisplayMessage("Flags Reset.");
	}

	Vector2 getRelativePosition(Vector3 input, Vector2 size){
		float x, y;
		x = Mathf.Lerp(0,size.x,flagTracker.GetPercentagePosition(input).x);
		y = Mathf.Lerp(0,size.y,flagTracker.GetPercentagePosition(input).z);
		return new Vector2(x,y);
	}
}
