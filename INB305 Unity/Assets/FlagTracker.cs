using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class FlagTracker : MonoBehaviour {

	public bool setup = false;

	public List<Vector3> flags;

	public Vector3 lowerBound, higherBound;

	public GameObject tracker;

	// Use this for initialization
	void Start () {
		GetComponent<VRTK_ControllerEvents>().TriggerUnclicked += new ControllerInteractionEventHandler(DoTriggerUnClicked);
	}

	void OnDrawGizmos () {
		Gizmos.color = Color.blue;
		foreach(Vector3 f in flags){
			Gizmos.DrawSphere(f, 0.05f);
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


		AddFlag(transform.position);
		GetComponent<VRTK_ControllerActions>().TriggerHapticPulse(1,0.5f,0.01f);

		Debug.Log("Placing a flag at " + transform.position.ToString() + " | Relative position: " + GetPercentagePosition(transform.position).ToString());


    }

    void AddFlag(Vector3 position){
		flags.Add(position);
		if(flags.Count >= 2){
			CalculateBounds();
		}
    }

    void CalculateBounds(){
    	float x, z; 

		//lower bounds
    	x = Mathf.Infinity;
    	z = Mathf.Infinity;
    	foreach(Vector3 v in flags){
    		if(v.x < x){
    			x = v.x;
    		}
    		if(v.z < z){
    			z = v.z;
    		}
    	}
    	lowerBound = new Vector3(x-0.5f,0,z-0.5f);

		//higher bounds
    	x = Mathf.NegativeInfinity;
    	z = Mathf.NegativeInfinity;
    	foreach(Vector3 v in flags){
    		if(v.x > x){
    			x = v.x;
    		}
    		if(v.z > z){
    			z = v.z;
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
}
