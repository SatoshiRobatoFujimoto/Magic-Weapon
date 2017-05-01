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
        ControlledObject.transform.rotation = Quaternion.Euler(-ControllerObject.transform.rotation.eulerAngles.z, ControllerObject.transform.rotation.eulerAngles.y, ControllerObject.transform.rotation.eulerAngles.x);
        ControlledObject.transform.position = new Vector3(-ControllerObject.transform.position.z * 0.95f, ControllerObject.transform.position.y * 0.9f, ControllerObject.transform.position.x);

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
        var cube = PhotonNetwork.Instantiate("Cube", Vector3.zero, Quaternion.identity, 0);
        ControlledObject = cube;
    }
}
