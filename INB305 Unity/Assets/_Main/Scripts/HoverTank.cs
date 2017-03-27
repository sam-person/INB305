using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverTank : MonoBehaviour {

	public VRTK.VRTK_SpringLever crank;
	public VRTK.VRTK_Wheel wheel;
	public float hoverheight, hoverforce, maxspeed, turnrate;

	private Rigidbody body;

	// Use this for initialization
	void Awake () {
		body = GetComponent<Rigidbody>();
	}


	void FixedUpdate(){
		Ray ray = new Ray(transform.position, -transform.up);
		RaycastHit hit;

		if(Physics.Raycast(ray, out hit, hoverheight)){
			float height = (hoverheight - hit.distance) / hoverheight;
			Vector3 appliedHoverForce = Vector3.up * height * hoverforce;
			body.AddForce(appliedHoverForce, ForceMode.Acceleration);
		}

		body.AddRelativeForce(0, 0, -(crank.GetValue()/100.0f) * maxspeed);
		Debug.Log("Force " + (crank.GetValue()/100.0f) * maxspeed);
		body.AddRelativeTorque(0, (wheel.GetValue()/100.0f) * turnrate * (crank.GetNormalizedValue()/100.0f), 0);
		Debug.Log("Torque " + -(wheel.GetValue()/100.0f) * turnrate * Mathf.Abs((crank.GetNormalizedValue()/100.0f)));
	}
}
