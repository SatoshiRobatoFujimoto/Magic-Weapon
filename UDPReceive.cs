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

public class UDPReceive : MonoBehaviour {

	// receiving Thread
	Thread receiveThread;

	// udpclient object
	UdpClient client;

	public int port;

	public Vector3 swordPos;
	public Vector3 swordRot;
	public GameObject sword;

	// infos
	public string lastReceivedUDPPacket="";
	public string allReceivedUDPPackets=""; // clean up this from time to time!

	// start from unity3d
	async void Start()
	{
		init();
	}

	// OnGUI
	void OnGUI()
	{
		Rect rectObj=new Rect(40,10,200,400);
		GUIStyle style = new GUIStyle();
		style.alignment = TextAnchor.UpperLeft;
		GUI.Box(rectObj,"# UDPReceive\n127.0.0.1 "+port+" #\n"
			+ "shell> nc -u 127.0.0.1 : "+port+" \n"
			+ "\nLast Packet: \n"+ lastReceivedUDPPacket
			+ "\n\nAll Messages: \n"+allReceivedUDPPackets
			,style);
	}

	// init
	private void init()
	{
		print("UDPSend.init()");

		// define port
		port = 8051;

		// status
		print("Sending to 127.0.0.1 : "+port);
		print("Test-Sending to this Port: nc -u 127.0.0.1  "+port+"");

		receiveThread = new Thread(
			new ThreadStart(ReceiveData));
		receiveThread.IsBackground = true;
		receiveThread.Start();
	}

	// receive thread
	private async void ReceiveData()
	{
		client = new UdpClient(port);

		while (true)
		{
			try
			{
				IPEndPoint anyIP = new IPEndPoint(IPAddress.Any, 0);
				byte[] data = client.Receive(ref anyIP);

				string text = Encoding.UTF8.GetString(data);

				print(">> " + text);

				StringToVector3(text);

//				print ("sword.transform.position:" + swordPos);
//				print ("sword.transform.rotation:" + swordRot);

				lastReceivedUDPPacket=text;
				allReceivedUDPPackets=allReceivedUDPPackets+text;
			}
			catch (Exception err)
			{
				print(err.ToString());
			}
		}
	}

	public void Update() {
		sword.transform.position = swordPos;
	}

	// getLatestUDPPacket
	// cleans up the rest
	public string getLatestUDPPacket()
	{
		allReceivedUDPPackets="";
		return lastReceivedUDPPacket;
	}

	// (4.2, 3.0, 0.0)(0.0,0.0,0.0)
	public void StringToVector3(string sVector)
	{
		string sVector_1 = "";
		string sVector_2 = "";
		// Remove the parentheses

		if (sVector.StartsWith ("(") && sVector.EndsWith (")")) {
			sVector = sVector.Substring (1, sVector.Length - 2);
		}
		// split the items
		string[] sArray_tmp = sVector.Split(')');

		string[] sArray_1 = sArray_tmp [0].Split (',');

		string sArray_tmp_tmp = sArray_tmp [1].Substring (1, sArray_tmp [1].Length - 1);

		string[] sArray_2 = sArray_tmp_tmp.Split(',');

		// store as a Vector3
		Vector3 result_1 = new Vector3(
			float.Parse(sArray_1[0]),
			float.Parse(sArray_1[1]),
			float.Parse(sArray_1[2]));

		swordPos = result_1;

		Vector3 result_2 = new Vector3(
			float.Parse(sArray_2[0]),
			float.Parse(sArray_2[1]),
			float.Parse(sArray_2[2]));

		swordRot = result_2;
	}
}
