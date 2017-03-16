using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSInteract : MonoBehaviour {

	public float reach;
	public float rotationSpeed;
	public Steering steeringScript;

	// Raycasts
	RaycastHit[] hits;
	RaycastHit hit;
	Ray ray;
	Vector3 reference;
	Vector3 currentPoint;
	Vector3 rightOfRef;

	Quaternion originalRotation;
	Quaternion platformQ;
	GameObject platform;

	void Start() {
		originalRotation = GameObject.FindGameObjectWithTag ("Steering").transform.rotation;
		platform = GameObject.FindGameObjectWithTag ("Respawn");
		platformQ = platform.transform.rotation;
	}


	void Update() {
		Debug.DrawRay (ray.origin, ray.direction);

		if (Input.GetMouseButtonDown (0)) {
			ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			if (Physics.Raycast (ray, out hit, reach)) {
				if (hit.transform.CompareTag ("Steering")) {
//					reference = hit.point - hit.transform.position;
					steeringScript.InitialInteractPoint (hit.point);
				}
			}

		}
			
		if (Input.GetMouseButton (0)) {
			ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			if (Physics.Raycast (ray, out hit, reach)) {
				if (hit.transform.CompareTag ("Steering")) {
					steeringScript.PlayerRotating (hit.point);

//					currentPoint = hit.point - hit.transform.position;
//					rightOfRef = new Vector3 (reference.x, 0f, 1f);
//
//					rightOfRef.y = (0 - reference.z) / reference.y;
//
//					if (reference.y < 0) {
//						rightOfRef *= -1;
//					}
//
//					float angleRef = Vector3.Angle (rightOfRef, reference);
//					float angleCur = Vector3.Angle (rightOfRef, currentPoint);
//
//					hit.transform.RotateAround (hit.transform.position, hit.transform.right, angleRef - angleCur);
////					hit.transform.localRotation.SetFromToRotation(reference, currentPoint);
//
//					reference = currentPoint;
				}
			}
		}

		if (Input.GetMouseButtonUp (0)) {
			ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			if (Physics.Raycast (ray, out hit, reach)) {
				if (hit.transform.CompareTag ("Steering")) {
//					hit.transform.localRotation.SetFromToRotation (hit.transform.localRotation.eulerAngles, Vector3.zero);
//					hit.transform.rotation = new Quaternion(originalRotation.x, platform.transform.rotation.y, platform.transform.rotation.z, platform.transform.rotation.w) ;
				}
			}
		}
	}
}
