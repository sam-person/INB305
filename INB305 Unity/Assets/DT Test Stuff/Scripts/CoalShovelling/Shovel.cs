using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shovel : MonoBehaviour {

	public float coalAmount = 0.0f;
	public float maxAmount;

	Vector3 prev = new Vector3();
	Vector3 next = new Vector3();
	public Vector3 velocity = new Vector3();


	void Start() {
		next = this.transform.position;
	}

	void Update() {

		CalculateVelocity ();

		if (Input.GetKeyDown (KeyCode.P)) {
			coalAmount = 0.0f;
		}
	}

	void CalculateVelocity() {
		prev = next;
		next = this.transform.position;

		velocity = (next - prev) / Time.deltaTime;
	}
}
