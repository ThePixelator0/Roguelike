using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SpawnPlayers : MonoBehaviourPunCallbacks
{
    public GameObject playerPrefab;
    public GameController controller;

    [PunRPC]
    void Start() {
        print("Player Joining Game...");
        GameObject player = PhotonNetwork.Instantiate(playerPrefab.name, new Vector2(), Quaternion.identity);
        controller.photonView.RPC("PlayerJoinedLobby", RpcTarget.All);
    }
}
