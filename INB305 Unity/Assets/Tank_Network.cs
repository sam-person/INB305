using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System.Net.Sockets;
using System.Text;

public class Tank_Network : MonoBehaviour {

	public string ip;
	public int port;

	[Range(0,180)]
	public float angle;

	Socket sender;

	// Use this for initialization
	void Start () {
		Connect (ip, port);
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.LeftArrow)) {
			Debug.Log ("Left");
			Send ("rd000");
		}
		if (Input.GetKeyDown (KeyCode.RightArrow)) {
			Debug.Log ("Right");
			Send ("rd180");
		}
		if (Input.GetKeyDown (KeyCode.UpArrow)) {
			Debug.Log ("Forward");
			Send ("rf060");
		}
		if (Input.GetKeyDown (KeyCode.DownArrow)) {
			Debug.Log ("Stop");
			Send ("rf055");
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

	public void SendAngle(float angle){
		string packet = "rd" + Mathf.RoundToInt (angle).ToString ("D3");
		Send (packet);
	}

	public void SendSpeed(float speed){
		string packet = "rf" + Mathf.RoundToInt (speed).ToString ("D3");
		Send (packet);
	}

	void OnApplicationQuit(){
		Debug.Log ("Closed network conection");
		sender.Shutdown (SocketShutdown.Both);
		sender.Close ();
	}
}
