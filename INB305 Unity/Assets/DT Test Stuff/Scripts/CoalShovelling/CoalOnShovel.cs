using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoalOnShovel : MonoBehaviour {

//	BoxCollider boxCollider;
	Vector3 maxScale = new Vector3();
	Vector3 currentScale = new Vector3();

//	public GameObject shovelCoal;
	public MeshRenderer shovelCoalRenderer;
//	Color originalColor;

	// Use this for initialization
	void Start () {
//		boxCollider = GetComponent<BoxCollider> ();
		maxScale = transform.localScale;
		currentScale = maxScale;

//		meshRenderer = shovelCoal.GetComponent<MeshRenderer> ();
//		Debug.Log (meshRenderer);
//		originalColor = meshRenderer.material.color;

		SetScale (0f);
	}
	
	public void SetScale(float scale) {
		currentScale.y = scale;
		this.transform.localScale = currentScale;
		if (currentScale.y < 0.02f) {
//			originalColor.a = 0f;
			shovelCoalRenderer.enabled = false;
		} else {
//			originalColor.a = 1f;
			shovelCoalRenderer.enabled = true;
		}
//		meshRenderer.material.color = originalColor;
	}
}
