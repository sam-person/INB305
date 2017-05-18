using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoalOnShovel : MonoBehaviour {

	// The representation of coal that is scooped up by the shovel.
	// Scaling this game object shows the amount of coal collected

	// Scale of coal on shovel
	Vector3 maxScale = new Vector3();
	Vector3 currentScale = new Vector3();

	// References
	public MeshRenderer shovelCoalRenderer;

	// Use this for initialization
	void Start () {
		maxScale = transform.localScale;
		currentScale = maxScale;

		// 
		SetScale (0f);
	}

	// Sets the Y scale of this object and turns the renderer off or on based on scale
	public void SetScale(float scale) {
		currentScale.y = scale; // Store new scale

		this.transform.localScale = currentScale; // Set scale

		// Check if scale is less than an arbitrary value
		if (currentScale.y < 0.02f) {
			shovelCoalRenderer.enabled = false; // turn off
		} else {
			shovelCoalRenderer.enabled = true; // turn on
		}
	}
}
