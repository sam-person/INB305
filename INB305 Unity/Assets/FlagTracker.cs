using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VRTK;

public class FlagTracker : MonoBehaviour {

	[System.Serializable]
	public class Flag{
		public Vector3 position;
		public bool active = true;
		public Flag(Vector3 position, bool active){
			this.position = position;
			this.active = active;
		}
	}

	public bool setup = false;

	public List<Flag> flags;

	public Vector3 lowerBound, higherBound;

	public GameObject tracker;

	public float threshold;

	public int nearFlag = -1;

	public GameObject uploadPanel;

	public Image uploadBar;

	public float uploadMaxTime, uploadTimer;

	public TMPro.TextMeshProUGUI uploadText;

	public AudioSource dialupSound;


	// Use this for initialization
	void Start () {
		GetComponent<VRTK_ControllerEvents>().TriggerUnclicked += new ControllerInteractionEventHandler(DoTriggerUnClicked);
	}

	void Update(){
		//check if we're near each flag
		nearFlag = -1;
		for(int i = 0; i < flags.Count; i++){
			if(Vector3.Distance(flags[i].position, new Vector3(tracker.transform.position.x, 0, tracker.transform.position.z)) < threshold){
				nearFlag = i;
			}
		}

		if(nearFlag == -1){
			dialupSound.Stop();
			uploadPanel.SetActive(false);
			uploadTimer = 0;
		}
		else{
			uploadPanel.SetActive(true);
			if(flags[nearFlag].active){
				if(uploadTimer == 0){
					dialupSound.Play();
				}
				uploadText.text = "Flag " + nearFlag.ToString() + " UPLOADING...";
				uploadTimer = Mathf.Min(uploadTimer + Time.deltaTime, uploadMaxTime);
				uploadBar.color = Color.red;
				uploadBar.fillAmount = Mathf.InverseLerp(0, uploadMaxTime, uploadTimer);
				if(uploadTimer == uploadMaxTime){
					flags[nearFlag].active = false;
				}
			}
			else{
				uploadText.text = "UPLOAD COMPLETE!";
				uploadTimer = uploadMaxTime;
				uploadBar.color = Color.green;
				uploadBar.fillAmount = 1;
			}
		}
	}

	void OnDrawGizmos () {
		Gizmos.color = Color.blue;
		foreach(Flag f in flags){
			Gizmos.DrawSphere(f.position, 0.05f);
		}

		if(flags.Count >= 2){
			Gizmos.color = Color.red;
			Gizmos.DrawLine(lowerBound, new Vector3(lowerBound.x, 0, higherBound.z));
			Gizmos.DrawLine(lowerBound, new Vector3(higherBound.x, 0, lowerBound.z));
			Gizmos.DrawLine(higherBound, new Vector3(lowerBound.x, 0, higherBound.z));
			Gizmos.DrawLine(higherBound, new Vector3(higherBound.x, 0, lowerBound.z));
		}
	}

	private void DoTriggerUnClicked(object sender, ControllerInteractionEventArgs e){
		if(!setup){
			return;
		}
		for(int i = 0; i < flags.Count; i++){
			if(Vector3.Distance(flags[i].position, transform.position) < threshold*2){
				Debug.Log("Can't place a flag within threshold of another flag");
				return;
			}
		}

		AddFlag(transform.position);
		GetComponent<VRTK_ControllerActions>().TriggerHapticPulse(1,0.5f,0.01f);
		Debug.Log("Placing a flag at " + transform.position.ToString() + " | Relative position: " + GetPercentagePosition(transform.position).ToString());


    }


    void AddFlag(Vector3 position){
		flags.Add(new Flag(new Vector3(position.x, 0, position.z), true));
		if(flags.Count >= 2){
			CalculateBounds();
		}
    }

    void CalculateBounds(){
    	float x, z; 

		//lower bounds
    	x = Mathf.Infinity;
    	z = Mathf.Infinity;
    	foreach(Flag v in flags){
    		if(v.position.x < x){
    			x = v.position.x;
    		}
			if(v.position.z < z){
				z = v.position.z;
    		}
    	}
    	lowerBound = new Vector3(x-0.5f,0,z-0.5f);

		//higher bounds
    	x = Mathf.NegativeInfinity;
    	z = Mathf.NegativeInfinity;
    	foreach(Flag v in flags){
			if(v.position.x > x){
				x = v.position.x;
    		}
			if(v.position.z > z){
				z = v.position.z;
    		}
    	}
    	higherBound = new Vector3(x+0.5f,0,z+0.5f);

    }

    public Vector3 GetPercentagePosition(Vector3 input){
    	if(flags.Count < 2){
    		return Vector3.zero;
    	}
    	else{
    		float x = Mathf.InverseLerp(lowerBound.x, higherBound.x, input.x);
			float z = Mathf.InverseLerp(lowerBound.z, higherBound.z, input.z);
			return new Vector3(x, input.y, z);
    	}
    }

    public void ResetFlags(){
    	flags.Clear();
    }

    public void ActivateAllFlags(){
    	foreach(Flag f in flags){
    		f.active = true;
    	}
    }

    public bool AllFlagsActive(){
    	bool a = true;
		foreach(Flag f in flags){
    		if(f.active){
    			a = false;
    		}
    	}
    	return a;
    }
}
