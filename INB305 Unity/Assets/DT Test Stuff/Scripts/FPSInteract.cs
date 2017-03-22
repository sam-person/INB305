using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSInteract : MonoBehaviour {

	public float reach;

	// Steering
	public Steering steeringScript;

	// Crank
	public Crank crankScript;

	// Raycasts
	RaycastHit hit;
	Ray ray;


	void Update() {
		Debug.DrawRay (ray.origin, ray.direction);

		if (Input.GetMouseButtonDown (0)) {
			ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			if (Physics.Raycast (ray, out hit, reach)) {
				if (hit.transform.CompareTag ("Steering")) {
					steeringScript.InitialInteractPoint (hit.point);
				} else if (hit.transform.CompareTag ("Crank")) {
					crankScript.InitialInteractPoint (hit.point);
				}
			}

		}
			
		if (Input.GetMouseButton (0)) {
			ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			if (Physics.Raycast (ray, out hit, reach)) {
				if (hit.transform.CompareTag ("Steering")) {
					steeringScript.PlayerRotating (hit.point);
				} else if (hit.transform.CompareTag ("Crank")) {
					crankScript.PlayerRotating (hit.point);
				}
			}
		}
	}
}
