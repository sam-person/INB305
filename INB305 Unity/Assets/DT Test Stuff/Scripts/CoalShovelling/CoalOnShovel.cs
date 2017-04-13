using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoalOnShovel : MonoBehaviour {

	BoxCollider boxCollider;
	Vector3 initialScale = new Vector3();
	Vector3 currentScale = new Vector3();

	// Use this for initialization
	void Start () {
		boxCollider = GetComponent<BoxCollider> ();
		initialScale = transform.localScale;
		currentScale = initialScale;
	}
	
	public void SetScale(float scale) {
		currentScale.y = scale;
		this.transform.localScale = currentScale;
	}
}
