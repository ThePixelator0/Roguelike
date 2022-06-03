using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class CreateAndJoinRooms : MonoBehaviourPunCallbacks
{
    public InputField createInput;
    public InputField joinInput;

    // void Start() {
    //     PhotonNetwork.AutomaticallySyncScene = true;
    // }

    public void CreateRoom() {
        PhotonNetwork.CreateRoom(createInput.text);
    }

    public void JoinRoom() {
        PhotonNetwork.JoinRoom(joinInput.text);
    }

    public override void OnJoinedRoom() {
        PhotonNetwork.LoadLevel("Game");
    }
}
