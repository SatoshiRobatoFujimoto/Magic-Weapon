using UnityEngine;
using System.Collections;

public class SwordNetworkHolo : Photon.PunBehaviour {

    string roomName;
	// Use this for initialization
	void Start () {
        PhotonNetwork.ConnectUsingSettings("0.1");
        roomName = GenerateRoomName();
   
    }

    // Update is called once per frame
    void Update () {
	
	}

    void OnGUI()
    {
        GUILayout.Label(PhotonNetwork.connectionStateDetailed.ToString());
    }

    public override void OnJoinedLobby()
    {
        //base.OnJoinedLobby();
        PhotonNetwork.CreateRoom(roomName);
    }

    static string GenerateRoomName()
    {
        const string characters = "abcdefghijk";

        string result = "";

        int charAmount = Random.Range(4, 6);
        for (int i = 0; i < charAmount; i++)
        {
            result += characters[Random.Range(0, characters.Length)];
        }
        return result;
    }

    public override void OnPhotonJoinRoomFailed(object[] codeAndMsg)
    {
        base.OnPhotonJoinRoomFailed(codeAndMsg);
    }

    public override void OnCreatedRoom()
    {
        base.OnCreatedRoom();
    }
  
}
