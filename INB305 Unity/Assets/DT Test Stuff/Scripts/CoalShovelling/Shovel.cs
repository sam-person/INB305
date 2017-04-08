using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shovel : MonoBehaviour {

	public float coalAmount = 0.0f;
	public float maxAmount = 20f;

	public bool isUpsideDown = false;
	float lossRate = 4f;
	Vector2 Xmin = new Vector2 (320f, 40f);
	Vector2 Xmax = new Vector2 (290f, 70f);

	Vector2 Zmin = new Vector2 (335f, 25f);
	Vector2 Zmax = new Vector2 (280f, 80f);

	// Velocity
	Vector3 prev = new Vector3();
	Vector3 next = new Vector3();
	public Vector3 velocity = new Vector3();

	public ShovelTip tipScript;
	public Transform shovelTip;

	public Rigidbody rb;

	void Start() {
		next = this.transform.position;
		rb = GetComponent<Rigidbody> ();
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
//		Debug.Log ("shovel script calc vel: " + velocity);
	}

	void CheckXZAngle() {
		float angle = 0.0f;
		if (Xmin.x > this.transform.eulerAngles.x && this.transform.eulerAngles.x > Xmin.y) {
			// lose coal based on angle amount
			// need to get angle and take from boundaries
			// then divide by something, multiply by time.delta (implies timer)
			// then take from coal amount

			if (Xmax.x > this.transform.eulerAngles.x && this.transform.eulerAngles.x > Xmax.y) {
				LoseCoal (lossRate * Time.deltaTime);
			} else {
				angle = this.transform.eulerAngles.x;
				if (angle > 180f) {
					angle = Mathf.Abs (angle - 360f);
				}

				LoseCoal (lossRate * angle / Xmax.y * Time.deltaTime);
			}

		} else if (Zmin.x > this.transform.eulerAngles.z && this.transform.eulerAngles.z > Zmin.y) {

			if (Zmax.x > this.transform.eulerAngles.z && this.transform.eulerAngles.z > Zmax.y) {
				LoseCoal (lossRate * Time.deltaTime);
			} else {
				angle = this.transform.eulerAngles.z;
				if (angle > 180f) {
					angle = Mathf.Abs (angle - 360f);
				}

				LoseCoal (lossRate * angle / Zmax.y * Time.deltaTime);
			}
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
//		Debug.Log ("Losing coal: " + amount);
		coalAmount = Mathf.Clamp (coalAmount - amount, 0f, maxAmount);
	}
}
