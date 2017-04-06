using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoalPile : MonoBehaviour {

	public Shovel shovelScript;
	public float maxAngle = 75f;
	public float giveMultiplier = 0.5f;
	public float minVelocty = 1.5f;
	float giveAmount = 0.0f;
	float magnitude = 0.0f;

	Vector3 vel = new Vector3();
	Vector3 projection = new Vector3();
	Vector3 otherPos = new Vector3();
	float angle = 0.0f;

	void Update() {
		Debug.DrawLine (projection + this.transform.position, this.transform.position, Color.blue);
		Debug.DrawLine (otherPos + this.transform.position, this.transform.position, Color.red);
		Debug.DrawLine (vel.normalized + this.transform.position, this.transform.position, Color.green);
		
//		Debug.DrawRay (otherPos, vel, Color.green);
	}

	void OnTriggerEnter(Collider other) {
		if (other.CompareTag("Shovel")) {
			// Convert to local
			Debug.Log("rotation: " + Vector3.Angle(other.transform.rotation.eulerAngles, Vector3.up));
			Rigidbody attach = other.transform.parent.GetComponent<Rigidbody> ();
			if (attach) {
				Debug.Log ("attached: " + attach.velocity);
			}
//			otherPos = other.transform.position;
//			otherPos = this.transform.InverseTransformPoint (otherPos);
//			Debug.Log ("rel shovel pos: " + otherPos);
//			vel = shovelScript.rb.velocity;
//			Debug.Log ("vel: " + vel);
//			vel = this.transform.InverseTransformPoint (vel);
//			Debug.Log ("rel vel: " + vel);
//			projection = new Vector3 (-otherPos.x, 0f, -otherPos.z);
//			Debug.Log ("rel projection: " + projection);
//			float angle = Vector3.Angle (projection.normalized, vel.normalized);
//			Debug.Log ("Shovel angle: " + angle);

			// Check if entry angle is greater than max
			if (angle <= maxAngle) {
				// Check if shovel is upsidedown
				if (!shovelScript.isUpsideDown) {
					magnitude = shovelScript.velocity.magnitude * giveMultiplier;
//			Debug.Log("initial vel: " + shovelScript.velocity.magnitude + " with multiplier: " + magnitude);
					giveAmount = Mathf.Clamp (magnitude, 0, shovelScript.maxAmount);
					if (giveAmount > shovelScript.coalAmount && magnitude > minVelocty) {
						shovelScript.coalAmount = giveAmount;
					}
				}
			}
		}
	}
}
