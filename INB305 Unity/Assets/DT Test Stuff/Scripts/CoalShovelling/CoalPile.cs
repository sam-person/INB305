using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoalPile : MonoBehaviour {

	public Shovel shovelScript;
	public float giveMultiplier = 0.5f;
	public float minVelocty = 1.5f;
	float giveAmount = 0.0f;
	float magnitude = 0.0f;

	Vector3 vel = new Vector3();
	Vector3 projection = new Vector3();
	Vector3 otherPos = new Vector3();
	float angle = 0.0f;

	void Update() {
		Debug.DrawLine (projection, this.transform.position, Color.blue);
		Debug.DrawLine (otherPos, this.transform.position, Color.red);
		Debug.DrawLine (otherPos, vel.normalized, Color.green);
//		Debug.DrawRay (otherPos, vel, Color.green);
	}

	void OnTriggerEnter(Collider other) {
		if (other.CompareTag("Shovel")) {
			otherPos = other.transform.position;
			vel = shovelScript.velocity;
			projection = new Vector3 (other.transform.position.x, this.transform.position.y, other.transform.position.z);
			Debug.Log ("projection: " + Vector3.Angle (projection, this.transform.up));
			float angle = Vector3.Angle (projection, other.transform.position - shovelScript.velocity);
			Debug.Log ("Shovel angle: " + angle);

			magnitude = shovelScript.velocity.magnitude * giveMultiplier;
			Debug.Log("initial vel: " + shovelScript.velocity.magnitude + " with multiplier: " + magnitude);
			giveAmount = Mathf.Clamp(magnitude, 0, shovelScript.maxAmount);
			if (giveAmount > shovelScript.coalAmount && magnitude > minVelocty) {
				shovelScript.coalAmount = giveAmount;
			}
		}
	}
}
