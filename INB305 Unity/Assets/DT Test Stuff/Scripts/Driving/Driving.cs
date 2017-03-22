using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Driving : MonoBehaviour {

	public GameObject steering;
	public GameObject crank;

	public float baseSpeed;
	public float rotationSpeed;
	float currentSpeed = 0.0f;


	void Update() {
//		Debug.Log ("Rotation: " + steering.transform.rotation.x);

		if (steering.transform.rotation.eulerAngles.x > 5 && steering.transform.rotation.eulerAngles.x < 90) {
//			Debug.Log ("Angle: " + steering.transform.rotation.eulerAngles.x);
			this.transform.Rotate (new Vector3 (0f, rotationSpeed, 0f));
		} else if (steering.transform.rotation.eulerAngles.x < 355 && steering.transform.rotation.eulerAngles.x > 270) {
//			Debug.Log ("Angle: " + steering.transform.rotation.eulerAngles.x);
			this.transform.Rotate (new Vector3 (0f, -rotationSpeed, 0f));
		}

		if (crank.transform.rotation.eulerAngles.z >= 5 && crank.transform.rotation.eulerAngles.z <= 50f) {
			currentSpeed = baseSpeed * (Mathf.Clamp (crank.transform.rotation.eulerAngles.z, 0f, 30f) / 30f);
//			Debug.Log ("speed: " + baseSpeed + " current: " + currentSpeed + " multiplier: " + (Mathf.Clamp (crank.transform.rotation.eulerAngles.z, 0f, 30f) / 30f));
		} else if (crank.transform.rotation.eulerAngles.z <= 355 && crank.transform.rotation.eulerAngles.z >= 300) {
			currentSpeed = baseSpeed * (Mathf.Clamp (crank.transform.rotation.eulerAngles.z - 360f, -30f, 0f) / 30f);

//			Debug.Log ("multiplier: " + (Mathf.Clamp (crank.transform.rotation.eulerAngles.z - 360f, -30f, 0f) / 30f) + " -360: " + (crank.transform.rotation.eulerAngles.z - 360f));
		} else {
			currentSpeed = 0.0f;
		}


//		Debug.Log ("actual speed: " + Vector3.forward * currentSpeed);
		this.transform.position += -this.transform.right * currentSpeed * Time.deltaTime;
	}
}
