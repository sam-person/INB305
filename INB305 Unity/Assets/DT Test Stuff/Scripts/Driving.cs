using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Driving : MonoBehaviour {

	public GameObject steering;

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

	}
}
