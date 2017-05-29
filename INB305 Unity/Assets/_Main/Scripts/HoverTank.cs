using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverTank : MonoBehaviour {

	public FakeTank_Manager controller;
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

		//float force = controller.speed * maxspeed;
		//body.AddRelativeForce(0, 0, force);
		//body.AddRelativeTorque(0, controller.rotation, 0);
	}
}
