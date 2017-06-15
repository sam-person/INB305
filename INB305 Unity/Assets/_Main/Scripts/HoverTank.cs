using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverTank : MonoBehaviour {

	public FakeTank_Manager controller;
	public float speed;
	public WheelCollider frontLeft, frontRight, backLeft, backRight;


	void FixedUpdate(){
		float leftTread = controller.crankL * speed;
		float rightTread = controller.crankR * speed;

		frontLeft.motorTorque = leftTread;
		backLeft.motorTorque = leftTread;
		frontRight.motorTorque = rightTread;
		backRight.motorTorque = rightTread;
	}
}
