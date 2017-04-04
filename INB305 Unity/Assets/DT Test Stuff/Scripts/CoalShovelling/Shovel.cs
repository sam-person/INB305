using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shovel : MonoBehaviour {

	public float coalAmount = 0.0f;
	public float maxAmount;

	public bool isUpsideDown = false;


	Vector3 prev = new Vector3();
	Vector3 next = new Vector3();
	public Vector3 velocity = new Vector3();

	public ShovelTip tipScript;
	public Transform shovelTip;

	void Start() {
		next = this.transform.position;
	}

	void Update() {

		CalculateVelocity ();

		if (Input.GetKeyDown (KeyCode.P)) {
			coalAmount = 0.0f;
		}

		if (!tipScript.IsInCoal) {
			CheckXZAngle ();
		}

		CheckUpAngle ();
	}

	void CalculateVelocity() {
		prev = next;
		next = this.transform.position;

		velocity = (next - prev) / Time.deltaTime;
	}

	void CheckXZAngle() {
		if (65f > this.transform.eulerAngles.y || this.transform.eulerAngles.y > (360f - 65)) {
			// lose coal based on angle amount
			// need to get angle and take from boundaries
			// then divide by something, multiply by time.delta (implies timer)
			// then take from coal amount
			LoseCoal (1);
		} else if (300 > this.transform.eulerAngles.z && this.transform.eulerAngles.z > 230f) {
			LoseCoal (1);
		}
	}

	void CheckYAngle() {
		if (80f > this.transform.eulerAngles.y || this.transform.eulerAngles.y > (360f - 80)) {
			isUpsideDown = true;
			LoseCoal (maxAmount);
		}
	}

	void CheckUpAngle() {
		if (Vector3.Angle (this.transform.up, Vector3.up) > 90f) {
			isUpsideDown = true;
			LoseCoal (maxAmount);
		} else {
			isUpsideDown = false;
		}
	}

	// coal falling out of shovel
	void LoseCoal(float amount) {
		coalAmount -= amount;
		coalAmount = Mathf.Clamp (coalAmount, 0f, maxAmount);
	}
}
