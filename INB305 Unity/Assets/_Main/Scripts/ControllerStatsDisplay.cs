using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ControllerStatsDisplay : MonoBehaviour {

	public TextMeshPro textMesh;

	private string _text;

	[SerializeField]
	private Transform controller1;
	[SerializeField]
	private Transform controller2;

	// Update is called once per frame
	void Update () {
		_text = "Controller 1: " + controller1.position.x.ToString("F3") + ", " + controller1.position.y.ToString("F3") + ", " + controller1.position.z.ToString("F3")
			+ "\nController 2: " + controller2.position.x.ToString("F3") + ", " + controller2.position.y.ToString("F3") + ", " + controller2.position.z.ToString("F3");

		textMesh.SetText(_text);
	}
}
