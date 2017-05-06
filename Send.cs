using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SocketIO;

public class Send : MonoBehaviour {

	private SocketIOComponent socket;
	public Vector3 swordPos = new Vector3(0,0,0);
	public Vector3 swordRot = new Vector3(0,0,0);
	GameObject sword;
	GameObject testCube;

	// Use this for initialization
	void Start () {
		sword = GameObject.Find("Cube");
		testCube = GameObject.Find ("testCube");
		GameObject go = GameObject.Find("SocketIO");
		socket = go.GetComponent<SocketIOComponent>();
		socket.On("open", TestOpen);
		socket.On("open", OnSocketOpen);
		socket.On("error", TestError);
		socket.On("close", TestClose);
		socket.On ("ReceivePos", SwordPosition);
	}
	
	// Update is called once per frame
	void Update () {
		swordPos = sword.transform.position;
		swordRot = sword.transform.rotation.eulerAngles;
		string transmit = swordPos.ToString () + swordRot.ToString ();

		Dictionary<string, string> data = new Dictionary<string, string>();
		data["position"] = swordPos.ToString ();
		data["rotation"] = swordRot.ToString ();
		socket.Emit("SendPos", new JSONObject(data));

//		print ("sword position:" + swordPos.ToString());
//		print ("sword rotation:" + swordRot.ToString ());

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

	public void SwordPosition(SocketIOEvent e)
	{
		Debug.Log("[SocketIO] GetPosition received: " + e.name + " " + e.data);
		print ("e.name " + e.name);
		print ("e.data " + e.data);

		if (e.data == null) { return; }

		Debug.Log(
			"#####################################################" +
			"THIS: " + e.data.GetField("position").str +
			"#####################################################"
		);

		swordPos = StringToVector3 (e.data.GetField ("position").str);
		swordRot = StringToVector3 (e.data.GetField ("rotation").str);

		testCube.transform.position = new Vector3 (swordPos.x, swordPos.y + 5, swordPos.z);
	}

	public void TestOpen(SocketIOEvent e)
	{
		Debug.Log("[SocketIO] Open received: " + e.name + " " + e.data);
	}

	public void OnSocketOpen(SocketIOEvent ev){
		Debug.Log("updated socket id " + socket.sid);
	}

	public void TestError(SocketIOEvent e)
	{
		Debug.Log("[SocketIO] Error received: " + e.name + " " + e.data);
	}

	public void TestClose(SocketIOEvent e)
	{	
		Debug.Log("[SocketIO] Close received: " + e.name + " " + e.data);
	}


	// (4.2, 3.0, 0.0)(0.0,0.0,0.0)
	public Vector3 StringToVector3(string sVector)
	{
		string sVector_1 = "";
		// Remove the parentheses

		if (sVector.StartsWith ("(") && sVector.EndsWith (")")) {
			sVector = sVector.Substring (1, sVector.Length - 2);
		}
		// split the items
		string[] sArray = sVector.Split(',');

		// store as a Vector3
		Vector3 result = new Vector3(
			float.Parse(sArray[0]),
			float.Parse(sArray[1]),
			float.Parse(sArray[2]));

		return result;
	}
}
