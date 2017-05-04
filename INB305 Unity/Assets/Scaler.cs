using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scaler : MonoBehaviour {
	public KeyCode plusKey, minusKey;
	public float scalar;

	public GameObject shovel;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (plusKey)) {
			Debug.Log ("up");
			ChangeScale (true);
		}
		if (Input.GetKeyDown (minusKey)) {
			ChangeScale (false);
			Debug.Log ("down");
		}
	}

	void ChangeScale(bool up){
		transform.localScale += Vector3.one * (up ? (scalar) : (-scalar));
	}

	void ResetScale() {
		transform.localScale = Vector3.one;
		shovel.transform.localScale = Vector3.one;
	}
}
