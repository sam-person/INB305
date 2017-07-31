using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class FlagTracker : MonoBehaviour {

	public bool setup = false;

	public List<Vector3> flags;

	// Use this for initialization
	void Start () {
		GetComponent<VRTK_ControllerEvents>().TriggerUnclicked += new ControllerInteractionEventHandler(DoTriggerUnClicked);
	}

	void OnDrawGizmos () {
		foreach(Vector3 f in flags){
			Gizmos.DrawSphere(f, 0.1f);
		}
	}

	private void DoTriggerUnClicked(object sender, ControllerInteractionEventArgs e){
		if(!setup){
			return;
		}

		Debug.Log("Placing a flag at " + transform.position.ToString());
		flags.Add(transform.position);



    }
}
