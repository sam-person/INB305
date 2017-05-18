using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlarmSpin : MonoBehaviour {

	// Spins the alarm lights 

	public bool on = false;
	public float rotateSpeed = 1f;
	public Vector3 axis = new Vector3(0f, 1f, 0f);
	
	// Update is called once per frame
	void Update () {
		if (on) {
			transform.Rotate(axis * rotateSpeed);
		}
	}
}
