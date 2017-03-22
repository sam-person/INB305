using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crank : MonoBehaviour {

	public GameObject crankLever;
	Collider crankCollider;
	float crankColliderSizeX;
	public GameObject vehicle;

	Vector3 relativeInitialInteractPoint = new Vector3();
	Vector3 relativeInteractPoint = new Vector3();
	Vector3 rightOfInteractPoint = new Vector3();

	void Start() {
		crankCollider = crankLever.GetComponent<Collider> ();
		crankColliderSizeX = crankCollider.bounds.extents.x * 2;
	}

	void Update() {
		if (Input.GetKey (KeyCode.K)) {
			this.transform.Rotate (new Vector3 (0f, 0f, 5f));
		}

		if (Input.GetKey (KeyCode.L)) {
			this.transform.Rotate (new Vector3 (0f, 0f, -5f));
		}
	}

	// Initial Function need to be called before INteracting
	// Gets the initial point of interaction for reference.
	public void InitialInteractPoint(Vector3 initialPoint) {
		relativeInitialInteractPoint = this.transform.InverseTransformPoint (new Vector3(crankLever.transform.transform.position.x, initialPoint.y, initialPoint.z));
	}

	// Called when player is interacting with the steering
	// rotates steering based on the point of interaction
	public void PlayerRotating(Vector3 interactPoint) {
		relativeInteractPoint = this.transform.InverseTransformPoint(interactPoint);

		rightOfInteractPoint = new Vector3 (1f, 0f, 0f);

		rightOfInteractPoint.y = (0 - relativeInitialInteractPoint.x) / relativeInitialInteractPoint.y;

		if (relativeInitialInteractPoint.y < 0) {
			rightOfInteractPoint *= -1;
		}

		float angleRef = Vector3.Angle (rightOfInteractPoint, relativeInitialInteractPoint);
		float angleCur = Vector3.Angle (rightOfInteractPoint, relativeInteractPoint);

		this.transform.RotateAround (this.transform.position, vehicle.transform.forward, angleRef - angleCur);
	}
}
