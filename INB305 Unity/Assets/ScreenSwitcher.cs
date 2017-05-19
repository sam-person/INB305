using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenSwitcher : MonoBehaviour {

	public Material[] materials; 
	public KeyCode[] keys;
	public bool[] isRandom;

	MeshRenderer r;
	bool random = false;

	void Awake () {
		r = GetComponent<MeshRenderer> ();
	}
	
	// Update is called once per frame
	void Update () {
		for (int i = 0; i < keys.Length; i++) {
			if(Input.GetKeyDown(keys[i])){
				r.material = materials [i];
				random = isRandom [i];
				Debug.Log ("Screen switched to " + i);
			}
		}

		if (random) {
			r.material.mainTextureOffset = new Vector2 (Random.Range (-10f, 10f), Random.Range (-10f, 10f));
		}
	}
}
