/*
 
    -----------------------
    UDP-Send
    -----------------------
    // [url]http://msdn.microsoft.com/de-de/library/bb979228.aspx#ID0E3BAC[/url]
   
    // > gesendetes unter
    // 127.0.0.1 : 8050 empfangen
   
    // nc -lu 127.0.0.1 8050
 
        // todo: shutdown thread at the end
*/
using UnityEngine;
using System.Collections;
using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Collections.Generic; 
using System.Runtime.Serialization.Formatters.Binary; 
using System.IO;

public class UDPSend : MonoBehaviour
{
	private static int localPort;

	private string IP;  
	public int port; 

	public Vector3 swordPos = new Vector3(0,0,0);
	public Vector3 swordRot = new Vector3(0,0,0);

	public GameObject sword;

	IPEndPoint remoteEndPoint;
	UdpClient client;

//	string strMessage="testing";

	// start from unity3d
	public void Start()
	{
		init();
	}

	// OnGUI
	void OnGUI()
	{
		Rect rectObj=new Rect(40,10,200,400);
		GUIStyle style = new GUIStyle();
		style.alignment = TextAnchor.UpperLeft;
		GUI.Box(rectObj,"# UDPSend-Data\n127.0.0.1 "+port+" #\n"
			+ "shell> nc -lu 127.0.0.1  "+port+" \n"
			,style);

//		strMessage=GUI.TextField(new Rect(40,70,140,20),strMessage);
//		if (GUI.Button(new Rect(40,100,40,20),"send"))
//		{
//			sendString(strMessage+"\n");
//		}  
//			
	}

	public void Update() {
		swordPos = sword.transform.position;
		swordRot = sword.transform.rotation.eulerAngles;
		sendString (swordPos.ToString () + swordRot.ToString());
		print ("sword position:" + swordPos.ToString());
		print ("sword rotation:" + swordRot.ToString ());

		if (Input.GetKey(KeyCode.W)) {
			sword.transform.position = new Vector3(sword.transform.position.x + 0.2f, sword.transform.position.y, sword.transform.position.z);
		}
		if (Input.GetKey(KeyCode.S)) {
			sword.transform.position = new Vector3 (sword.transform.position.x - 0.2f, sword.transform.position.y, sword.transform.position.z);
		}
		if (Input.GetKey(KeyCode.A)) {
			sword.transform.position = new Vector3 (sword.transform.position.x, sword.transform.position.y + 0.2f, sword.transform.position.z);
		}
		if (Input.GetKey (KeyCode.D)) {
			sword.transform.position = new Vector3 (sword.transform.position.x, sword.transform.position.y - 0.2f, sword.transform.position.z);
		}
			
	}


	public void init()
	{
		print("UDPSend.init()");

		IP="158.130.168.72";
		port=12345;

		remoteEndPoint = new IPEndPoint(IPAddress.Parse(IP), port);
		client = new UdpClient();

		// status
		print("Sending to "+IP+" : "+port);
		print("Testing: nc -lu "+IP+" : "+port);

	}
		

	// send Data
	private void sendString(string message)
	{
		try
		{
			byte[] data = Encoding.UTF8.GetBytes(message);

			client.Send(data, data.Length, remoteEndPoint);
			print ("data.lenth:" + data.Length);
		}
		catch (Exception err)
		{
			print(err.ToString());
		}
	}


}


