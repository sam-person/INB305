using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectRotator : MonoBehaviour {

	public Transform center;
	private Vector3 v;


	void Start() {
		center = this.transform;
		// Requires the block to be directly to the right of the center
		//   with rotation set correctly on start
		v = (transform.position - center.position);
	}

	void Update(){
		Vector3 centerScreenPos = Camera.main.WorldToScreenPoint (center.position);
		Vector3 dir = Input.mousePosition - centerScreenPos;
		float angle = Mathf.Atan2 (dir.y, dir.x) * Mathf.Rad2Deg;
		Quaternion q = Quaternion.AngleAxis (angle, -Vector3.right);
		transform.position = center.position + q * v;
		transform.rotation = q;
	}



//	private float _sensitivity;
//	private Vector3 _mouseReference;
//	private Vector3 _mouseOffset;
//	private Vector3 _rotation;
//	private bool _isRotating;
//
//	void Start ()
//	{
//		_sensitivity = 0.4f;
//		_rotation = Vector3.zero;
//	}
//
//	void Update()
//	{
//		if (Input.GetMouseButton (0)) {
//			_isRotating = true;
//			// store mouse
//			_mouseReference = Input.mousePosition;
//			Debug.Log ("Rotating");
//		} else {
//			_isRotating = false;
//		}
//		if(_isRotating)
//		{
//			// offset
//			_mouseOffset = (Input.mousePosition - _mouseReference);
//
//			// apply rotation
//			_rotation.y = -(_mouseOffset.x + _mouseOffset.y) * _sensitivity;
//
//			// rotate
//			transform.Rotate(_rotation);
//
//			// store mouse
//			_mouseReference = Input.mousePosition;
//		}
//	}
//
////	void OnMouseDown()
////	{
////		// rotating flag
////		_isRotating = true;
////
////		// store mouse
////		_mouseReference = Input.mousePosition;
////
////	}
//
////	void OnMouseUp()
////	{
////		// rotating flag
////		_isRotating = false;
////	}
//
}
