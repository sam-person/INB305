using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerLineRenderer : MonoBehaviour {
	
	public bool render = false;
	LineRenderer lr;
	public Transform target;

	void Awake(){
		lr = GetComponent<LineRenderer>();
	}

	// Use this for initialization
	void Start () {
		setRender(false);
	}
	
	// Update is called once per frame
	void Update () {
		lr.SetPosition(0, transform.position);
		if(render){
			lr.SetPosition(1, target.position);
		}
	}

	public void setRender(bool doRender){
		lr.enabled = doRender;
		render = doRender;
	}
}
