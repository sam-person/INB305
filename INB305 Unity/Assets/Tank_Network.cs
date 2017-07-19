using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System.Net.Sockets;
using System.Text;

public class Tank_Network : MonoBehaviour {
	public bool useNetwork = false;


	public string ip;
	public int port;

	public string testPacket;

	public float speedMultiplier;

	Socket sender;

	// Use this for initialization
	void Start () {
		if (!useNetwork) {
			return;
		}
		Connect (ip, port);
	}

	// Left Forward 50: lf050
	// Left Backward 70: lb070
	// Right Forward 50: rf050
	// Right Backward 99: rb099


	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.LeftArrow)) {
			Debug.Log ("Left");
			Send ("lb050");
			Send ("rf050");
		}
		if (Input.GetKeyDown (KeyCode.RightArrow)) {
			Debug.Log ("Right");
			Send ("rb050");
			Send ("lf050");
		}
		if (Input.GetKeyDown (KeyCode.UpArrow)) {
			Debug.Log ("Forward");
			Send ("lf050");
			Send ("rf050");
		}
		if (Input.GetKeyDown (KeyCode.DownArrow)) {
			Debug.Log ("Back");
			Send ("lb050");
			Send ("rb050");
		}
		if (Input.GetKeyDown (KeyCode.Space)) {
			Debug.Log (testPacket);
			Send (testPacket);
		}
		if (Input.GetKeyDown (KeyCode.Return)) {
			Send ("lf000");
			//Send ("lb000");
			Send ("rf000");
			//Send ("rb000");
			Debug.Log ("Stop");
		}
	}

	void Connect(string ip_, int port_){
		IPAddress ipaddress = IPAddress.Parse (ip_);
		IPEndPoint ipendpoint = new IPEndPoint (ipaddress, port_);
		sender = new Socket (AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
		sender.Connect (ipendpoint);
		Debug.Log (sender.RemoteEndPoint.ToString ());
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
		Debug.Log ("Closed network conection");
		sender.Shutdown (SocketShutdown.Both);
		sender.Close ();
	}

	void AllStop(){
		Send ("af000");
		Send ("bf000");
		Send ("cf000");
		Send ("df000");
		Send ("ef000");


		Debug.Log ("All Stop");
	}
}
