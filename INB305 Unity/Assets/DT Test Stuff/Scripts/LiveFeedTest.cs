using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiveFeedTest : MonoBehaviour {



	// Use this for initialization
	void Start () {
		WebCamTexture webcamTexture = new WebCamTexture();
		Renderer renderer = GetComponent<Renderer>();
		renderer.material.mainTexture = webcamTexture;
		webcamTexture.Play();


		WebCamDevice[] devices = WebCamTexture.devices;
		for (int i = 0; i < devices.Length; i++) {
			Debug.Log (devices [i].name);
		}
	}
}
