using UnityEngine;
using System.Collections;

public class SwordNetwork : Photon.PunBehaviour
{
    public GameObject ControlledObject;
    public GameObject ControllerObject;

    void Start()
    {
        PhotonNetwork.ConnectUsingSettings("0.1");
    }

    private void Update()
    {
        ControlledObject.transform.rotation = new Quaternion(ControllerObject.transform.rotation.x,
            ControllerObject.transform.rotation.y, -ControllerObject.transform.rotation.z, -ControllerObject.transform.rotation.w);
        ControlledObject.transform.position = ControllerObject.transform.position;

    }

    void OnGUI()
    {
        GUILayout.Label(PhotonNetwork.connectionStateDetailed.ToString());
        GUILayout.Label(ControlledObject.transform.position.ToString());
    }

    public override void OnJoinedLobby()
    {
        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnJoinedRoom()
    {
        var cube = PhotonNetwork.Instantiate("Sword", Vector3.zero, Quaternion.identity, 0);
        ControlledObject = cube;
    }
}
