using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System.Net.Sockets;
using System.Text;

public class Tank_Network : MonoBehaviour {
	public bool useNetwork = false;
	public bool useTestControls = false;


	public string ip;
	public int port;

	public float speedMultiplier;

	Socket sender;

	public bool connected;

	// Use this for initialization
	void Start () {
		if (!useNetwork) {
			return;
		}
		Connect ();
	}

	// Left Forward 50: lf050
	// Left Backward 70: lb070
	// Right Forward 50: rf050
	// Right Backward 99: rb099


	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Return)) {
			AllStop ();
		}

		if (useTestControls && useNetwork) {
			if (Input.GetKey(KeyCode.Keypad7)) {
				Send ("lf100");
			}
			if (Input.GetKey(KeyCode.Keypad4)) {
				Send ("lf000");
			}
			if (Input.GetKey(KeyCode.Keypad1)) {
				Send ("lb100");
			}

			if (Input.GetKey(KeyCode.Keypad9)) {
				Send ("rf100");
			}
			if (Input.GetKey(KeyCode.Keypad6)) {
				Send ("rf000");
			}
			if (Input.GetKey(KeyCode.Keypad3)) {
				Send ("rb100");
			}
		}

		connected = sender.Connected;
	}

	public void Connect(){
		IPAddress ipaddress = IPAddress.Parse (ip);
		IPEndPoint ipendpoint = new IPEndPoint (ipaddress, port);
		sender = new Socket (AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
		sender.Connect (ipendpoint);
		Debug.Log ("Connected to tank @ " + sender.RemoteEndPoint.ToString ());
		//connected = true;
	}
		

	void Send(string message){
		byte[] msg = Encoding.ASCII.GetBytes (message);
		sender.Send (msg);
	}

	public void SendSpeed(float speedL, float speedR){
		if (speedL > 0) {
			string packetL = "lf" + Mathf.RoundToInt (Mathf.Abs (speedL*speedMultiplier)).ToString ("D3");
			Send (packetL);
			//Debug.Log (packetL);
		} else {
			string packetL = "lb" + Mathf.RoundToInt (Mathf.Abs (speedL*speedMultiplier)).ToString ("D3");
			Send (packetL);
			//Debug.Log (packetL);
		}

		if (speedR > 0) {
			string packetR = "rf" + Mathf.RoundToInt (Mathf.Abs (speedR*speedMultiplier)).ToString ("D3");
			Send (packetR);
			//Debug.Log (packetR);
		} else {
			string packetR = "rb" + Mathf.RoundToInt (Mathf.Abs (speedR*speedMultiplier)).ToString ("D3");
			Send (packetR);
			//Debug.Log (packetR);
		}


	}

	void OnApplicationQuit(){
		if (!useNetwork) {
			return;
		}
		Disconnect ();
	}

	public void Disconnect(){
		Debug.Log ("Closed network conection");
		sender.Shutdown (SocketShutdown.Both);
		sender.Close ();
	}

	public void AllStop(){
		Send ("lf000");
		//Send ("lb000");
		Send ("rf000");
		//Send ("rb000");
		Debug.Log ("Stop");
	}
}
