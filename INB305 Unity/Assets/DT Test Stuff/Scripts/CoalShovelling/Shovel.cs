﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shovel : MonoBehaviour {

	// Meta
	public float coalAmount = 0.0f;
	public float maxAmount = 20f;

	// Tilt detection
	public bool isUpsideDown = false;
	public bool isTipping = false;
	float lossRate = 4f;
	Vector2 Xmin = new Vector2 (320f, 40f);
	Vector2 Xmax = new Vector2 (290f, 70f);
	Vector2 Zmin = new Vector2 (335f, 25f);
	Vector2 Zmax = new Vector2 (280f, 80f);

	// Velocity
	Vector3 prev = new Vector3();
	Vector3 next = new Vector3();
	public Vector3 velocity = new Vector3();

	// Reference
	public ShovelTip shovelTipScript;
	public Transform shovelTip;
	public Furnace furnaceScript;

	void Start() {
		next = this.transform.position;
	}

	void Update() {
		CalculateVelocity ();

		if (Input.GetKeyDown (KeyCode.P)) {
			coalAmount = 0.0f;
		}

		if (!shovelTipScript.isInCoal) {

			CheckXZAngle ();
		}

		CheckUpAngle ();
	}

	void CalculateVelocity() {
		prev = next;
		next = this.transform.position;

		velocity = (next - prev) / Time.deltaTime;
//		Debug.Log ("shovel script calc vel: " + velocity);
	}

	void CheckXZAngle() {

		if (Xmin.x > this.transform.eulerAngles.x && this.transform.eulerAngles.x > Xmin.y) {
			LossCalculation (this.transform.eulerAngles.x, Xmax);
		} else if (Zmin.x > this.transform.eulerAngles.z && this.transform.eulerAngles.z > Zmin.y) {
			LossCalculation (this.transform.eulerAngles.z, Zmax);
		} else {
			isTipping = false;
		}

	}

	void LossCalculation(float angle, Vector2 max) {
		float lossAmount = 0.0f;

		isTipping = true;
		if (max.x > angle && angle > max.y) {
			lossAmount = lossRate * Time.deltaTime;
		} else {
			if (angle > 180f) {
				angle = Mathf.Abs (angle - 360f);
			}
			lossAmount = lossRate * angle / max.y * Time.deltaTime;
		}

		if (shovelTipScript.isInFurnace) {
			furnaceScript.AcceptFuel (lossAmount);
		}

		LoseCoal (lossAmount);
	}

	void CheckUpAngle() {
		if (Vector3.Angle (this.transform.up, Vector3.up) > 80f) {
			isUpsideDown = true;
			if (shovelTipScript.isInFurnace) {
				furnaceScript.AcceptFuel (maxAmount);
			}
			LoseCoal (maxAmount);
		} else {
			isUpsideDown = false;
		}
	}

	// coal falling out of shovel
	void LoseCoal(float amount) {
//		Debug.Log ("Losing coal: " + amount);
		coalAmount = Mathf.Clamp (coalAmount - amount, 0f, maxAmount);
	}
}
