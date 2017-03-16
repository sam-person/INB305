using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Steering : MonoBehaviour {

	bool rotating = false;
	float rotateTimer = 0.0f;
	float rotateTimeOut = 0.2f;

	Vector3 relativeInitialInteractPoint = new Vector3();
	Vector3 relativeInteractPoint = new Vector3();
	Vector3 rightOfInteractPoint = new Vector3();
	float storedAngle = 0.0f;

	void Update() {
		if (rotating) {
			rotateTimer += Time.deltaTime;

			if (rotateTimer >= rotateTimeOut) {
				rotating = false;
			}

		} else {
			this.transform.localRotation = Quaternion.identity;
		}

		if (Input.GetKey (KeyCode.P)) {
//			Test ();
			rotating = true;
			if (this.transform.rotation.eulerAngles.x < 30 || this.transform.rotation.eulerAngles.x > 340)
				this.transform.Rotate (new Vector3 (10f, 0f, 0f));
			
		}
		if (Input.GetKey (KeyCode.O)) {
			rotating = true;
			if (this.transform.rotation.eulerAngles.x > -30 && this.transform.rotation.eulerAngles.x < 350)
				this.transform.Rotate (new Vector3 (-10f, 0f, 0f));

		}
	}

	// Initial Function need to be called before INteracting
	// Gets the initial point of interaction for reference.
	public void InitialInteractPoint(Vector3 initialPoint) {
		relativeInitialInteractPoint = initialPoint - this.transform.position;
	}

	// Called when player is interacting with the steering
	// rotates steering based on the point of interaction
	public void PlayerRotating(Vector3 interactPoint) {
		rotating = true;
		rotateTimer = 0.0f;
		relativeInteractPoint = interactPoint - this.transform.position;

		rightOfInteractPoint = new Vector3 (0f, 0f, 1f);

		rightOfInteractPoint.y = (0 - relativeInitialInteractPoint.z) / relativeInitialInteractPoint.y;

		if (relativeInitialInteractPoint.y < 0) {
			rightOfInteractPoint *= -1;
		}

//		Debug.Log ("interact: " + relativeInteractPoint + " ref: " + relativeInitialInteractPoint + " right: " + rightOfInteractPoint + " | Dot: " + Vector3.Dot (rightOfInteractPoint, relativeInitialInteractPoint));

//		Debug.Log ("Dot: " + Vector3.Dot (rightOfInteractPoint, relativeInitialInteractPoint));

		float angleRef = Vector3.Angle (rightOfInteractPoint, relativeInitialInteractPoint);
		float angleCur = Vector3.Angle (rightOfInteractPoint, relativeInteractPoint);

//		Debug.Log ("Angle ref: " + angleRef + " cur: " + angleCur);

//		this.transform.RotateAround (this.transform.position, this.transform.right, (angleRef - angleCur));

		Debug.Log ("Calc Angle: " + (angleRef - angleCur) + " ref: " + relativeInitialInteractPoint + " cur: " + relativeInteractPoint);
		this.transform.localRotation = Quaternion.Euler (new Vector3 ((angleRef - angleCur), 0f, 0f));
//		this.transform.localRotation.SetFromToRotation (this.transform.right, relativeInteractPoint);

	}
}
